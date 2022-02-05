using APITesting_RestSharp.Framework.Models;
using RestSharp;

namespace APITesting_RestSharp.Framework.Controllers
{
    public class AlbumsController : BaseController
    {
        public AlbumsController(IRestClient client) : base(client)
        {
        }

        public IRestResponse<Album> GetAlbum(string pathId)
        {
            IRestRequest request = new RestRequest(Settings.AlbumsPath + $"/{pathId}", Method.GET);
            Logger.Info("Get data from album response");
            IRestResponse<Album> response = RestClient.Execute<Album>(request);
            return response;
        }
    }
}
