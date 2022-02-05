using APITesting_RestSharp.Framework.Models;
using RestSharp;
using System.Collections.Generic;

namespace APITesting_RestSharp.Framework.Controllers
{
    public class PostController : BaseController
    {
        public PostController(IRestClient client) : base(client)
        {
        }

        private const string PostRoute = "/664/posts";

        public IRestResponse<List<Post>> GetPostsDataByAddingMultipleIdParameters(int[] id)
        {
            Logger.Info("Get post data by adding data to parameters");
            IRestRequest request = new RestRequest(Settings.PostsPath, Method.GET);
            foreach (var value in id)
            {
                request.AddQueryParameter("id", value.ToString());
            }
            return RestClient.Execute<List<Post>>(request);
        }

        public IRestResponse<List<Post>> GetAllPostsData()
        {
            Logger.Info("Get all posts data");
            IRestRequest restRequest = new RestRequest(Settings.PostsPath, Method.GET);
            return RestClient.Execute<List<Post>>(restRequest);
        }

        public IRestResponse<List<Post>> GetLimitPostsData(string val)
        {
            Logger.Info("Get posts by limit");
            IRestRequest request = new RestRequest(Settings.PostsPath, Method.GET).AddParameter("_limit", val);
            return RestClient.Execute<List<Post>>(request);
        }

        public IRestResponse<Post> GetPostData(string id)
        {
            Logger.Info("Get post data by id");
            IRestRequest request = new RestRequest(Settings.PostsPath + $"/{id}", Method.GET);
            return RestClient.Execute<Post>(request);
        }

        public IRestResponse<Post> UpdatePost(string id, string key, string newData, LoginData loginData)
        {
            IRestClient client = RestClientFactory.GetClientWithAuthentication(loginData);
            Logger.Info("Update post data");
            IRestRequest restRequest = new RestRequest(Settings.PostsPath + $"/{id}", Method.PATCH)
                .AddOrUpdateParameter(key, newData);
            restRequest.RequestFormat = DataFormat.Json;
            return client.Execute<Post>(restRequest);
        }

        public IRestResponse<Post> CreateNewPost(Post postData, LoginData loginData = null, string path = PostRoute)
        {
            IRestRequest request = new RestRequest(path, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(postData);
            Logger.Info("Create new post");
            if (loginData != null)
            {
                Logger.Info("Add token to the client");
                IRestClient clientWithAuth = RestClientFactory.GetClientWithAuthentication(loginData);
                return clientWithAuth.Execute<Post>(request);
            }
            return RestClient.Execute<Post>(request);
        }
        public IRestResponse<Post> CreateNewPost(string jsonPostData, LoginData loginData = null, string path = PostRoute)
        {
            IRestRequest request = new RestRequest(path, Method.POST);
            request.AddJsonBody(jsonPostData);
            Logger.Info("Create new post");
            if (loginData != null)
            {
                Logger.Info("Add token to the client");
                IRestClient clientWithAuth = RestClientFactory.GetClientWithAuthentication(loginData);
                return clientWithAuth.Execute<Post>(request);

            }
            return RestClient.Execute<Post>(request);
        }

        public IRestResponse DeletePost(string postId)
        {
            IRestRequest request = new RestRequest(Settings.PostsPath + $"/{postId}", Method.DELETE);
            Logger.Info("Delete post");
            return RestClient.Delete(request);
        }
    }
}
