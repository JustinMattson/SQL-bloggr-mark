using System;
using System.Collections.Generic;
using bloggr.Models;
using bloggr.Repositories;

namespace bloggr.Services
{
    public class BlogService
    {
        private readonly BlogRepository _repo;
        public BlogService(BlogRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Blog> Get()
        {
            return _repo.Get();
        }

        internal IEnumerable<Blog> Get(string user)
        {
            return _repo.Get(user);
        }

        internal Blog Get(int id)
        {
            Blog found = _repo.Get(id);
            if (found == null) { throw new Exception("Invalid Id"); }
            return found;
        }

        internal Blog Create(Blog newData)
        {
            return _repo.Create(newData);
        }

        internal Blog Edit(Blog update, string userEmail)
        {
            Blog original = Get(update.Id);
            if (original.Author != userEmail)
            {
                throw new UnauthorizedAccessException("You cannot edit this");
            }
            original.Body = update.Body;
            return _repo.Edit(original);
        }

        internal Blog Delete(int id, string userEmail)
        {
            Blog toDelete = Get(id);
            if (toDelete.Author != userEmail || !_repo.Delete(id))
            {
                throw new UnauthorizedAccessException("Invalid Access");
            }
            return toDelete;
        }
    }
}