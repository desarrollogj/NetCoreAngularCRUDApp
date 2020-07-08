using System;

namespace NetCoreAngularCRUDApp.Models.ViewModels
{
    public class BlogPostViewModel
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Creator { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Dt { get; set; }

        public BlogPostViewModel()
        {
        }

        public BlogPostViewModel(BlogPost blogPost)
        {
            this.PostId = blogPost.PostId;
            this.CategoryId = blogPost.Category.CategoryId;
            this.CategoryName = blogPost.Category.Name;
            this.Creator = blogPost.Creator;
            this.Title = blogPost.Title;
            this.Body = blogPost.Body;
            this.Dt = blogPost.Dt;
        }
    }
}
