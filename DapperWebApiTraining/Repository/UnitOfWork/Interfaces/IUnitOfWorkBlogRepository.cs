using DapperWebApiTraining.Repository.Entities;

namespace DapperWebApiTraining.Repository.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkBlogRepository
    {
        // C
        Task<int> CreateAsync(Blog blog);

        // R
        Task<Blog?> GetByIdAsync(int id);

        Task<BlogWithPost?> GetByIdWithPostAsync(int id);

        Task<IEnumerable<Blog>> GetAllAsync();

        Task<IEnumerable<BlogWithPost>> GetAllWithPostAsync();


        // U
        Task<bool> UpdateAsync(Blog blog);

        // D
        Task<bool> DeleteByIdAsync(int id);
    }
}
