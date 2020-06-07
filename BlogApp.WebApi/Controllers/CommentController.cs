using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Entity;
using BlogApp.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogApp.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;

        private IUnitOfWork _unitOfWork;

        public CommentController(ILogger<CommentController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public List<Comment> Get()
        {
            return _unitOfWork.Comments.GetAll().Include(c=>c.Blog).ToList();
        }

        [HttpGet("{id}")]
        public Comment Get(int id)
        {
            return _unitOfWork.Comments.Get(id);
        }

        [HttpPost]
        public ActionResult Post(Comment comment)
        {
            _unitOfWork.Comments.Add(comment);
            _unitOfWork.SaveChanges();

            return CreatedAtAction("Get", new { id = comment.BlogId }, comment);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Comment comment)
        {
            var _comment = _unitOfWork.Comments.Get(id);

            if (_comment is null)
            {
                _unitOfWork.Comments.Add(_comment);
                _unitOfWork.SaveChanges();
                return CreatedAtAction("Get", new { id = comment.CommentId }, comment);
            }

            _comment.Name = comment.Name;
            _comment.Surname = comment.Surname;
            _comment.Content = comment.Content;
            _comment.Email = comment.Email;
            _comment.WebSite = comment.WebSite;
            _unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var _blog = _unitOfWork.Comments.Get(id);

            if (_blog is null)
                return NotFound();

            _unitOfWork.Comments.Remove(_blog);
            _unitOfWork.SaveChanges();
            return NoContent();
        }
    }
}
