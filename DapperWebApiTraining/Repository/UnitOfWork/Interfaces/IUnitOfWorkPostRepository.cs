using DapperWebApiTraining.Repository.Entities;

namespace DapperWebApiTraining.Repository.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkPostRepository
    {
        // C
        Task<int> CreateAsync(Post post);


        // R
        Task<Post?> GetByIdAsync(int id);


        Task<IEnumerable<Post>> GetAllAsync();


        // U
        Task<bool> UpdateAsync(Post post);

        // D
        Task<bool> DeleteByIdAsync(int id);
    }
}
