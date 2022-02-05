using APITesting_RestSharp.Framework.Controllers;
using APITesting_RestSharp.Framework.Models;
using log4net;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace APITesting_RestSharp.Framework
{
    public static class RestClientFactory
    {
        private static readonly ILog SLogger = LogManager.GetLogger(typeof(RestClientFactory));
        public static IRestClient GetClientWithAuthentication(LoginData userLoginData)
        {
            SLogger.Info("Get IRestClient object with authentication");
            IRestClient restClient = new RestClient(Settings.BaseUrl);
            AuthenticationData userAuthData = new LoginController(restClient).Login(userLoginData).Data;
            restClient.Authenticator = new JwtAuthenticator(userAuthData.AccessToken);
            restClient.UseNewtonsoftJson();
            return restClient;
        }
    }
}
