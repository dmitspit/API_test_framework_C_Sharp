using APITesting_RestSharp.Framework.Models;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Collections.Generic;

namespace APITesting_RestSharp.Framework.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IRestClient client) : base(client)
        {
        }

        public IRestResponse<List<User>> GetAllUsers()
        {
            IRestRequest restRequest = new RestRequest(Settings.UsersPath, Method.GET);
            Logger.Info("Get user data from response");
            return RestClient.Execute<List<User>>(restRequest);
        }

        public IRestResponse<User> GetUserById(string id)
        {
            IRestRequest restRequest = new RestRequest(Settings.UsersPath + $"/{id}", Method.GET);
            Logger.Info("Get user data by id");
            return RestClient.Execute<User>(restRequest);
        }

        public IRestResponse<List<User>> GetUserByCityName(string city)
        {
            Logger.Info("Get user data by city");
            IRestRequest restRequest = new RestRequest(Settings.UsersPath, Method.GET)
                   .AddQueryParameter("address.city", city);
            return RestClient.Execute<List<User>>(restRequest);
        }

        public IRestResponse<List<User>> GetUserByStreetName(string street)
        {
            Logger.Info("Get user data by street from response");
            IRestRequest restRequest = new RestRequest(Settings.UsersPath, Method.GET)
                   .AddQueryParameter("address.street", street);
            return RestClient.Execute<List<User>>(restRequest);
        }

        public IRestResponse<List<User>> GetUserDataAfterUserExclusion(string val)
        {
            IRestRequest restRequest = new RestRequest(Settings.UsersPath, Method.GET).AddParameter("id_ne", val);
            Logger.Info("Get user data from response without excluded user");
            return RestClient.Execute<List<User>>(restRequest);
        }

        public IRestResponse<AuthenticationData> RegisterNewUser(LoginData newUser)
        {
            IRestRequest restRequest = new RestRequest(Settings.RegisterPath, Method.POST);
            RestClient.UseNewtonsoftJson();
            restRequest.AddJsonBody(newUser);
            Logger.Info("Register new user");
            return RestClient.Execute<AuthenticationData>(restRequest);
        }
    }
}
