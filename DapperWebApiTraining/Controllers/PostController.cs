using DapperWebApiTraining.Repository.Interfaces;
using DapperWebApiTraining.Repository.Entities;
using DapperWebApiTraining.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace DapperWebApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository ;

        public PostController(IPostRepository postRepository, IBlogRepository blogRepository)
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
        } 

        // C
        [HttpPost]
        public async Task<IActionResult> CreateAsync(PostCreateParameter post)
        {
            var blog = await _blogRepository.GetByIdAsync(post.BlogId);

            if (blog == null)
            {
                return BadRequest("Blog不存在");
            }

            return Ok(await _postRepository.CreateAsync(new Post() 
            { 
                Title = post.Title, 
                Content = post.Content,
                BlogId = post.BlogId,
            }
            ));
        }

        // R
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _postRepository.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _postRepository.GetAllAsync());
        }

        // U
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(PostUpdateParameter post)
        {
            return Ok(await _postRepository.UpdateAsync(new Post()
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
            }));
        }

        // D
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            return Ok(await _postRepository.DeleteByIdAsync(id));
        }

    }
}
