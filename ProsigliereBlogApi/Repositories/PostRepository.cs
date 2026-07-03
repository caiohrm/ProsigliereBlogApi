using ProsigliereBlogApi.Data;
using ProsigliereBlogApi.Models;
using ProsigliereBlogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ProsigliereBlogApi.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts
                .AsNoTracking()
                .Include(x => x.Comments)
                .ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(int id)
        {
            return await _context.BlogPosts
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(BlogPost post)
        {
            await _context.BlogPosts.AddAsync(post);
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
