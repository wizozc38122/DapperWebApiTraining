using System.Data;

namespace DapperWebApiTraining.Repository.UnitOfWork.Bases
{
    public abstract class RepositoryBase
    {

        public IDbTransaction Transaction { get; private set; }
        public IDbConnection? Connection { get; }

        public RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
            Connection = transaction.Connection;

        }
    }    
}
