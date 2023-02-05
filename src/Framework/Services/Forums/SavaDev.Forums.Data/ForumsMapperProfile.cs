using AutoMapper;
using Sava.Forums.Data;
using Sava.Forums.Data.Entities;

namespace SavaDev.Forums.Data
{
    public class ForumsMapperProfile : Profile
    {
        public ForumsMapperProfile()
        {
            CreateMap<Forum, ForumModel>();
            CreateMap<ForumModel, Forum>();

            CreateMap<Topic, TopicModel>();
            CreateMap<TopicModel, Topic>();

            CreateMap<Post, PostModel>();
            CreateMap<PostModel, Post>();
        }
    }
}
