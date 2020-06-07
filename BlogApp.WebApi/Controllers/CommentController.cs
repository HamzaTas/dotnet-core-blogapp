using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Entity;
using BlogApp.Repository;
using BlogApp.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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

        private IMemoryCache _memCache;

        public CommentController(ILogger<CommentController> logger, IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _memCache = memoryCache;
        }

        [HttpGet]
        public List<Comment> Get()
        {
            if (!_memCache.TryGetValue(CacheKeys.CommentKey, out List<Comment> comments)) // if there are no data in cache get it from db
                comments = _unitOfWork.Comments.GetAll().Include(c => c.Blog).ToList();

            return comments;
        }

        [HttpGet("{id}")]
        public Comment Get(int id)
        {
            _logger.LogInformation("Get ", id);
            return _unitOfWork.Comments.Get(id);
        }

        [HttpPost]
        public ActionResult Post(Comment comment)
        {
            _unitOfWork.Comments.Add(comment);
            _unitOfWork.SaveChanges();

            _logger.LogInformation("Added ", comment.BlogId);

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

            _logger.LogInformation("Updated ", id);

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

            _logger.LogInformation("Deleted ", id);

            return NoContent();
        }
    }
}
