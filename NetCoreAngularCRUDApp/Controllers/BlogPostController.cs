using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCoreAngularCRUDApp.Models;
using NetCoreAngularCRUDApp.Models.ViewModels;
using NetCoreAngularCRUDApp.Service;
using NLog.Fluent;

namespace NetCoreAngularCRUDApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly ILogger<BlogPostController> logger;
        private readonly IBlogPostService postService;
        private readonly IBlogCategoryService categoryService;

        public BlogPostController(ILogger<BlogPostController> logger, IBlogPostService postService, IBlogCategoryService categoryService)
        {
            this.logger = logger;
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
                logger.LogDebug("Blog post {Id} not found", id);
                return NotFound();
            }

            return new BlogPostViewModel(blogPost);
        }

        // POST: api/BlogPost
        [HttpPost]
        public ActionResult<BlogPostViewModel> PostBlogPost(BlogPostViewModel blogPost)
        {
            if (blogPost == null)
            {
                logger.LogDebug("Received a null content");
                return BadRequest();
            }

            var category = categoryService.Get(blogPost.CategoryId);

            if (category == null)
            {
                logger.LogDebug("Category {Id} not found", blogPost.CategoryId);
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

            return CreatedAtAction("GetBlogPost", new { id = post.PostId }, new BlogPostViewModel(post));
        }

        // PUT: api/BlogPost/5
        [HttpPut("{id}")]
        public IActionResult PutBlogPost(int id, BlogPostViewModel blogPost)
        {
            if (blogPost == null)
            {
                logger.LogDebug("Received a null content");
                return BadRequest();
            }

            if (id != blogPost.PostId)
            {
                logger.LogDebug("Body PostId mismatch. Expected {Expected}, Received {Received}", id, blogPost.PostId);
                return BadRequest("Body PostId mismatch");
            }

            var category = categoryService.Get(blogPost.CategoryId);

            if (category == null)
            {
                logger.LogDebug("Category {Id} not found", blogPost.CategoryId);
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
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BlogPostExists(id))
                {
                    logger.LogDebug("Blog post id {Id} not found", id);
                    return NotFound();
                }
                else
                {
                    logger.LogError(ex, "Unexpected exception when try to update Blog post id {Id}", id);
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/BlogPost/5
        [HttpDelete("{id}")]
        public ActionResult<BlogPostViewModel> DeleteBlogPost(int id)
        {
            var blogPost = postService.Get(id);
            if (blogPost == null)
            {
                logger.LogDebug("Blog post id {Id} not found", id);
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
