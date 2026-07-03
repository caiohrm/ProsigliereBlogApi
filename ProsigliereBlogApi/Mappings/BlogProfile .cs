using AutoMapper;
using ProsigliereBlogApi.DTOs.Requests;
using ProsigliereBlogApi.DTOs.Responses;
using ProsigliereBlogApi.Models;

namespace ProsigliereBlogApi.Mappings
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            
            CreateMap<BlogPost, PostSummaryResponse>()
                .ForMember(dest => dest.CommentsCount,
                    opt => opt.MapFrom(src => src.Comments.Count));

            CreateMap<BlogPost, PostResponse>();

            CreateMap<Comment, CommentResponse>();


            CreateMap<CreatePostRequest, BlogPost>();

            CreateMap<CreateCommentRequest, Comment>();
        }
    }
}
