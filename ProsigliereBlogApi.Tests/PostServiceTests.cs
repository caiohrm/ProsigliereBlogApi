using AutoMapper;
using Moq;
using ProsigliereBlogApi.DTOs.Requests;
using ProsigliereBlogApi.DTOs.Responses;
using ProsigliereBlogApi.Models;
using ProsigliereBlogApi.Repositories.Interfaces;
using ProsigliereBlogApi.Services;
using ProsigliereBlogApi.Exceptions;
using Xunit;
using Microsoft.Extensions.Logging;

public class PostServiceTests
{
    private readonly Mock<IPostRepository> _repoMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<ILogger<PostService>> _loggerMock = new();

    private PostService CreateService()
        => new PostService(_repoMock.Object, _mapperMock.Object, _loggerMock.Object);

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedPosts()
    {
        var posts = new List<BlogPost>
        {
            new BlogPost { Id = 1, Title = "A", Content = "A" }
        };

        var expected = new List<PostSummaryResponse>
        {
            new PostSummaryResponse { Title = "A" }
        };

        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(posts);
        _mapperMock.Setup(m => m.Map<List<PostSummaryResponse>>(posts))
                   .Returns(expected);

        var service = CreateService();

        var result = await service.GetAllAsync();

        Assert.Single(result);
        Assert.Equal("A", result.First().Title);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnPost_WhenExists()
    {
        var post = new BlogPost { Id = 1, Title = "Test", Content = "Content" };
        var response = new PostResponse { Title = "Test", Content = "Content" };

        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(post);
        _mapperMock.Setup(m => m.Map<PostResponse>(post)).Returns(response);

        var service = CreateService();

        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Test", result!.Title);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
    {
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((BlogPost)null!);

        var service = CreateService();

        var result = await service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateAndReturnPost()
    {
        var request = new CreatePostRequest
        {
            Title = "New Post",
            Content = "Content"
        };

        var post = new BlogPost { Id = 1, Title = "New Post", Content = "Content" };

        var response = new PostResponse { Title = "New Post", Content = "Content" };

        _mapperMock.Setup(m => m.Map<BlogPost>(request)).Returns(post);
        _repoMock.Setup(r => r.AddAsync(post)).Returns(Task.CompletedTask);
        _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
        _repoMock.Setup(r => r.GetByIdAsync(post.Id)).ReturnsAsync(post);
        _mapperMock.Setup(m => m.Map<PostResponse>(post)).Returns(response);

        var service = CreateService();

        var result = await service.CreateAsync(request);

        Assert.Equal("New Post", result.Title);
    }

    [Fact]
    public async Task AddCommentAsync_ShouldThrow_WhenPostNotFound()
    {
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((BlogPost)null!);

        var service = CreateService();

        await Assert.ThrowsAsync<NotFoundException>(() =>
            service.AddCommentAsync(1, new CreateCommentRequest
            {
                Author = "A",
                Content = "B"
            })
        );
    }

    [Fact]
    public async Task AddCommentAsync_ShouldSave_WhenPostExists()
    {
        var post = new BlogPost { Id = 1 };

        var comment = new Comment
        {
            Author = "A",
            Content = "B",
            BlogPostId = 1
        };

        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(post);
        _mapperMock.Setup(m => m.Map<Comment>(It.IsAny<CreateCommentRequest>()))
                   .Returns(comment);

        _repoMock.Setup(r => r.AddCommentAsync(comment)).Returns(Task.CompletedTask);
        _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var service = CreateService();

        await service.AddCommentAsync(1, new CreateCommentRequest
        {
            Author = "A",
            Content = "B"
        });

        _repoMock.Verify(r => r.AddCommentAsync(It.IsAny<Comment>()), Times.Once);
        _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}