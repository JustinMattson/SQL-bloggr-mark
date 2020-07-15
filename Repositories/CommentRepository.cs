using System;
using System.Collections.Generic;
using System.Data;
using bloggr.Models;
using Dapper;

namespace bloggr.Repositories
{
    public class CommentRepository
    {
        private readonly IDbConnection _db;
        public CommentRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Comment> GetByBlogId(int id)
        {
            string sql = @"SELECT * FROM comments WHERE blogId = @id";
            return _db.Query<Comment>(sql, new { id });
        }

        internal Comment Get(int id)
        {
            string sql = @"SELECT * FROM comments WHERE id = @id";
            return _db.QueryFirstOrDefault<Comment>(sql, new { id });
        }

        internal Comment Create(Comment newData)
        {
            string sql = @"
            INSERT INTO comments
            (author, body, blogId)
            VALUES
            (@Author, @Body, @BlogId);
            SELECT LAST_INSERT_ID();
            ";
            newData.Id = _db.ExecuteScalar<int>(sql, newData);
            return newData;
        }

        internal Comment Edit(Comment original)
        {
            string sql = @"
            UPDATE comments
            SET
            body = @Body
            WHERE id = @Id;
            SELECT * FROM comments WHERE id = @Id;";
            return _db.QueryFirstOrDefault<Comment>(sql, original);

        }

        internal bool Delete(int id, string author)
        {
            string sql = @"DELETE FROM comments WHERE id = @id AND author = @author LIMIT 1;";
            int affected = _db.Execute(sql, new { id, author });
            return affected == 1;
        }
    }
}