using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreAngularCRUDApp.Data;
using NetCoreAngularCRUDApp.Models;
using NetCoreAngularCRUDApp.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreAngularCRUDTest.IntegrationTests.Controllers
{
    public class BlogPostControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public BlogPostControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async void WhenGetAll_ReturnResult()
        {
            var client = CreateClient();

            var response = await client.GetAsync("/api/blogpost");

            var responseString = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<IEnumerable<BlogPost>>(responseString);
            Assert.Equal(2, posts.ToList().Count);
        }

        [Fact]
        public async void WhenGet_ReturnResult()
        {
            var client = CreateClient();

            var response = await client.GetAsync("/api/blogpost/2");

            var responseString = await response.Content.ReadAsStringAsync();
            var post = JsonConvert.DeserializeObject<BlogPost>(responseString);
            Assert.Equal(2, post.PostId);
            Assert.Equal("Otra publicación", post.Title);
        }

        [Fact]
        public async void WhenGet_AndNotExists_ReturnNotFound()
        {
            var client = CreateClient();

            var response = await client.GetAsync("/api/blogpost/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void WhenPost_ReturnCreated()
        {
            var client = CreateClient();
            
            var blogPost = new BlogPostViewModel { CategoryId = 1, Creator = "User Test", Title = "Test Post", Body = "This is a test", Dt = DateTime.Now };
            var jsonData = JsonConvert.SerializeObject(blogPost);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);  
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("/api/blogpost", byteContent);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async void WhenPut_ReturnNoContent()
        {
            var client = CreateClient();

            var blogPost = new BlogPostViewModel { PostId = 1, CategoryId = 1, Creator = "User Test", Title = "Test PUT", Body = "This is a test", Dt = DateTime.Now };
            var jsonData = JsonConvert.SerializeObject(blogPost);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync("/api/blogpost/1", byteContent);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async void WhenDelete_ReturnNoContent()
        {
            var client = CreateClient();

            var response = await client.DeleteAsync("/api/blogpost/1");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private HttpClient CreateClient() {
            return factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var serviceProvider = services.BuildServiceProvider();

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<NetCoreAngularCRUDAppContext>();
                        var logger = scopedServices.GetRequiredService<ILogger<BlogPostControllerIntegrationTests>>();

                        try
                        {
                            DBSeeder.ReinitializeDbForTests(db);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
                        }
                    }
                });
            })
           .CreateClient();
        }
    }
}
