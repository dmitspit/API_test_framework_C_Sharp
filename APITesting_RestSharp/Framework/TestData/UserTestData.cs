using APITesting_RestSharp.Framework.Models;
using Bogus;
using log4net;

namespace APITesting_RestSharp.Framework.TestData
{
    public static class UserTestData
    {
        private static readonly ILog SLogger = LogManager.GetLogger(typeof(UserTestData));
        private static readonly Faker SFaker = new Faker();

        public static LoginData CreateDataForNewUser()
        {
            SLogger.Info("Create data for new user");
            var newUser = new LoginData
            {
                Email = SFaker.Person.Email,
                Password = SFaker.Random.AlphaNumeric(7)
            };
            return newUser;
        }
        public static LoginData GetRegisteredUser()
        {
            SLogger.Info("Get registered user data");
            var user = new LoginData
            {
                Email = "testUser@mail.com",
                Password = "testPassword"
            };
            return user;
        }
    }
}
