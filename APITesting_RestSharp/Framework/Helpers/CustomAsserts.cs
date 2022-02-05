using APITesting_RestSharp.Framework.Models;
using log4net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using NUnit.Framework;
using RestSharp;
using static APITesting_RestSharp.Framework.Models.ModelsType;

namespace APITesting_RestSharp.Framework.Helpers
{
    public static class CustomAsserts
    {
        private static ILog Logger => LogManager.GetLogger(typeof(CustomAsserts));

        public static void AssertStatusCode(int expected, IRestResponse response)
        {
            Logger.Info($"Assert status code => {expected}");
            Assert.AreEqual(expected, (int)response.StatusCode, "Status code is not as expected");
        }

        public static void AssertJsonSchema(string response, EnumModelType model)
        {
            Logger.Info("Create json schema");
            JSchema schema = GenerateJsonSchema(model);
            JObject jsonData = JObject.Parse(response);
            Logger.Info("Check if response data is valid by json schema");
            bool isValid = jsonData.IsValid(schema);

            Assert.IsTrue(isValid, "Json schema is not valid");
        }

        private static JSchema GenerateJsonSchema(EnumModelType model)
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            switch (model)
            {
                case EnumModelType.User:
                    return generator.Generate(typeof(User));
                case EnumModelType.Album:
                    return generator.Generate(typeof(Album));
                case EnumModelType.Post:
                    return generator.Generate(typeof(Post));
                default: return generator.Generate(typeof(User));
            }
        }
    }
}
