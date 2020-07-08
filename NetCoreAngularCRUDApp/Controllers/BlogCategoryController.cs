using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NetCoreAngularCRUDApp.Models;
using NetCoreAngularCRUDApp.Service;

namespace NetCoreAngularCRUDApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IBlogCategoryService service;

        public BlogCategoryController(IBlogCategoryService service)
        {
            this.service = service;
        }

        // GET: api/BlogCategory
        [HttpGet]
        public IEnumerable<BlogCategory> GetBlogCategories()
        {
            return service.GetAll();
        }
    }
}
