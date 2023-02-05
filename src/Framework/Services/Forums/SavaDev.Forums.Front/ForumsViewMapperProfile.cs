using AutoMapper;
using Sava.Forums;
using Sava.Forums.Helpers;
using Sava.Forums.Data;
using System;

namespace SavaDev.Forums.Front
{
    public class ForumsViewMapperProfile : Profile
    {
        public ForumsViewMapperProfile()
        {
            CreateMap<ForumViewModel, ForumModel>();
            CreateMap<ForumModel, ForumViewModel>();

            CreateMap<ForumInputViewModel, ForumModel>();
            CreateMap<ForumModel, ForumInputViewModel>();

            CreateMap<TopicViewModel, TopicModel>();
            CreateMap<TopicModel, TopicViewModel>();

            CreateMap<TopicInputViewModel, TopicModel>();
            CreateMap<TopicModel, TopicInputViewModel>();

            CreateMap<TopicCreatingInputViewModel, TopicModel>()
                .ForMember(x => x.Date, y => y.MapFrom(s => string.IsNullOrEmpty(s.Date) ? DateTime.UtcNow : DateTime.Parse(s.Date))); //TODO заменить на TryParse и ConvertUsing;

            CreateMap<TopicCreatingInputViewModel, PostModel>()
                .ForMember(x => x.Date, y => y.MapFrom(s => string.IsNullOrEmpty(s.Date) ? DateTime.UtcNow : DateTime.Parse(s.Date))); //TODO заменить на TryParse и ConvertUsing;

            CreateMap<PostViewModel, PostModel>();
            CreateMap<PostModel, PostViewModel>()
                .ForMember(x => x.Text, y => y.MapFrom(s => BBCodePreprocessor.Process(s.Text)));

            CreateMap<PostInputViewModel, PostModel>()
                .ForMember(x => x.Date, y => y.MapFrom(s => string.IsNullOrEmpty(s.Date) ? DateTime.UtcNow : DateTime.Parse(s.Date))); //TODO заменить на TryParse и ConvertUsing
            CreateMap<PostModel, PostInputViewModel>()
                .ForMember(x => x.Date, y => y.MapFrom(s => s.Date.ToString("dd-MM-yyyy HH:mm")));
        }
    }
}
