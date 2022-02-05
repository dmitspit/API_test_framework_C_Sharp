using APITesting_RestSharp.Framework.Controllers;
using log4net;
using NUnit.Framework;

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
        }

        [SetUp]
        public void Init()
        {
            this.Logger.Info("log4net initialized");
            this.Logger.Info("Test started");
        }
    }
}
