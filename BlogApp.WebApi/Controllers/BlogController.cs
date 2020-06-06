using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Entity;
using BlogApp.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogApp.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;

        private IUnitOfWork _unitOfWork;
        private IBlogRepository _repository;

        public BlogController(ILogger<BlogController> logger, IBlogRepository repository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public List<Blog> Get()
        {
            return _unitOfWork.Blogs.GetAll().Include(b=>b.Comments).Include(b=>b.BlogCategories).ToList();
        }

        [HttpGet("{id}")]
        public Blog Get(int id)
        {
            return _unitOfWork.Blogs.Get(id);
        }

        [HttpPost]
        public ActionResult Post(Blog blog)
        {
            _unitOfWork.Blogs.Add(blog);
            _unitOfWork.SaveChanges();

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
            return NoContent();
        }

    }
}
