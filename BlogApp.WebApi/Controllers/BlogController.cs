using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
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
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;

        private IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memCache;

        public BlogController(ILogger<BlogController> logger, IUnitOfWork unitOfWork, IMemoryCache memCache)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _memCache = memCache;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (!_memCache.TryGetValue(CacheKeys.BlogKey, out List<Blog> blogs)) // if there are no data in cache get it from db
                blogs = _unitOfWork.Blogs.GetAll().Include(b => b.Comments).Include(b => b.BlogCategories).ToList();

            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public Blog Get(int id)
        {
            _logger.LogInformation("Get ", id);

            return _unitOfWork.Blogs.Get(id);
        }

        [HttpPost]
        public ActionResult Post(Blog blog)
        {
            _unitOfWork.Blogs.Add(blog);
            _unitOfWork.SaveChanges();

            _logger.LogInformation("Added ", blog.BlogId);

            return CreatedAtAction("Get", new { id = blog.BlogId }, blog);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Blog blog)
        {
            var _blog = _unitOfWork.Blogs.Get(id);

            if (_blog is null)
            {
                blog.UpdatedTime = DateTime.Now;
                blog.CreatedTime = DateTime.Now;
                _unitOfWork.Blogs.Add(blog);
                _unitOfWork.SaveChanges();
                return CreatedAtAction("Get", new { id = blog.BlogId }, blog);
            }

            _blog.Title = blog.Title;
            _blog.Description = blog.Description;
            _blog.Content = blog.Content;
            _blog.IsApproved = blog.IsApproved;
            _blog.UpdatedTime = DateTime.Now;
            _unitOfWork.SaveChanges();

            _logger.LogInformation("Updated ", id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var _blog = _unitOfWork.Blogs.Get(id);

            if (_blog is null)
                return NotFound();

            _unitOfWork.Blogs.Remove(_blog);
            _unitOfWork.SaveChanges();

            _logger.LogInformation("Deleted ", id);

            return NoContent();
        }

    }
}
