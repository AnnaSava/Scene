using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models.ListView;
using System.Threading.Tasks;

namespace Sava.Forums.Data
{
    public interface IPostService : IRestorableEntityService<PostModel>
    {
        Task<PageListModel<PostModel>> GetAllByTopic(long topicId, int page, int count);

        Task<int> GetPostsCount(long topicId);
    }
}
