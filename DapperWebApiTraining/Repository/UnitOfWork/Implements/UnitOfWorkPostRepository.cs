using DapperWebApiTraining.Repository.UnitOfWork.Interfaces;
using DapperWebApiTraining.Repository.UnitOfWork.Bases;
using DapperWebApiTraining.Repository.Entities;
using System.Data;
using Dapper;

namespace DapperWebApiTraining.Repository.UnitOfWork.Implements
{
    public class UnitOfWorkPostRepository : RepositoryBase, IUnitOfWorkPostRepository
    {
        public UnitOfWorkPostRepository(IDbTransaction transaction) : base(transaction) { }

        // C
        public async Task<int> CreateAsync(Post post)
        { 
            var sql =
                @"INSERT INTO Posts
                (
                    Title, Content, BlogId
                )
                VALUES
                (
                    @Title, @Content, @BlogId
                );

                SELECT @@IDENTITY;
                ";

            var id = await Connection.ExecuteScalarAsync<int>(
                sql: sql, 
                param: post,
                transaction: Transaction);

            return id;
        }


        // R
        public async Task<Post?> GetByIdAsync(int id)
        {
            var sql =
                @"SELECT TOP 1 * FROM Posts
                WHERE PostId = @id
                ";

            return await Connection.QueryFirstOrDefaultAsync<Post>(
                sql: sql,
                param: new { id = id },
                transaction: Transaction);
        }


        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var sql = "SELECT * FROM Posts";

            return await Connection.QueryAsync<Post>(
                sql: sql,
                transaction: Transaction);
        }


        // U
        public async Task<bool> UpdateAsync(Post post)
        {
            var sql =
                @"UPDATE Posts
                SET Title = @Title, Content = @Content
                WHERE PostId = @PostId;
                ";

            var count = await Connection.ExecuteAsync(
                sql: sql, 
                param: post,
                transaction: Transaction);

            return count > 0;

        }

        // D
        public async Task<bool> DeleteByIdAsync(int id)
        {
            var sql =
                @"DELETE FROM Posts
                WHERE PostId = @id
                ";

            var count = await Connection.ExecuteAsync(
                sql: sql, 
                param: new { id = id },
                transaction: Transaction);

            return count > 0;
        }
    }
}
