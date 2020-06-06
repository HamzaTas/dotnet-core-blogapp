﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Entity;
using BlogApp.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public List<Comment> Get()
        {
            return _unitOfWork.Comments.GetAll().ToList();
        }
    }
}