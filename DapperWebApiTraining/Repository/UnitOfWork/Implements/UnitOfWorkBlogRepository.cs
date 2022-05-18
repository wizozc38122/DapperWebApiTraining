using DapperWebApiTraining.Repository.UnitOfWork.Interfaces;
using DapperWebApiTraining.Repository.UnitOfWork.Bases;
using DapperWebApiTraining.Repository.Entities;
using System.Data;
using Dapper;

namespace DapperWebApiTraining.Repository.UnitOfWork.Implements
{
    public class UnitOfWorkBlogRepository : RepositoryBase, IUnitOfWorkBlogRepository
    {

        public UnitOfWorkBlogRepository(IDbTransaction transaction) : base(transaction) { }


        // C
        public async Task<int> CreateAsync(Blog blog)
        {

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

            var id = await Connection.ExecuteScalarAsync<int>(
                sql: sql,
                param: blog,
                transaction: Transaction);

            return id;

        }

        // R
        public async Task<Blog?> GetByIdAsync(int id)
        {
            var sql = "SELECT TOP 1 * FROM Blogs Where BlogId = @id";

            return await Connection.QueryFirstOrDefaultAsync<Blog>(
                sql: sql,
                param: new { id = id},
                transaction: Transaction);
        }

        public async Task<BlogWithPost?> GetByIdWithPostAsync(int id)
        {
            var sql =
                @"SELECT b.*, p.*
                FROM Blogs b
                LEFT JOIN Posts p ON
                b.BlogId = p.BlogId
                WHERE b.BlogID = @id
                ";

            BlogWithPost? blogResult = null;

            await Connection.QueryAsync<BlogWithPost, Post, BlogWithPost>(
                sql: sql, 
                (b, p) =>
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
                param: new { id = id },
                splitOn: "PostId",
                transaction: Transaction);

            return blogResult;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            var sql = "SELECT * FROM Blogs";

            return await Connection.QueryAsync<Blog>(
                sql: sql,
                transaction: Transaction);
        }

        public async Task<IEnumerable<BlogWithPost>> GetAllWithPostAsync()
        {

            var sql =
                @"SELECT b.*, p.*
                FROM Blogs b
                LEFT JOIN Posts p ON
                b.BlogId = p.BlogId
                ";

            var blogDict = new Dictionary<int, BlogWithPost>();

            await Connection.QueryAsync<BlogWithPost, Post, BlogWithPost>(
                sql: sql,
                (b, p) =>
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
                splitOn: "PostId",
                transaction: Transaction);

            return blogDict.Values;
        }

        // U
        public async Task<bool> UpdateAsync(Blog blog)
        {

            var sql =
                @"UPDATE blogs
                SET Url=@Url
                WHERE BlogId = @BlogId;
                ";

            var count = await Connection.ExecuteAsync(
                sql: sql,
                param: blog,
                transaction: Transaction);

            return count > 0;
        }

        // D
        public async Task<bool> DeleteByIdAsync(int id)
        {
            var sql =
                @"DELETE FROM Blogs
                WHERE BlogId = @id;
                ";

            var count = await Connection.ExecuteAsync(
                sql: sql,
                param: new { id =id },
                transaction: Transaction);

            return count > 0;

        }
    }
}
