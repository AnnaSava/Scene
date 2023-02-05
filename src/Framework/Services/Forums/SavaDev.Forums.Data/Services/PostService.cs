using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Models.ListView;
using Microsoft.EntityFrameworkCore;
using Sava.Forums.Data;
using Sava.Forums.Data.Entities;
using SavaDev.Base.Data.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Sava.Forums.Data.Services
{
    public class PostService : BaseEntityService<Post, PostModel>, IPostService
    {
        public PostService(ForumsContext dbContext, IMapper mapper) : base(dbContext, mapper, nameof(PostService))
        {

        }

        public async Task<PostModel> Delete(long id)
        {
            throw new NotImplementedException();
            //return await _dbContext.Delete<Post, PostModel>(id, _mapper, OnDeletingAsync);
        }

        public async Task<int> GetPostsCount(long topicId)
        {
            return await _dbContext.Set<Post>()
                .Where(m => m.TopicId == topicId)
                .CountAsync();
        }

        public async Task<PageListModel<PostModel>> GetAllByTopic(long topicId, int page, int count)
        {
            var dbSet = _dbContext.Set<Post>()
                .Where(m => m.TopicId == topicId)
                .AsNoTracking();
            var ordered = dbSet.OrderBy(m => m.Id);
            var res = await ordered.ProjectTo<PostModel>(_mapper.ConfigurationProvider).ToPagedListAsync(page, count);

            var pageModel = new PageListModel<PostModel>()
            {
                Items = res,
                Page = res.PageNumber,
                TotalPages = res.PageCount,
                TotalRows = res.TotalItemCount
            };

            return pageModel;
        }

        protected void OnAdding(Post entity)
        {
            //base.OnAdding(entity);

            var context = _dbContext as ForumsContext;

            var topic = context.Topics.First(m => m.Id == entity.TopicId);
            topic.Posts++;
            topic.LastAnswered = entity.Date;
            var forum = context.Forums.First(m => m.Id == topic.ForumId);
            forum.Posts++;
        }

        private async Task OnDeletingAsync(Post entity)
        {
            var lastPost = await GetLastPostInTopic(entity.TopicId, entity.Id);

            if (lastPost == null)
                throw new Exception("Нельзя удалить единственный пост из темы!");

            entity.Topic.LastAnswered = lastPost.Date;

            entity.Topic.Posts--;
            entity.Topic.Forum.Posts--;
        }

        protected void OnRestoring(Post entity)
        {
            entity.Topic.Posts++;
            entity.Topic.Forum.Posts++;
        }

        private async Task<Post> GetLastPostInTopic(long topicId, long exceptPostId = 0)
        {
            var query = _dbContext.Set<Post>()
                .Where(m => m.TopicId == topicId);
            if (exceptPostId != 0)
            {
                query = query.Where(m => m.Id != exceptPostId);
            }
            var lastPost = await query.OrderByDescending(m => m.Date).FirstOrDefaultAsync();
            return lastPost;
        }
    }
}
