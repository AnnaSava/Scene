using Framework.Base.Service.ListView;

namespace Sava.Forums
{
    public class PostsViewModel : ListPageViewModel<PostViewModel>
    {
        public ForumViewModel Forum { get; set; }
        public TopicViewModel Topic { get; set; }
    }
}
