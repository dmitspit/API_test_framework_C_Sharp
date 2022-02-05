using log4net;
using RestSharp;

namespace APITesting_RestSharp.Framework.Controllers
{
    public abstract class BaseController
    {
        protected readonly ILog Logger = LogManager.GetLogger(typeof(BaseController));
        protected static IRestClient RestClient;
        protected BaseController(IRestClient client)
        {
            RestClient = client;
        }
    }
}
