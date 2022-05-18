using DapperWebApiTraining.Repository.Interfaces;
using DapperWebApiTraining.Repository.Entities;
using DapperWebApiTraining.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace DapperWebApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private readonly IBlogRepository _blogRepository;
        public BlogController(IBlogRepository blogRepository) => _blogRepository = blogRepository;

        // C
        [HttpPost]
        public async Task<IActionResult> CreateAsync(BlogCreateParameter blog)
        {
            return Ok(await _blogRepository.CreateAsync( new Blog() { Url = blog.Url} ) );
        }

        // R
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _blogRepository.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _blogRepository.GetAllAsync());
        }

        [HttpGet("withPost/{id}")]
        public async Task<IActionResult> GetByIdWithPostAsync(int id)
        {
            var result = await _blogRepository.GetByIdWithPostAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("withPost")]
        public async Task<IActionResult> GetAllWithPostAsync()
        {
            return Ok(await _blogRepository.GetAllWithPostAsync());
        }

        // U
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Blog blog)
        {
            return Ok(await _blogRepository.UpdateAsync(blog));
        }

        // D
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            return Ok(await _blogRepository.DeleteByIdAsync(id));
        }
    }
}
