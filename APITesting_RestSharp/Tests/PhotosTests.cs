using APITesting_RestSharp.Framework.Helpers;
using APITesting_RestSharp.Framework.Models;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Diagnostics;

namespace APITesting_RestSharp.Tests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Photos_Path_Tests")]
    [Category("Photos_Path_Tests")]

    public class PhotosTests : BaseTest
    {
        [Test]
        [Description("Get all photos and verify that content length header is absent in response.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method")]
        public void VerifyContentHeaderIsAbsentInResponseAfterGettingAllPhotos()
        {
            IRestResponse<List<Photo>> photoResponse = Controllers.Photos.GetAllDataFromPhotos();

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, photoResponse);
                Assert.AreEqual(-1, photoResponse.ContentLength, "Content Length is not as expected");
            });
        }

        [Test]
        [Description("Verify response time for photos, Path is less than 10 seconds")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method")]
        public void VerifyResponseTimeForPhotos()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            IRestResponse<List<Photo>> photosData = Controllers.Photos.GetAllDataFromPhotos();
            stopWatch.Stop();
            double responseTime = stopWatch.Elapsed.TotalSeconds;

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, photosData);
                Logger.Info("Assert response time");
                Assert.IsTrue(10 > responseTime, "Response time is not as expected");
            });
        }

        [Test]
        [Description("Get photos from the third album. Verify HTTP response status code. " +
        "Verify that only photos from third album are returned.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method")]
        public void VerifyReturnedPhotosDataFromThirdAlbum()
        {
            IRestResponse<List<Photo>> photoData = Controllers.Photos.GetPhotoById("3");

            Assert.Multiple(() =>
            {
                CustomAsserts.AssertStatusCode(200, photoData);
                Logger.Info("Check if data is sorted");
                Assert.IsTrue(photoData.Data.TrueForAll(x => x.AlbumId == 3), "Data is not as expected");
            });
        }

        [Test]
        [Description("Get photos from first album in range from 20th to 25th. " +
            "Verify HTTP response status code. Verify returned album and photo ids.")]
        [Author("Dmytro Spitchenko")]
        [AllureTag("GET Method Test")]
        public void VerifyReturnedAlbumAndPhotoIds20To25()
        {
            IRestResponse<List<Photo>> photoResponse = Controllers.Photos.GetAlbumIdFromPhotosByRange("20", "25");

            Assert.Multiple(() =>
            {
                Logger.Info("Assert content is not null");
                Assert.IsTrue(photoResponse.Content != null, "Content data is not as expected");
                CustomAsserts.AssertStatusCode(200, photoResponse);
                Logger.Info("Check if response data is sorted");
                Assert.False(photoResponse.Data.Exists(x => x.AlbumId == 19 || x.AlbumId == 26), "Id`s are not as expected");
            });
        }
    }
}
