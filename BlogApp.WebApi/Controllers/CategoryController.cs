using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;

        private IUnitOfWork _unitOfWork;

        public CategoryController(ILogger<CategoryController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public List<Category> Get()
        {
            return _unitOfWork.Categories.GetAll().Include(c => c.BlogCategories).ToList();
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
