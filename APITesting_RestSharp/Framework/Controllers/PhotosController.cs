using APITesting_RestSharp.Framework.Models;
using RestSharp;
using System.Collections.Generic;

namespace APITesting_RestSharp.Framework.Controllers
{
    public class PhotosController : BaseController
    {
        public PhotosController(IRestClient client) : base(client)
        {
        }

        public IRestResponse<List<Photo>> GetAllDataFromPhotos()
        {
            Logger.Info("Get all photos");
            IRestRequest request = new RestRequest(Settings.PhotosPath, Method.GET);
            return RestClient.Execute<List<Photo>>(request);
        }

        public IRestResponse<List<Photo>> GetPhotoById(string id)
        {
            Logger.Info("Get photo data by id");
            IRestRequest request = new RestRequest(Settings.PhotosPath, Method.GET).AddParameter("albumId", id);
            return RestClient.Execute<List<Photo>>(request);
        }

        public IRestResponse<List<Photo>> GetAlbumIdFromPhotosByRange(string firstAlbumId, string secondAlbumId)
        {
            Logger.Info("Get a range by key");
            IRestRequest request = new RestRequest(Settings.PhotosPath, Method.GET);
            request.AddQueryParameter("albumId" + "_gte", firstAlbumId);
            request.AddQueryParameter("albumId" + "_lte", secondAlbumId);
            return RestClient.Execute<List<Photo>>(request);
        }
    }
}
