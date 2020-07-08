using Moq;
using NetCoreAngularCRUDApp.Controllers;
using NetCoreAngularCRUDApp.Models;
using NetCoreAngularCRUDApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NetCoreAngularCRUDTest.UnitTests.Controllers
{
    public class BlogCategoryControllerTests
    {
        [Fact]
        public void WhenGetAll_ReturnResult()
        {
            var responseMock = new List<BlogCategory>();
            responseMock.Add(new BlogCategory { Name = "Category Alpha" });
            responseMock.Add(new BlogCategory { Name = "Category Beta" });
            responseMock.Add(new BlogCategory { Name = "Category Gamma" });

            var serviceMock = new Mock<IBlogCategoryService>();
            serviceMock.Setup(p => p.GetAll()).Returns(responseMock);

            var controller = new BlogCategoryController(serviceMock.Object);

            var categories = controller.GetBlogCategories();

            Assert.NotNull(categories);
            Assert.Equal(3, categories.Count());
        }
    }
}
