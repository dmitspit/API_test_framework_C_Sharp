using APITesting_RestSharp.Framework.Models;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace APITesting_RestSharp.Framework.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(IRestClient client) : base(client)
        {
        }

        public IRestResponse<AuthenticationData> Login(LoginData loginData)
        {
            Logger.Info("Get user credentials after login");
            IRestRequest restRequest = new RestRequest(Settings.SignInPath, Method.POST);
            RestClient.UseNewtonsoftJson();
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddJsonBody(loginData);
            IRestResponse<AuthenticationData> response = RestClient.Execute<AuthenticationData>(restRequest);
            return response;
        }
    }
}
