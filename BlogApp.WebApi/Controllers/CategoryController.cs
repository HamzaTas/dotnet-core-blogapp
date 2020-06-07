using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Entity;
using BlogApp.Repository;
using BlogApp.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BlogApp.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;

        private IUnitOfWork _unitOfWork;

        private readonly IMemoryCache _memCache;

        public CategoryController(ILogger<CategoryController> logger, IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _memCache = memoryCache;
        }

        [HttpGet]
        public List<Category> Get()
        {
            if (!_memCache.TryGetValue(CacheKeys.CategoryKey, out List<Category> categories)) // if there are no data in cache get it from db
                categories = _unitOfWork.Categories.GetAll().Include(c => c.BlogCategories).ToList();

            return categories;
        }

        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _unitOfWork.Categories.Get(id);
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            _unitOfWork.Categories.Add(category);
            _unitOfWork.SaveChanges();

            return CreatedAtAction("Get", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Category category)
        {
            var _category = _unitOfWork.Categories.Get(id);

            if (_category is null)
            {
                category.CreatedTime = DateTime.Now;
                category.UpdatedTime = DateTime.Now;
                _unitOfWork.Categories.Add(category);
                _unitOfWork.SaveChanges();
                return CreatedAtAction("Get", new { id = category.CategoryId }, category);
            }

            _category.CategoryName = category.CategoryName;
            _category.UpdatedTime = DateTime.Now;
            _category.Description = category.Description;
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
