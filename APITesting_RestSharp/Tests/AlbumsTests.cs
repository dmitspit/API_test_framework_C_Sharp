using APITesting_RestSharp.Framework.Helpers;
using APITesting_RestSharp.Framework.Models;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using RestSharp;

namespace APITesting_RestSharp.Tests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Albums_Path_Tests")]
    [Category("Albums_Path_Tests")]

    public class AlbumsTests : BaseTest
    {
        [Test]
        [Description("Get third album (path parameter) and verify content length.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method")]
        public void VerifyContentLengthOfTheThirdAlbum()
        {
            IRestResponse<Album> albumData = Controllers.Albums.GetAlbum("3");
            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, albumData);
                Logger.Info("Assert Content-Length from response");
                albumData.Content.Should().NotBeEmpty().And.NotBe("{}");
                albumData.ContentLength.Should().Be(61);
            });
        }

        [Test]
        [Description("Get non-existing album. Verify HTTP response status code.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method")]
        public void VerifyStatusCode404AfterGettingNonExistingAlbum()
        {
            IRestResponse<Album> albumData = Controllers.Albums.GetAlbum("3000");
            using (new AssertionScope())
            {
                CustomAsserts.AssertStatusCode(404, albumData);
                Logger.Info("Assert response content");
                albumData.Content.Should().Be("{}");
            }
        }
    }
}
