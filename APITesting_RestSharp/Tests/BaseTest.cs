using APITesting_RestSharp.Framework.Controllers;
using APITesting_RestSharp.Framework.Models;
using APITesting_RestSharp.Framework.TestData;
using log4net;
using NUnit.Framework;
using RestSharp;

namespace APITesting_RestSharp.Tests
{
    public class BaseTest
    {
        protected ILog Logger;
        protected Controllers Controllers;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Controllers = Controllers.GetInstance();
            this.Logger = LogManager.GetLogger(typeof(BaseTest));
            CreateNewUserIfItDoesNotExist();
        }

        [SetUp]
        public void Init()
        {
            this.Logger.Info("log4net initialized");
            this.Logger.Info("Test started");
        }

        private void CreateNewUserIfItDoesNotExist()
        {
            IRestResponse<AuthenticationData> authenticationData = Controllers.Login.Login(UserTestData.GetRegisteredUser());
            if ((int)authenticationData.StatusCode != 200 )
            {
                Controllers.Users.RegisterNewUser(UserTestData.GetRegisteredUser());
            }
        }
    }
}
