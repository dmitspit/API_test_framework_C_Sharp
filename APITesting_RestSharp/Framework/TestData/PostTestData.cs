using APITesting_RestSharp.Framework.Models;
using Bogus;
using log4net;
using Newtonsoft.Json;

namespace APITesting_RestSharp.Framework.TestData
{
    public static class PostTestData
    {
        private static readonly ILog SLogger = LogManager.GetLogger(typeof(PostTestData));
        private static readonly Faker SFaker = new Faker();

        public static Post BuildPostData(int userId)
        {
            SLogger.Info("Create data for post");
            var post = new Post()
            {
                UserId = userId,
                Title = SFaker.Lorem.Word(),
                Body = SFaker.Lorem.Sentence()
            };
            return post;
        }

        public static string BuildJsonPostData(int userId)
        {
            SLogger.Info("Create json data for test");
            var post = new Post()
            {
                UserId = userId,
                Title = SFaker.Lorem.Word(),
                Body = SFaker.Lorem.Sentence()
            };
            string jsonPostData = JsonConvert.SerializeObject(post);
            return jsonPostData;
        }
    }
}
