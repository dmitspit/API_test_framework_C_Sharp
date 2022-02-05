using APITesting_RestSharp.Framework.Helpers;
using APITesting_RestSharp.Framework.Models;
using APITesting_RestSharp.Framework.TestData;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using static APITesting_RestSharp.Framework.Models.ModelsType;

namespace APITesting_RestSharp.Tests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Users_Path_Tests")]
    [Category("Users_Path_Tests")]
    public class UsersTests : BaseTest
    {
        [Test]
        [Description("Get all users. verify http response status code. verify the 5th user geo coordinates")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method Test")]
        public void Verify5ThUserGeoCoordinates()
        {
            IRestResponse<List<User>> usersData = Controllers.Users.GetAllUsers();
            Assert.IsTrue(usersData.Content != null, "Content data is not as expected");

            User user = usersData.Data.FirstOrDefault(x => x.Id == 4);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(user);
                CustomAsserts.AssertStatusCode(200, usersData);
                Logger.Info("Assert Geo data => latitude");
                Assert.AreEqual("29.4572", user.Address.Geo.Lat, "Geo latitude is not as expected");
                Logger.Info("Assert Geo data => longitude");
                Assert.AreEqual("-164.2990", user.Address.Geo.Lng, "Geo longitude is not as expected");
            });
        }

        [Test]
        [Description("Get user by street name. Verify HTTP status code. " +
            "Verify street field of returned user record.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method")]
        public void VerifyUserByStreetName()
        {
            IRestResponse<List<User>> userData = Controllers.Users.GetUserByStreetName("Douglas Extension");

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, userData);
                Logger.Info("Assert user street from response data");
                Assert.AreEqual("Douglas Extension", userData.Data.FirstOrDefault().Address.Street, "The street is not as expected");
            });
        }

        [Test]
        [Description("Get all users without the third one excluded by name. " +
          "Verify HTTP response status code. Verify that the third user in not present in response.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method")]
        public void VerifyThatThirdOneUserIsNotPresentInResponseData()
        {
            IRestResponse<List<User>> usersData = Controllers.Users.GetUserDataAfterUserExclusion("3");
            Assert.IsTrue(usersData.Content != null, "Content data is not as expected");

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, usersData);
                Logger.Info("Check if user does not exist in response data");
                Assert.IsFalse(usersData.Data.Exists(x => x.Id == 3), "Data of user id is not as expected");
            });
        }

        [Test]
        [Description("Get user by city name. Verify HTTP response status code. Verify user with proper city is returned.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method")]
        public void VerifyCityDataFromResponse()
        {
            IRestResponse<List<User>> usersData = Controllers.Users.GetUserByCityName("Gwenborough");
            string dataOfCity = usersData.Data.FirstOrDefault().Address.City;

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, usersData);
                Logger.Info("Check if city from response");
                Assert.AreEqual("Gwenborough", dataOfCity, "City from response data is not as expected");
            });
        }

        [Test]
        [Description("Get tenth user. Verify HTTP response status code. Verify response against JSON schema.")]
        [Author("Dmytro Spitchenko")]
        [Category("GET_Tests")]
        [AllureTag("GET Method Test")]
        public void VerifyJsonSchemaOf10ThUser()
        {
            IRestResponse<User> usersData = Controllers.Users.GetUserById("10");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(usersData.Content != null, "Content data is not as expected");
                CustomAsserts.AssertStatusCode(200, usersData);
                CustomAsserts.AssertJsonSchema(usersData.Content, EnumModelType.User);
            });
        }
       
        [Test]
        [Description("Register new user. Verify HTTP response status code. " +
          "Verify that access token is present is response body.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("POST Method")]
        public void VerifyStatusCodeAndAccessTokenAfterNewUserRegistering()
        {
            LoginData newUserData = UserTestData.CreateDataForNewUser();
            IRestResponse<AuthenticationData> userResponse = Controllers.Users.RegisterNewUser(newUserData);
            
            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(201, userResponse);
                Logger.Info("Assert accessToken");
                Assert.False(String.IsNullOrEmpty(userResponse.Data.AccessToken), "Access token is not as expected");
            });
        }
    }
}
