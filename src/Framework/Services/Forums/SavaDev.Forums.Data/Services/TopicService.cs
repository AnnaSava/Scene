using AutoMapper;
using AutoMapper.QueryableExtensions;
using Framework.Base.DataService.Contract.Models.ListView;
using Microsoft.EntityFrameworkCore;
using Sava.Forums.Data;
using Sava.Forums.Data.Entities;
using SavaDev.Base.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Sava.Forums.Data.Services
{
    public class TopicService : BaseEntityService<Topic, TopicModel>, ITopicService
    {
        public TopicService(ForumsContext dbContext, IMapper mapper) : base(dbContext, mapper, nameof(TopicService))
        {

        }

        public async Task<TopicModel> Create(TopicModel topicModel, PostModel postModel)
        {
            var topic = _mapper.Map<Topic>(topicModel);
            var post = _mapper.Map<Post>(postModel);

            topic.TopicPosts = new List<Post>();
            topic.TopicPosts.Add(post);

            topic.Posts = 1;
            topic.LastUpdated = topic.Date;
            topic.LastAnswered = post.Date;

            //var forum = await _dbContext.GetEntityForUpdate<Entities.Forum>(topicModel.ForumId);
            //forum.Topics++;
            //forum.Posts++;

            _dbContext.Set<Topic>().Add(topic);

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<TopicModel>(topic);
        }

        //public async Task<TopicModel> Close(long topicId) => await ChangeEntity(topicId, topic => topic.IsClosed = true);

        //public async Task<TopicModel> Open(long topicId) => await ChangeEntity(topicId, topic => topic.IsClosed = false);

        //public async Task<TopicModel> IncViews(long topicId) => await ChangeEntity(topicId, topic => topic.Views++);

        //public async Task<TopicModel> SetLastAnswered(long topicId, DateTime date) => await ChangeEntity(topicId, topic => topic.LastAnswered = date);

        public async Task<PageListModel<TopicModel>> GetAllByForum(long forumId, int page, int count)
        {
            var res = await _dbContext.Set<Topic>()
                .Where(m => m.ForumId == forumId)
                .AsNoTracking()
                .OrderBy(m => m.Id)
                .ProjectTo<TopicModel>(_mapper.ConfigurationProvider).ToPagedListAsync(page, count);

            var pageModel = new PageListModel<TopicModel>()
            {
                Items = res,
                Page = res.PageNumber,
                TotalPages = res.PageCount,
                TotalRows = res.TotalItemCount
            };

            return pageModel;
        }

        protected void OnAdding(Topic entity)
        {
            //base.OnAdding(entity);

            var context = _dbContext as ForumsContext;

            var forum = context.Forums.First(m => m.Id == entity.ForumId);
            forum.Topics++;
        }

        protected void OnDeleting(Topic entity)
        {
            entity.Forum.Topics--;
        }

        protected void OnRestoring(Topic entity)
        {
            entity.Forum.Topics++;
        }
    }
}
