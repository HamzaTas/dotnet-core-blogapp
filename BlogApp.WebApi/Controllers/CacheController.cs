using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Entity;
using BlogApp.Repository.Abstract;
using BlogApp.Repository.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BlogApp.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        #region fields
        private readonly ILogger<CacheController> _logger;
        private readonly IMemoryCache _memCache;

        private IUnitOfWork _unitOfWork;
        #endregion

        public CacheController(ILogger<CacheController> logger, IUnitOfWork unitOfWork, IMemoryCache memCache)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _memCache = memCache;
        }

        [HttpGet]
        public ActionResult Get()
        {
            CacheData cache = new CacheData(_memCache,_unitOfWork);
            cache.Recycle();

            return Ok("All data cached for 30 minutes");
        }
    }
}
