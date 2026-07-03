using AutoMapper;
using ProsigliereBlogApi.DTOs.Requests;
using ProsigliereBlogApi.DTOs.Responses;
using ProsigliereBlogApi.Exceptions;
using ProsigliereBlogApi.Models;
using ProsigliereBlogApi.Repositories.Interfaces;
using ProsigliereBlogApi.Services.Interfaces;

namespace ProsigliereBlogApi.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(IPostRepository repository, IMapper mapper, ILogger<PostService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<PostSummaryResponse>> GetAllAsync()
        {
            var posts = await _repository.GetAllAsync();

            return _mapper.Map<List<PostSummaryResponse>>(posts);
        }

        public async Task<PostResponse?> GetByIdAsync(int id)
        {
            var post = await _repository.GetByIdAsync(id);

            if (post is null)
                return null;

            return _mapper.Map<PostResponse>(post);
        }

        public async Task<PostResponse> CreateAsync(CreatePostRequest request)
        {
            _logger.LogInformation("Creating post with title: {Title}", request.Title);
            var post = _mapper.Map<BlogPost>(request);

            await _repository.AddAsync(post);
            await _repository.SaveChangesAsync();
            
            _logger.LogInformation("Post created with Id: {PostId}", post.Id);
            
            var created = await _repository.GetByIdAsync(post.Id);

            return _mapper.Map<PostResponse>(created);
        }

        public async Task AddCommentAsync(int postId, CreateCommentRequest request)
        {
            _logger.LogInformation("Adding comment to PostId: {PostId}", postId);

            var post = await _repository.GetByIdAsync(postId);

            if (post is null)
            {
                _logger.LogWarning("Post not found: {PostId}", postId);
                throw new NotFoundException($"Post {postId} not found");
            }

            var comment = _mapper.Map<Comment>(request);
            comment.BlogPostId = postId;

            await _repository.AddCommentAsync(comment);
            await _repository.SaveChangesAsync();
            
            _logger.LogInformation("Comment added to PostId: {PostId}", postId);
        }
    }
}
