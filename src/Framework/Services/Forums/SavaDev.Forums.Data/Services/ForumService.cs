using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sava.Forums.Data;
using SavaDev.Base.Data.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Sava.Forums.Data.Services
{
    public class ForumService : BaseEntityService<Entities.Forum, ForumModel>, IForumService
    {
        public ForumService(ForumsContext dbContext, IMapper mapper) : base(dbContext, mapper, nameof(ForumService))
        {

        }

        //public async Task<ForumModel> IncTopics(long forumId, int count = 1) => await ChangeEntity(forumId, forum => forum.Topics+=count);

        //public async Task<ForumModel> DecTopics(long forumId, int count = 1) => await ChangeEntity(forumId, forum => forum.Topics-=count);

        //public async Task<ForumModel> IncPosts(long forumId, int count = 1) => await ChangeEntity(forumId, forum => forum.Posts+=count);

        //public async Task<ForumModel> DecPosts(long forumId, int count = 1) => await ChangeEntity(forumId, forum => forum.Posts -= count);

        public async Task<IEnumerable<ForumModel>> GetAllByModule(string module)
        {
            return await _dbContext.Set<Entities.Forum>()
                .Where(m => m.Section == module)
                .AsNoTracking()
                .OrderBy(m => m.Id)
                .ProjectTo<ForumModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        protected void ValidateCreate(ForumModel model)
        {
            //base.ValidateCreate(model);

            //if (string.IsNullOrWhiteSpace(model.Title))
            //    throw new ProjectArgumentException(
            //        GetType(),
            //        nameof(ValidateCreate),
            //        nameof(model.Title),
            //        model.Title);
        }

        protected void ValidateUpdate(ForumModel model)
        {
            //base.ValidateUpdate(model);

            //if (string.IsNullOrWhiteSpace(model.Title))
            //    throw new ProjectArgumentException(
            //        GetType(),
            //        nameof(ValidateCreate),
            //        nameof(model.Title),
            //        model.Title);
        }
    }
}
