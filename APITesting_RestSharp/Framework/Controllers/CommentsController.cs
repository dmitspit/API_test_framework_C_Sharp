using APITesting_RestSharp.Framework.Models;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Collections.Generic;

namespace APITesting_RestSharp.Framework.Controllers
{
    public class CommentsController : BaseController
    {
        public CommentsController(IRestClient client) : base(client)
        {
        }

        public IRestResponse<List<Comment>> GetCommentsData()
        {
            Logger.Info("Get comment data");
            IRestRequest request = new RestRequest(Settings.CommentsPath, Method.GET);
            return RestClient.Execute<List<Comment>>(request);
        }

        public IRestResponse<List<Comment>> GetSortedComments(string sortValue, string orderValue)
        {
            Logger.Info("Get sorted data from response");
            IRestRequest request = new RestRequest(Settings.CommentsPath, Method.GET)
            .AddQueryParameter("_sort", sortValue)
            .AddQueryParameter("_order", orderValue);
            return RestClient.Execute<List<Comment>>(request);
        }

        public IRestResponse<List<Comment>> CreateComment(Comment comment)
        {
            Logger.Info("Create comment data");
            IRestRequest request = new RestRequest(Settings.CommentsPath, Method.POST);
            RestClient.UseNewtonsoftJson();
            request.AddJsonBody(comment);
            return RestClient.Execute<List<Comment>>(request);
        }
    }
}

