using DapperWebApiTraining.Repository.Entities;
using DapperWebApiTraining.Repository.UnitOfWork.Interfaces;
using DapperWebApiTraining.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace DapperWebApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostUnitOfWorkController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostUnitOfWorkController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // C
        [HttpPost]
        public async Task<IActionResult> CreateAsync(PostCreateParameter post)
        {
            var blog = await _unitOfWork.BlogRepository.GetByIdAsync(post.BlogId);

            if (blog == null)
            {
                return BadRequest("Blog不存在");
            }

            var id = await _unitOfWork.PostRepository.CreateAsync(new Post()
            {
                Title = post.Title,
                Content = post.Content,
                BlogId = post.BlogId,
            });

            _unitOfWork.Commit();

            return Ok(id);
        }

        // R
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.PostRepository.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _unitOfWork.PostRepository.GetAllAsync());
        }

        // U
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(PostUpdateParameter post)
        {
            var result = await _unitOfWork.PostRepository.UpdateAsync(new Post()
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
            });

            _unitOfWork.Commit();

            return Ok(result);
        }

        // D
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var result = await _unitOfWork.PostRepository.DeleteByIdAsync(id);

            _unitOfWork.Commit();

            return Ok(result);
        }
    }
}
