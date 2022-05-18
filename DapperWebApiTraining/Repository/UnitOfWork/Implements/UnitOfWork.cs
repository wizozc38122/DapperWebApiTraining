using DapperWebApiTraining.Repository.UnitOfWork.Interfaces;
using DapperWebApiTraining.Repository.UnitOfWork.Bases;
using DapperWebApiTraining.Repository.Entities;
using System.Data;
using Dapper;
using System.Data.SqlClient;

namespace DapperWebApiTraining.Repository.UnitOfWork.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed = false;

        public IUnitOfWorkBlogRepository BlogRepository { get; }
        public IUnitOfWorkPostRepository PostRepository { get; }


        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();

            BlogRepository = new UnitOfWorkBlogRepository(_transaction);
            PostRepository = new UnitOfWorkPostRepository(_transaction);
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        // IDisposable
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                    }
                }
                _disposed = true;
            }
        }
    }
}
