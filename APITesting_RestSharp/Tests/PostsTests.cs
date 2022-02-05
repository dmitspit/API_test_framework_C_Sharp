using APITesting_RestSharp.Framework;
using APITesting_RestSharp.Framework.Helpers;
using APITesting_RestSharp.Framework.Models;
using APITesting_RestSharp.Framework.TestData;
using Bogus;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace APITesting_RestSharp.Tests
{
    [AllureNUnit]
    [AllureSuite("Posts_Path_Tests")]
    [Category("Posts_Path_Tests")]

    public class PostsTests : BaseTest
    {
        private LoginData _loginData;

        [OneTimeSetUp]
        public void SetUp()
        {
            _loginData = UserTestData.GetRegisteredUser();
        }

        [Test]
        [Description("Get all posts. Verify HTTP response status code(200) and content type")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method")]
        public void CheckResponseStatusCodeOfAllUser()
        {
            IRestResponse postsRepose = Controllers.Posts.GetAllPostsData();
            Assert.IsTrue(postsRepose.Content != null);

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, postsRepose);
                Logger.Info("Assert ContentType of response");
                Assert.AreEqual("application/json; charset=utf-8", postsRepose.ContentType, "Content type is not as expected");
            });
        }

        [Test]
        [Description("Get only first 10 posts. Verify HTTP response status code." +
            "Verify that only first posts are returned.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method")]
        public void VerifyReturnedRecordsAmountAre10()
        {
            IRestResponse<List<Post>> postsData = Controllers.Posts.GetLimitPostsData("10");
            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, postsData);
                Logger.Info("Check if data is in the limit");
                Assert.AreEqual(10, postsData.Data.Count, "Response data is not as expected");
            });
        }

        [Test]
        [Description("Get posts with id = 55 and id = 60. " +
           "Verify HTTP response status code. Verify id values of returned records.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method Test, Posts")]
        public void VerifyIdValuesOfReturnedRecordsWithId55And60()
        {
            int[] id = { 55, 60 };
            IRestResponse<List<Post>> postsResponse = Controllers.Posts.GetPostsDataByAddingMultipleIdParameters(id);
            Assert.IsNotNull(postsResponse.Content, "Result is not as expected");

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, postsResponse);
                Logger.Info("Check if response has values from selected items");
                Assert.AreEqual(55, postsResponse.Data.ElementAt(0).Id, "Id is not as expected");
                Assert.AreEqual(60, postsResponse.Data.ElementAt(1).Id, "Id is not as expected");
            });
        }

        [Test]
        [Description("Create a post. Verify HTTP response status code 401")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("POST Method")]
        public void VerifyResponseStatusCode401()
        {
            Post postData = PostTestData.BuildPostData(11);
            IRestResponse<Post> postResponse = Controllers.Posts.CreateNewPost(postData);
            CustomAsserts.AssertStatusCode(401, postResponse);

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(401, postResponse);
                Assert.IsTrue(postResponse.Content == "\"Missing authorization header\"");
            });
        }

        [NonParallelizable]
        [Test]
        [Description("Create post with adding access token in header." +
            " Verify HTTP response status code. Verify post is created.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("POST Method")]
        public void VerifyPostIsCreated()
        {
            Post postData = PostTestData.BuildPostData(11);
            IRestResponse<Post> newPostData = Controllers.Posts.CreateNewPost(postData, _loginData);
            Assert.IsTrue(newPostData.Content != null, "Content data is not as expected");

            IRestResponse<Post> postResponseData = Controllers.Posts.GetPostData($"{newPostData.Data.Id}");
            Assert.IsNotNull(postResponseData.Content, "Content data is not as expected");

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(201, newPostData);
                Logger.Info("Assert two title from posts");
                Assert.True(newPostData.Data.Title == postResponseData.Data.Title, "The title is not as expected");
            });
        }
        
        [Test]
        [Description("Create post entity and verify that the entity is created. " +
        "Verify HTTP response status code. Use JSON in body.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("POST Method Test")]
        public void VerifyResponseCodeAndDataAfterPostCreating()
        {
            string postData = PostTestData.BuildJsonPostData(11);
            IRestResponse<Post> newPostData = Controllers.Posts.CreateNewPost(postData, null, Settings.PostsPath);
            Assert.IsNotNull(newPostData.Content, "Content data is not as expected");

            Post postResult = Controllers.Posts.GetPostData($"{newPostData.Data.Id}").Data;

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(201, newPostData);
                Logger.Info("Assert two title from posts");
                Assert.True(newPostData.Data.Title == postResult.Title, "The title is not as expected");
            });
        }

        [Test]
        [Description("Update non-existing entity. Verify HTTP response status code.")]
        [Author("Dmytro Spitchenko")]
        [Category("POST_Tests")]
        [AllureTag("Patch Method Test")]
        public void VerifyStatusCode404AfterUpdatingNonExistingEntity()
        {
            string bodyStr = new Faker().Lorem.Sentence();
            IRestResponse<Post> postRespose = Controllers.Posts.UpdatePost("30000", "body", bodyStr, _loginData);

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(404, postRespose);
                Logger.Info("Assert content is not null");
                Assert.IsTrue(postRespose.Content == "{}", "Content length is not as expected");
            });
        }

        [Test]
        [Description("Create post entity and update the created entity." +
            "Verify HTTP response status code and verify that the entity is updated.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("Patch Method Test")]
        public void VerifyStatusCodeAndResponseDataAfterCreatingPostEndUpdating()
        {
            string bodyStr = new Faker().Lorem.Sentence();
            Post postData = PostTestData.BuildPostData(11);

            IRestResponse<Post> postResponseData = Controllers.Posts.CreateNewPost(postData, _loginData);
            Assert.IsNotNull(postResponseData.Content, "Content data is not as expected");

            IRestResponse<Post> postAfterUpdate = Controllers.Posts.UpdatePost(postResponseData.Data.Id.ToString(), "body", bodyStr, _loginData);
            Assert.IsNotNull(postAfterUpdate.Content, "Content data is not as expected");

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, postAfterUpdate);
                Logger.Info("Assert post body");
                Assert.AreEqual(bodyStr, postAfterUpdate.Data.Body, "The post data is not as expected");
            });
        }

        [Test]
        [Description("Delete non-existing post entity. Verify HTTP response status code.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("DELETE Method")]
        public void VerifyStatusCode404AfterDeletingNonExistingPost()
        {
            IRestResponse postResponseData = Controllers.Posts.DeletePost("11111");
            Assert.Multiple(() =>
            {
                Assert.IsTrue(postResponseData.Content == "{}", "Response content is not as expected");
                CustomAsserts.AssertStatusCode(404, postResponseData);
            });
        }

        [Test]
        [Description("Create post entity, update the created entity, and delete the entity. " +
            "Verify HTTP response status code and verify that the entity is deleted.")]
        [Author("Dmytro Spitchenko")]
        [Category("Delete_tests")]
        [AllureTag("DELETE Method")]
        public void VerifyStatusCode200AndEntityIsDeleted()
        {
            Post postData = PostTestData.BuildPostData(11);
            IRestResponse<Post> newPost = Controllers.Posts.CreateNewPost(postData, _loginData);
            Assert.IsNotNull(newPost.Content, "Content data is not as expected");

            IRestResponse updateDataResponse = Controllers.Posts.UpdatePost(newPost.Data.Id.ToString(), "title", "New Data", _loginData);
            Assert.IsNotNull(updateDataResponse.Content, "Content data is not as expected");

            IRestResponse deleteResponse = Controllers.Posts.DeletePost(newPost.Data.Id.ToString());
            Assert.IsTrue(deleteResponse.Content == "{}", "Content data is not as expected");

            IRestResponse<Post> postResponseData = Controllers.Posts.GetPostData(newPost.Data.Id.ToString());

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, deleteResponse);
                Logger.Info("Assert post data is existed");
                Assert.AreEqual("{}", postResponseData.Content, "The post data is not as expected");
            });
        }
    }
}
