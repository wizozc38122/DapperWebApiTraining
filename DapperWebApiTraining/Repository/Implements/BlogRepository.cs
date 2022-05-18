using DapperWebApiTraining.Repository.Interfaces;
using DapperWebApiTraining.Repository.Entities;
using Dapper;
using System.Data.SqlClient;

namespace DapperWebApiTraining.Repository.Implements
{
    public class BlogRepository : IBlogRepository
    {
        private readonly string _connectString = @"Server=localhost\SQLEXPRESS;Database=EFCore6WebApiTraining;Trusted_Connection=True;MultipleActiveResultSets=True;User ID='';Password=''";

        // C
        public async Task<int> CreateAsync(Blog blog)
        {
            using var conn = new SqlConnection(_connectString);

            var sql =
                @"INSERT INTO Blogs
                (
                    Url
                )
                VALUES
                (
                    @Url
                );

                SELECT @@IDENTITY;
                ";

            var id = await conn.ExecuteScalarAsync<int>(sql, blog);

            return id ;

        }

        // R
        public async Task<Blog?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectString);

            return await conn.QueryFirstOrDefaultAsync<Blog>("SELECT TOP 1 * FROM Blogs Where BlogId = @id",
                new
                {
                    id = id,
                });

        }

        public async Task<BlogWithPost?> GetByIdWithPostAsync(int id)
        {
            using var conn = new SqlConnection(_connectString) ;

            var sql =
                @"SELECT b.*, p.*
                FROM Blogs b
                LEFT JOIN Posts p ON
                b.BlogId = p.BlogId
                WHERE b.BlogID = @id
                ";

            BlogWithPost? blogResult = null;

            await conn.QueryAsync<BlogWithPost, Post, BlogWithPost>(sql, (b, p) =>
            {
                if (blogResult == null)
                {
                    blogResult = b;
                }

                if (blogResult.posts == null)
                {
                    blogResult.posts = new List<Post>();
                }

                if (p != null)
                {
                    blogResult.posts.Add(p);
                }

                return blogResult;
            },
            new { id = id },
            splitOn: "PostId");

            return blogResult;
        }
            

            



        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectString);


            return await conn.QueryAsync<Blog>("SELECT * FROM Blogs");

 
        }

        public async Task<IEnumerable<BlogWithPost>> GetAllWithPostAsync()
        {
            using var conn = new SqlConnection(_connectString);

            var sql =
                @"SELECT b.*, p.*
                FROM Blogs b
                LEFT JOIN Posts p ON
                b.BlogId = p.BlogId
                ";

            // 由於有多個Blog包含多個Post, 因此複數Post, 每請求出來會出現重複Blog
            // 利用字典去儲存已存在的Blog, 並添加Post
            var blogDict = new Dictionary<int, BlogWithPost>();

            await conn.QueryAsync<BlogWithPost, Post, BlogWithPost>(sql, (b, p) =>
            {
                BlogWithPost? blog;
                if (!blogDict.TryGetValue(b.BlogId, out blog))
                {
                    blogDict.Add(b.BlogId, blog = b);
                }

                if (blog.posts == null)
                {
                    blog.posts = new List<Post>();
                }

                if (p != null)
                {
                    blog.posts.Add(p);
                }

                return blog;
            }, 
            splitOn: "PostId");

            return blogDict.Values;
        }

        // U
        public async Task<bool> UpdateAsync(Blog blog)
        {
            using var conn = new SqlConnection(_connectString);

            var sql =
                @"UPDATE blogs
                SET Url=@Url
                WHERE BlogId = @BlogId;
                ";

            var count = await conn.ExecuteAsync(sql, blog);

            return (count > 0);
        }

        // D
        public async Task<bool> DeleteByIdAsync(int id)
        {
            var conn = new SqlConnection(_connectString);

            var sql =
                @"DELETE FROM Blogs
                WHERE BlogId = @id;
                ";

            var count = await conn.ExecuteAsync(sql, new { id = id });

            return count > 0;
        }

    }
}
