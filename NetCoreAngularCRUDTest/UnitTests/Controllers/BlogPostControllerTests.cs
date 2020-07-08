using Microsoft.AspNetCore.Mvc;
using Moq;
using NetCoreAngularCRUDApp.Controllers;
using NetCoreAngularCRUDApp.Models;
using NetCoreAngularCRUDApp.Models.ViewModels;
using NetCoreAngularCRUDApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreAngularCRUDTest.UnitTests.Controllers
{
    public class BlogPostControllerTests
    {
        [Fact]
        public void WhenGetBlogPosts_ReturnResult()
        {
            var responseMock = new List<BlogPost>();
            responseMock.Add(new BlogPost { PostId = 1, Category = new BlogCategory { CategoryId = 1, Name = "Category Alpha" }, Creator = "User One", Title = "My first post", Body = "Hello!", Dt = DateTime.Now });
            responseMock.Add(new BlogPost { PostId = 2, Category = new BlogCategory { CategoryId = 2, Name = "Category Beta" }, Creator = "User Two", Title = "Otra publicación", Body = "Probando el blog", Dt = DateTime.Now });

            var postServiceMock = new Mock<IBlogPostService>();
            postServiceMock.Setup(p => p.GetAll()).Returns(responseMock);
            var categoryServiceMock = new Mock<IBlogCategoryService>();

            var controller = new BlogPostController(postServiceMock.Object, categoryServiceMock.Object);
            var posts = controller.GetBlogPosts();

            Assert.NotNull(posts);
            Assert.Equal(2, posts.Count());
        }

        [Fact]
        public async void WhenGetBlogPost_ReturnResult()
        {
            var postMock = new BlogPost { PostId = 1, Category = new BlogCategory { CategoryId = 1, Name = "Category Alpha" }, Creator = "User One", Title = "My first post", Body = "Hello!", Dt = DateTime.Now };
            var postServiceMock = new Mock<IBlogPostService>();
            postServiceMock.Setup(p => p.GetAsync(1)).Returns(Task.FromResult(postMock));
            var categoryServiceMock = new Mock<IBlogCategoryService>();

            var controller = new BlogPostController(postServiceMock.Object, categoryServiceMock.Object);
            var post = await controller.GetBlogPost(1);

            Assert.NotNull(post);
            var actionResult = Assert.IsType<ActionResult<BlogPostViewModel>>(post);
            var returnedPost = Assert.IsType<BlogPostViewModel>(actionResult.Value);

            Assert.Equal(1, returnedPost.PostId);
            Assert.Equal("My first post", returnedPost.Title);
            Assert.Equal("Hello!", returnedPost.Body);
        }

        [Fact]
        public void GivenABlogPost_WhenPost_ReturnResult()
        {
            var postServiceMock = new Mock<IBlogPostService>();
            postServiceMock.Setup(p => p.Add(It.IsAny<BlogPost>()));

            var categoryMock = new BlogCategory { CategoryId = 1, Name = "Category Alpha" };
            var categoryServiceMock = new Mock<IBlogCategoryService>();
            categoryServiceMock.Setup(p => p.Get(1)).Returns(categoryMock);

            var postData = new BlogPostViewModel { CategoryId = 1, Creator = "User One", Title = "My first post", Body = "Hello!", Dt = DateTime.Now };

            var controller = new BlogPostController(postServiceMock.Object, categoryServiceMock.Object);
            var post = controller.PostBlogPost(postData);

            Assert.NotNull(post);
            var actionResult = Assert.IsType<ActionResult<BlogPostViewModel>>(post);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnedPost = Assert.IsType<BlogPostViewModel>(createdAtActionResult.Value);

            Assert.Equal("My first post", returnedPost.Title);
            Assert.Equal("Hello!", returnedPost.Body);
        }

        [Fact]
        public void GivenABlogPost_WhenPut_ReturnResult()
        {
            var postServiceMock = new Mock<IBlogPostService>();
            postServiceMock.Setup(p => p.Update(It.IsAny<BlogPost>()));

            var categoryMock = new BlogCategory { CategoryId = 1, Name = "Category Alpha" };
            var categoryServiceMock = new Mock<IBlogCategoryService>();
            categoryServiceMock.Setup(p => p.Get(1)).Returns(categoryMock);

            var putData = new BlogPostViewModel { PostId = 1, CategoryId = 1, Creator = "User One", Title = "My first post", Body = "Hello!", Dt = DateTime.Now };

            var controller = new BlogPostController(postServiceMock.Object, categoryServiceMock.Object);
            var result = controller.PutBlogPost(1, putData);

            Assert.NotNull(result);
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GivenABlogPost_WhenDelete_ReturnResult()
        {
            var deleteData = new BlogPost { PostId = 1, Category = new BlogCategory { CategoryId = 1, Name = "Category Alpha" }, Creator = "User One", Title = "My first post", Body = "Hello!", Dt = DateTime.Now };

            var postServiceMock = new Mock<IBlogPostService>();
            postServiceMock.Setup(p => p.Delete(It.IsAny<BlogPost>()));
            postServiceMock.Setup(p => p.Get(1)).Returns(deleteData);

            var categoryServiceMock = new Mock<IBlogCategoryService>();

            var controller = new BlogPostController(postServiceMock.Object, categoryServiceMock.Object);
            var result = controller.DeleteBlogPost(1);

            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<BlogPostViewModel>>(result);
            var returnedDelete = Assert.IsType<BlogPostViewModel>(actionResult.Value);

            Assert.Equal(1, returnedDelete.PostId);
            Assert.Equal("My first post", returnedDelete.Title);
            Assert.Equal("Hello!", returnedDelete.Body);
        }
    }
}
