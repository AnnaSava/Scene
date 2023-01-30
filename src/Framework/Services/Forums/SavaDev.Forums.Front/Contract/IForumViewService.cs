using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sava.Forums
{
    public interface IForumViewService
    {
        Task<ForumViewModel> Create(ForumInputViewModel model);

        Task<ForumViewModel> Update(ForumInputViewModel model);

        Task<ForumViewModel> Delete(long id);

        Task<ForumViewModel> Restore(long id);

        Task<TModel> GetOne<TModel>(long id);

        Task<TViewModel> GetTopicByAlias<TViewModel>(string alias);

        Task<IEnumerable<ForumViewModel>> GetAllByModule(string module);

        Task<TopicViewModel> CreateTopic(TopicCreatingInputViewModel model);

        Task<TopicViewModel> UpdateTopic(TopicInputViewModel model);

        Task CloseTopic(long topicId);

        Task OpenTopic(long topicId);

        Task DeleteTopic(long topicId);

        Task<TViewModel> GetTopic<TViewModel>(long id);

        Task<TopicsViewModel> GetTopics(string forumAlias, int page, int count);

        Task<PostViewModel> CreatePost(PostInputViewModel model);

        Task<PostViewModel> UpdatePost(PostInputViewModel model);

        Task DeletePost(long id);

        Task<TViewModel> GetPost<TViewModel>(long id);

        Task<PostsViewModel> GetPosts(string topicAlias, int page, int postsCount);
    }
}
