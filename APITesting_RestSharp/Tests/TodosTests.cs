using APITesting_RestSharp.Framework.Helpers;
using APITesting_RestSharp.Framework.Models;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using RestSharp;

namespace APITesting_RestSharp.Tests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Todos_Method_Tests")]
    [Category("Todos_Path_Tests")]
    public class TodosTests : BaseTest
    {
        [Test]
        [Description("Verify HTTP status code and completion status of the 10th task.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method")]
        public void VerifyStatusCodeAndStatusOf10ThTask()
        {
            IRestResponse<Todo> todoResponseData = Controllers.Todos.GetTodoById("10");

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, todoResponseData);
                Logger.Info("Assert response data completed is true");
                Assert.IsTrue(todoResponseData.Data.Completed, "Completion is not as expected");
            });
        }
    }
}
