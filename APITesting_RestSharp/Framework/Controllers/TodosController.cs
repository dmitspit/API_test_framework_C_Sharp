using APITesting_RestSharp.Framework.Models;
using RestSharp;

namespace APITesting_RestSharp.Framework.Controllers
{
    public class TodosController : BaseController
    {
        public TodosController(IRestClient client) : base(client)
        {
        }

        public IRestResponse<Todo> GetTodoById(string id)
        {
            Logger.Info("Get todo data by id");
            IRestRequest restRequest = new RestRequest(Settings.TodosPath + $"/{id}", Method.GET);
            return RestClient.Execute<Todo>(restRequest);
        }
    }
}
