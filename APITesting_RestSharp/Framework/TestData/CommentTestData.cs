using APITesting_RestSharp.Framework.Models;
using Bogus;
using log4net;

namespace APITesting_RestSharp.Framework.TestData
{
    public static class CommentTestData
    {
        private static readonly ILog SLogger = LogManager.GetLogger(typeof(CommentTestData));
        private static readonly Faker SFaker = new Faker();

        public static Comment BuildComment(int postId, int id)
        {
            SLogger.Info("Build fake data for comment");
            var comment = new Comment()
            {
                PostId = postId,
                Id = id,
                Name = SFaker.Person.FirstName,
                Email = SFaker.Person.Email,
                Body = SFaker.Lorem.Sentence()
            };
            return comment;
        }
    }
}
