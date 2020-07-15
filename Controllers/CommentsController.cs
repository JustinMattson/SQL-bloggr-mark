using System;
using System.Collections.Generic;
using System.Security.Claims;
using bloggr.Models;
using bloggr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _service;
        public CommentsController(CommentService service)
        {
            _service = service;
        }

        [HttpGet("{Id}")]// GetById
        public ActionResult<Comment> Get(int Id)
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

        [HttpPost]// Post
        [Authorize]
        public ActionResult<Comment> Create([FromBody] Comment newData)
        {
            try
            {
                newData.Author = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
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
        public ActionResult<Comment> Edit([FromBody] Comment update, int Id)
        {
            try
            {
                update.Author = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                update.Id = Id;
                return Ok(_service.Edit(update));
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
        public ActionResult<Comment> Delete(int Id)
        {
            try
            {
                string email = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(_service.Delete(Id, email));
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
    }
}