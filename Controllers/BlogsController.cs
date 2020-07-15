using System;
using System.Collections.Generic;
using System.Security.Claims;
using bloggr.Models;
using bloggr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// api/blogs/user


namespace bloggr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly BlogService _service;
        private readonly CommentService _cs;
        public BlogsController(BlogService service, CommentService cs)
        {
            _service = service;
            _cs = cs;
        }

        [HttpGet] // GetAll
        public ActionResult<IEnumerable<Blog>> Get()
        {
            try
            {
                return Ok(_service.Get());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("my-blogs")]
        [Authorize]
        public ActionResult<IEnumerable<Blog>> GetByUser()
        {
            try
            {
                string UserEmail = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(_service.Get(UserEmail));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{Id}")]// GetById
        public ActionResult<Blog> Get(int Id)
        {
            try
            {
                return Ok(_service.Get(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{Id}/comments")]// GetById
        public ActionResult<IEnumerable<Comment>> GetCommentsByBlogId(int Id)
        {
            try
            {
                return Ok(_cs.GetByBlogId(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost] // Post
        [Authorize] // This line enforces user is logged in
        public ActionResult<Blog> Create([FromBody] Blog newData)
        {
            try
            {
                string UserEmail = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value; // req.userInfo.email
                newData.Author = UserEmail;
                return Ok(_service.Create(newData));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Put
        [HttpPut("{Id}")]// GetById
        [Authorize]
        public ActionResult<Blog> Edit([FromBody] Blog update, int Id)
        {
            try
            {
                string UserEmail = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                update.Id = Id;
                return Ok(_service.Edit(update, UserEmail));
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Delete
        [HttpDelete("{Id}")]// GetById
        [Authorize]
        public ActionResult<Blog> Delete(int Id)
        {
            try
            {
                string UserEmail = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(_service.Delete(Id, UserEmail));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}