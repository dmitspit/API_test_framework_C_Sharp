using RestSharp;
using System;

namespace APITesting_RestSharp.Framework.Controllers
{
    public sealed class Controllers
    {
        private static Controllers _instance;

        private static readonly Lazy<UserController> UsersControllers = new Lazy<UserController>(() => new UserController(_restClient));
        private static readonly Lazy<PostController> PostControllers = new Lazy<PostController>(() => new PostController(_restClient));
        private static readonly Lazy<CommentsController> CommentsControllers = new Lazy<CommentsController>(() => new CommentsController(_restClient));
        private static readonly Lazy<AlbumsController> AlbumsControllers = new Lazy<AlbumsController>(() => new AlbumsController(_restClient));
        private static readonly Lazy<PhotosController> PhotosControllers = new Lazy<PhotosController>(() => new PhotosController(_restClient));
        private static readonly Lazy<TodosController> TodosController = new Lazy<TodosController>(() => new TodosController(_restClient));

        public UserController Users => UsersControllers.Value;
        public PostController Posts => PostControllers.Value;
        public CommentsController Comments => CommentsControllers.Value;
        public AlbumsController Albums => AlbumsControllers.Value;
        public PhotosController Photos => PhotosControllers.Value;
        public TodosController Todos => TodosController.Value;

        private static IRestClient _restClient;

        private Controllers() 
        {
            _restClient = new RestClient(Settings.BaseUrl);
        }

        public static Controllers GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Controllers();
            }
            return _instance;
        }
    }
}
