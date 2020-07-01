using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAngularCRUDApp.Data;
using NetCoreAngularCRUDApp.Models;
using NetCoreAngularCRUDApp.Service;

namespace NetCoreAngularCRUDApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostService service;

        public BlogPostsController(IBlogPostService service)
        {
            this.service = service;
        }

        // GET: api/BlogPosts
        [HttpGet]
        public IEnumerable<BlogPost> GetBlogPost()
        {
            return service.GetAll();
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blogPost = await service.GetAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return blogPost;
        }

        // POST: api/BlogPosts
        [HttpPost]
        public ActionResult<BlogPost> PostBlogPost(BlogPost blogPost)
        {
            service.Add(blogPost);

            return CreatedAtAction("GetBlogPost", new { id = blogPost.PostId }, blogPost);
        }

        // PUT: api/BlogPosts/5
        [HttpPut("{id}")]
        public IActionResult PutBlogPost(int id, BlogPost blogPost)
        {
            if (id != blogPost.PostId)
            {
                return BadRequest();
            }

            try
            {
                service.Update(blogPost);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public ActionResult<BlogPost> DeleteBlogPost(int id)
        {
            var blogPost = service.Get(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            service.Delete(blogPost);

            return blogPost;
        }

        private bool BlogPostExists(int id)
        {
            return service.Get(id) != null;
        }
    }
}
