using ProsigliereBlogApi.DTOs.Requests;
using ProsigliereBlogApi.DTOs.Responses;

namespace ProsigliereBlogApi.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<PostSummaryResponse>> GetAllAsync();

        Task<PostResponse?> GetByIdAsync(int id);

        Task<PostResponse> CreateAsync(CreatePostRequest request);

        Task AddCommentAsync(int postId, CreateCommentRequest request);
    }
}
