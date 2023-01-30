using Framework.Base.Service.ListView;

namespace Sava.Forums
{
    public class TopicsViewModel : ListPageViewModel<TopicViewModel>
    {
        public ForumViewModel Forum { get; set; }
    }
}
