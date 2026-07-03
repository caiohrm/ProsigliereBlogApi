using ProsigliereBlogApi.Models;

namespace ProsigliereBlogApi.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<List<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetByIdAsync(int id);

        Task AddAsync(BlogPost post);

        Task AddCommentAsync(Comment comment);

        Task SaveChangesAsync();
    }
}
