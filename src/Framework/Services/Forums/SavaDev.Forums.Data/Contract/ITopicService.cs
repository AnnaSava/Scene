using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models.ListView;
using System;
using System.Threading.Tasks;

namespace Sava.Forums.Data
{
    public interface ITopicService //: IAliasedEntityService<TopicModel>
    {
        Task<TopicModel> Create(TopicModel topicModel, PostModel postModel);

        //Task<TopicModel> Close(long topicId);

        //Task<TopicModel> Open(long topicId);

        //Task<TopicModel> IncViews(long topicId);

        //Task<TopicModel> SetLastAnswered(long topicId, DateTime date);

        Task<PageListModel<TopicModel>> GetAllByForum(long forumId, int page, int count);
    }
}
