using System;
using System.Collections.Generic;
using bloggr.Models;
using bloggr.Repositories;

namespace bloggr.Services
{
    public class CommentService
    {
        private readonly CommentRepository _repo;
        public CommentService(CommentRepository repo)
        {
            _repo = repo;
        }
        internal IEnumerable<Comment> GetByBlogId(int id)
        {
            return _repo.GetByBlogId(id);
        }
        internal Comment Get(int id)
        {
            Comment found = _repo.Get(id);
            if (found == null) { throw new Exception("Invalid Id"); }
            return found;
        }

        internal Comment Create(Comment newData)
        {
            return _repo.Create(newData);
        }

        internal Comment Edit(Comment update)
        {
            Comment original = Get(update.Id);
            if (original.Author != update.Author)
            {
                throw new UnauthorizedAccessException("Invalid Access");
            }
            original.Body = update.Body;
            return _repo.Edit(original);
        }

        internal Comment Delete(int id, string email)
        {
            Comment toDelete = Get(id);
            if (toDelete.Author != email || !_repo.Delete(id, email))
            {
                throw new UnauthorizedAccessException("Invalid Access");
            }
            return toDelete;
        }
    }
}