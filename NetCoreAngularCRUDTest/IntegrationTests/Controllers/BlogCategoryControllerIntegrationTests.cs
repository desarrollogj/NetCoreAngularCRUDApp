using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using NetCoreAngularCRUDApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xunit;

namespace NetCoreAngularCRUDTest.IntegrationTests.Controllers
{
    public class BlogCategoryControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;
        public BlogCategoryControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async void WhenGetAll_ReturnResult()
        {
            var client = factory.CreateClient();

            var response = await client.GetAsync("/api/blogcategory");

            var responseString = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<IEnumerable<BlogCategory>>(responseString);
            Assert.Equal(3, categories.ToList().Count);
        }
    }
}
