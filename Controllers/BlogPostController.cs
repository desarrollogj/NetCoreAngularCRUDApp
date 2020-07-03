using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAngularCRUDApp.Data;
using NetCoreAngularCRUDApp.Models;
using NetCoreAngularCRUDApp.Models.ViewModels;
using NetCoreAngularCRUDApp.Service;

namespace NetCoreAngularCRUDApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService postService;
        private readonly IBlogCategoryService categoryService;

        public BlogPostController(IBlogPostService postService, IBlogCategoryService categoryService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
        }

        // GET: api/BlogPost
        [HttpGet]
        public IEnumerable<BlogPostViewModel> GetBlogPosts()
        {
            return postService.GetAll().Select(p => new BlogPostViewModel(p));
        }

        // GET: api/BlogPost/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostViewModel>> GetBlogPost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blogPost = await postService.GetAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return new BlogPostViewModel(blogPost);
        }

        // POST: api/BlogPosts
        [HttpPost]
        public ActionResult<BlogPost> PostBlogPost(BlogPostViewModel blogPost)
        {
            if (blogPost == null)
            {
                return BadRequest();
            }

            var category = categoryService.Get(blogPost.CategoryId);

            if (category == null)
            {
                return BadRequest("Category not found");
            }

            var post = new BlogPost()
            {
                Category = category,
                Creator = blogPost.Creator,
                Title = blogPost.Title,
                Body = blogPost.Body,
                Dt = blogPost.Dt
            };

            postService.Add(post);

            return CreatedAtAction("GetBlogPost", new { id = blogPost.PostId }, blogPost);
        }

        // PUT: api/BlogPosts/5
        [HttpPut("{id}")]
        public IActionResult PutBlogPost(int id, BlogPostViewModel blogPost)
        {
            if (blogPost == null)
            {
                return BadRequest();
            }

            if (id != blogPost.PostId)
            {
                return BadRequest("Body PostId mismatch");
            }

            var category = categoryService.Get(blogPost.CategoryId);

            if (category == null)
            {
                return BadRequest("Category not found");
            }

            var post = new BlogPost()
            {
                PostId = id,
                Category = category,
                Creator = blogPost.Creator,
                Title = blogPost.Title,
                Body = blogPost.Body,
                Dt = blogPost.Dt
            };

            try
            {
                postService.Update(post);
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
        public ActionResult<BlogPostViewModel> DeleteBlogPost(int id)
        {
            var blogPost = postService.Get(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            postService.Delete(blogPost);

            return new BlogPostViewModel(blogPost);
        }

        private bool BlogPostExists(int id)
        {
            return postService.Get(id) != null;
        }
    }
}
