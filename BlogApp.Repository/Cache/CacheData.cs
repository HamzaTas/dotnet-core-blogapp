using BlogApp.Entity;
using BlogApp.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Repository.Cache
{
    public class CacheData
    {
        private readonly IMemoryCache _memCache;
        private IUnitOfWork _unitOfWork;

        public CacheData(IMemoryCache memoryCache,  IUnitOfWork unitOfWork)
        {
            _memCache = memoryCache;
            _unitOfWork = unitOfWork;
        }

        public void Recycle()
        {
            // Removed all data from cache
            CleanCache();

            // Cache Expirationtime and Priority
            var cacheExpOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            };

            if (!_memCache.TryGetValue(CacheKeys.BlogKey, out List<Blog> blogs))
            {
                blogs = _unitOfWork.Blogs.GetAll().Include(b => b.Comments).Include(b => b.BlogCategories).ToList();
                _memCache.Set(CacheKeys.BlogKey, blogs, cacheExpOptions);
            }

            if (!_memCache.TryGetValue(CacheKeys.CategoryKey, out List<Category> categories))
            {
                categories = _unitOfWork.Categories.GetAll().ToList();
                _memCache.Set(CacheKeys.CategoryKey, categories, cacheExpOptions);
            }

            if (!_memCache.TryGetValue(CacheKeys.BlogKey, out List<Comment> comments))
            {
                comments = _unitOfWork.Comments.GetAll().ToList();
                _memCache.Set(CacheKeys.CommentKey, comments, cacheExpOptions);
            }

        }

        public void CleanCache()
        {
            _memCache.Remove(CacheKeys.BlogKey);
            _memCache.Remove(CacheKeys.CategoryKey);
            _memCache.Remove(CacheKeys.CommentKey);
        }

    }
}
