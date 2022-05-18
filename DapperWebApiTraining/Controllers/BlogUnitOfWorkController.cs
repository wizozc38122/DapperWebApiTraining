using DapperWebApiTraining.Repository.Entities;
using DapperWebApiTraining.Repository.UnitOfWork.Interfaces;
using DapperWebApiTraining.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace DapperWebApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogUnitOfWorkController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogUnitOfWorkController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // C
        [HttpPost]
        public async Task<IActionResult> CreateAsync(BlogCreateParameter blog)
        {
            var id = await _unitOfWork.BlogRepository.CreateAsync(new Blog() { Url = blog.Url });

            _unitOfWork.Commit();

            return Ok(id);
        }

        // R
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {

            var result = await _unitOfWork.BlogRepository.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            _unitOfWork.Commit();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _unitOfWork.BlogRepository.GetAllAsync();


            return Ok(result);
        }

        [HttpGet("withPost/{id}")]
        public async Task<IActionResult> GetByIdWithPostAsync(int id)
        {
            var result = await _unitOfWork.BlogRepository.GetByIdWithPostAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("withPost")]
        public async Task<IActionResult> GetAllWithPostAsync()
        {
            return Ok(await _unitOfWork.BlogRepository.GetAllWithPostAsync());
        }

        // U
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Blog blog)
        {
            var result = await _unitOfWork.BlogRepository.UpdateAsync(blog);

            _unitOfWork.Commit();

            return Ok(result);
        }

        // D
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var result = await _unitOfWork.BlogRepository.DeleteByIdAsync(id);

            _unitOfWork.Commit();

            return Ok(result);
        }
    }
}
