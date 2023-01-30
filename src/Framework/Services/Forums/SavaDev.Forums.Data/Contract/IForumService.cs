using Framework.Base.DataService.Contract.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sava.Forums.Data
{
    public interface IForumService : IAliasedEntityService<ForumModel>
    {
        Task<ForumModel> IncTopics(long forumId, int count = 1);

        Task<ForumModel> DecTopics(long forumId, int count = 1);

        Task<ForumModel> IncPosts(long forumId, int count = 1);

        Task<ForumModel> DecPosts(long forumId, int count = 1);

        Task<IEnumerable<ForumModel>> GetAllByModule(string module);
    }
}
