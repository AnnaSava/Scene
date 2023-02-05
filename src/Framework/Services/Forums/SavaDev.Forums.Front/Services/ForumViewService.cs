using AutoMapper;
using Sava.Forums.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Sava.Forums.Services
{
    public class ForumViewService// : IForumViewService
    {
        private readonly IForumService _forumDbService;
        private readonly ITopicService _topicDbService;
        private readonly IPostService _postDbService;
        private readonly IMapper _mapper;

        public ForumViewService(IForumService forumDbService, ITopicService topicDbService, IPostService postDbService, IMapper mapper)
        {
            _forumDbService = forumDbService;
            _topicDbService = topicDbService;
            _postDbService = postDbService;
            _mapper = mapper;
        }

        //public async Task<ForumViewModel> Create(ForumInputViewModel model)
        //{
        //    var newModel = _mapper.Map<ForumModel>(model);
        //    var resultModel = await _forumDbService.Create(newModel);
        //    return _mapper.Map<ForumViewModel>(resultModel);
        //}

        //public async Task<ForumViewModel> Update(ForumInputViewModel model)
        //{
        //    var updatedModel = _mapper.Map<ForumModel>(model);
        //    var resultModel = await _forumDbService.Update(model.Id, updatedModel);
        //    return _mapper.Map<ForumViewModel>(resultModel);
        //}

        //public async Task<ForumViewModel> Delete(long id)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<ForumViewModel> Restore(long id)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<TModel> GetOne<TModel>(long id)
        //{
        //    var model = await _forumDbService.GetOne(id);
        //    return _mapper.Map<TModel>(model);
        //}

        //public async Task<IEnumerable<ForumViewModel>> GetAllByModule(string module)
        //{
        //    var list = await _forumDbService.GetAllByModule(module);
        //    return _mapper.Map<IEnumerable<ForumViewModel>>(list);
        //}

        //public async Task<TopicViewModel> CreateTopic(TopicCreatingInputViewModel model)
        //{
        //    var newModel = _mapper.Map<TopicModel>(model);
        //    var postModel = _mapper.Map<PostModel>(model);

        //    var resultModel = await _topicDbService.Create(newModel, postModel);

        //    return _mapper.Map<TopicViewModel>(resultModel);
        //}

        //public async Task<TopicViewModel> UpdateTopic(TopicInputViewModel model)
        //{
        //    var updatedModel = _mapper.Map<TopicModel>(model);
        //    var resultModel = await _topicDbService.Update(model.Id, updatedModel);
        //    return _mapper.Map<TopicViewModel>(resultModel);
        //}

        //public async Task CloseTopic(long topicId)
        //{
        //    await _topicDbService.Close(topicId);
        //}

        //public async Task OpenTopic(long topicId)
        //{
        //    await _topicDbService.Open(topicId);
        //}

        //public async Task DeleteTopic(long topicId)
        //{
        //    var topic = await _topicDbService.GetOne(topicId);

        //    await _topicDbService.Delete(topicId);

        //    //не удаляем ответы на случай восстановления темы, счетчики сбрасываем
        //    await _forumDbService.DecTopics(topic.ForumId);

        //    var postsCount = await _postDbService.GetPostsCount(topicId);
        //    await _forumDbService.DecPosts(topic.ForumId, postsCount);
        //}

        //public async Task<TViewModel> GetTopic<TViewModel>(long id)
        //{
        //    var topic = await _topicDbService.GetOne(id);
        //    return _mapper.Map<TViewModel>(topic);
        //}

        //public async Task<TViewModel> GetTopicByAlias<TViewModel>(string alias)
        //{
        //    var topic = await _topicDbService.GetOneByAlias(alias);
        //    return _mapper.Map<TViewModel>(topic);
        //}

        //public async Task<TopicsViewModel> GetTopics(string forumAlias, int page, int count)
        //{
        //    var forum = await _forumDbService.GetOneByAlias(forumAlias);
        //    var topics = await _topicDbService.GetAllByForum(forum.Id, page, count);

        //    return new TopicsViewModel
        //    {
        //        Forum = _mapper.Map<ForumViewModel>(forum),
        //        Items = topics.Items.Select(m => _mapper.Map<TopicModel, TopicViewModel>(m)).ToList(),
        //        Page = topics.Page,
        //        TotalPages = topics.TotalPages,
        //        TotalRows = topics.TotalRows
        //    };
        //}

        //public async Task<PostViewModel> CreatePost(PostInputViewModel model)
        //{
        //    var newModel = _mapper.Map<PostModel>(model);
        //    var resultModel = await _postDbService.Create(newModel);
        //    return _mapper.Map<PostViewModel>(resultModel);
        //}

        //public async Task<PostViewModel> UpdatePost(PostInputViewModel model)
        //{
        //    var updatedModel = _mapper.Map<PostModel>(model);
        //    var resultModel = await _postDbService.Update(model.Id, updatedModel);
        //    return _mapper.Map<PostViewModel>(resultModel);
        //}

        //public async Task DeletePost(long id)
        //{
        //    await _postDbService.Delete(id);
        //}

        //public async Task<TViewModel> GetPost<TViewModel>(long id)
        //{
        //    var topic = await _postDbService.GetOne(id);
        //    return _mapper.Map<TViewModel>(topic);
        //}

        //public async Task<PostsViewModel> GetPosts(string topicAlias, int page, int postsCount)
        //{
        //    var topic = await _topicDbService.GetOneByAlias(topicAlias);
        //    var forum = await _forumDbService.GetOne(topic.ForumId);

        //    var posts = await _postDbService.GetAllByTopic(topic.Id, page, postsCount);

        //    return new PostsViewModel
        //    {
        //        Forum = _mapper.Map<ForumViewModel>(forum),
        //        Topic = _mapper.Map<TopicViewModel>(topic),
        //        Items = posts.Items.Select(m => _mapper.Map<PostModel, PostViewModel>(m)).ToList(),
        //        Page = posts.Page,
        //        TotalPages = posts.TotalPages,
        //        TotalRows = posts.TotalRows
        //    };
        //}
    }
}
