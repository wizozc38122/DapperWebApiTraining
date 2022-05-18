namespace DapperWebApiTraining.Repository.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUnitOfWorkBlogRepository BlogRepository { get; }
        IUnitOfWorkPostRepository PostRepository { get; }

        void Commit();

    }
}
