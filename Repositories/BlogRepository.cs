using System;
using System.Collections.Generic;
using System.Data;
using bloggr.Models;
using Dapper;

namespace bloggr.Repositories
{
    public class BlogRepository
    {
        private readonly IDbConnection _db;
        public BlogRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Blog> GetByBlogId(int id)
        {
            string sql = @"SELECT * FROM blogs WHERE blogId = @id";
            return _db.Query<Blog>(sql, new { id });
        }

        internal IEnumerable<Blog> Get(string user)
        {
            string sql = @"SELECT * FROM blogs WHERE author = @user";
            return _db.Query<Blog>(sql, new { user });
        }

        internal IEnumerable<Blog> Get()
        {
            string sql = @"SELECT * FROM blogs";
            return _db.Query<Blog>(sql);
        }

        internal Blog Get(int id)
        {
            string sql = @"SELECT * FROM blogs WHERE id = @id";
            return _db.QueryFirstOrDefault<Blog>(sql, new { id });
        }

        internal Blog Create(Blog newData)
        {
            string sql = @"
            INSERT INTO blogs
            (author, body, title)
            VALUES
            (@Author, @Body, @Title);
            SELECT LAST_INSERT_ID();
            ";
            newData.Id = _db.ExecuteScalar<int>(sql, newData);
            return newData;
        }

        internal Blog Edit(Blog original)
        {
            string sql = @"
            UPDATE blogs
            SET
            body = @Body
            WHERE id = @Id;
            SELECT * FROM blogs WHERE id = @Id;";
            return _db.QueryFirstOrDefault<Blog>(sql, original);

        }

        internal bool Delete(int id)
        {
            string sql = @"DELETE FROM blogs WHERE id = @id LIMIT 1;";
            int affected = _db.Execute(sql, new { id });
            return affected == 1;
        }
    }
}