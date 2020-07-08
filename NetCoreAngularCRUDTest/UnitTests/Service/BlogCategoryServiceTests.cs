using Moq;
using NetCoreAngularCRUDApp.Data;
using NetCoreAngularCRUDApp.Models;
using NetCoreAngularCRUDApp.Service;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NetCoreAngularCRUDTest.UnitTests.Service
{
    public class BlogCategoryServiceTests
    {
        [Fact]
        public void BlogCategory_WhenGetAll_ReturnResult()
        {
            var responseMock = new List<BlogCategory>();
            responseMock.Add(new BlogCategory { Name = "Category Alpha" });
            responseMock.Add(new BlogCategory { Name = "Category Beta" });
            responseMock.Add(new BlogCategory { Name = "Category Gamma" });

            var persistenceMock = new Mock<IBlogCategoryRepository>();
            persistenceMock.Setup(p => p.GetAll()).Returns(responseMock);

            var service = new BlogCategoryService(persistenceMock.Object);
            var categories = service.GetAll();

            Assert.NotNull(categories);
            Assert.Equal(3, categories.ToList().Count);
        }
    }
}
