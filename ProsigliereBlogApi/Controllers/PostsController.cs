using Microsoft.AspNetCore.Mvc;
using ProsigliereBlogApi.DTOs.Requests;
using ProsigliereBlogApi.Services.Interfaces;

namespace ProsigliereBlogApi.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _service;

        public PostsController(IPostService service)
        {
            _service = service;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            var result = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result
            );
        }

        [HttpPost("{id:int}/comments")]
        public async Task<IActionResult> AddComment(int id, [FromBody] CreateCommentRequest request)
        {
            await _service.AddCommentAsync(id, request);

            return NoContent();
        }
    }
}
