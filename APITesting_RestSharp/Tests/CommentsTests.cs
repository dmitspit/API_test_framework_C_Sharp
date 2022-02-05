using APITesting_RestSharp.Framework.Helpers;
using APITesting_RestSharp.Framework.Models;
using APITesting_RestSharp.Framework.TestData;
using FluentAssertions;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace APITesting_RestSharp.Tests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Comments_Path_Tests")]
    [Category("Comments_Path_Tests")]

    public class CommentsTests : BaseTest
    {

        [Test]
        [Description("Get all comments and verify response charset")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method")]
        public void VerifyResponseCharsetAfterGettingAllComments()
        {
            IRestResponse commentsResponse = Controllers.Comments.GetCommentsData();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(commentsResponse.Content != null, "The content data is not as expected");
                CustomAsserts.AssertStatusCode(200, commentsResponse);
                Logger.Info("Assert response Content-Type from comments Path");
                Assert.IsTrue(commentsResponse.ContentType.Contains("charset=utf-8"), "charset type is not as expected");
            });
        }

        [Test]
        [Description("Get comments with postId sorted in descending order. Verify HTTP response status code. " +
            "Verify that records are sorted in response.")]
        [Author("Dmytro Spitchenko")]
        [Category("GET_Tests")]
        [AllureTag("GET Method Test, Comments")]
        public void VerifyResponseDataFromCommentsWithPostIdSortedInDescendingOder()
        {
            IRestResponse<List<Comment>> commentsResponse = Controllers.Comments.GetSortedComments("postId", "desc");

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, commentsResponse);
                Logger.Info("Check if data is sorted");
                commentsResponse.Data.Select(x => x.PostId).Should().BeInDescendingOrder();
            });
        }

        [Test]
        [Description("Create already existing comment entity. Verify HTTP response status code.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("POST Method")]
        public void VerifyStatusCodeAfterCreatingExistingCommentEntity()
        {
            Comment commentData = CommentTestData.BuildComment(1, 1);
            IRestResponse<List<Comment>> commentDataFromResponse = Controllers.Comments.CreateComment(commentData);

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(500, commentDataFromResponse);
                Logger.Info("Check if data is sorted");
                Assert.IsTrue(commentDataFromResponse.Content.Contains("Error: Insert failed"), "The content data is not as expected");
            });
        }
    }
}