using DapperWebApiTraining.Repository.Interfaces;
using DapperWebApiTraining.Repository.Entities;
using Dapper;
using System.Data.SqlClient;


namespace DapperWebApiTraining.Repository.Implements
{
    public class PostRepository : IPostRepository
    {
        private readonly string _connectString = @"Server=localhost\SQLEXPRESS;Database=EFCore6WebApiTraining;Trusted_Connection=True;MultipleActiveResultSets=True;User ID='';Password=''";
        // C
        public async Task<int> CreateAsync(Post post)
        {
            using var conn = new SqlConnection(_connectString);

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

            var id = await conn.ExecuteScalarAsync<int>(sql, post);

            return id;
        }


        // R
        public async Task<Post?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectString);

            var sql =
                @"SELECT TOP 1 * FROM Posts
                WHERE PostId = @id
                ";

            return await conn.QueryFirstOrDefaultAsync<Post>(sql, new { id = id });
        }


        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectString);

            return await conn.QueryAsync<Post>("SELECT * FROM Posts");
        }


        // U
        public async Task<bool> UpdateAsync(Post post)
        {
            using var conn = new SqlConnection(_connectString);

            var sql =
                @"UPDATE Posts
                SET Title = @Title, Content = @Content
                WHERE PostId = @PostId;
                ";

            var count = await conn.ExecuteAsync(sql, post);

            return count > 0;

        }

        // D
        public async Task<bool> DeleteByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectString);

            var sql =
                @"DELETE FROM Posts
                WHERE PostId = @id
                ";

            var count = await conn.ExecuteAsync(sql, new { id = id });

            return count > 0;
        }
    }
}
