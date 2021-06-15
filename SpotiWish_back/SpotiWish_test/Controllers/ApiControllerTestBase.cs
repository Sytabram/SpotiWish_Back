using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SpotiWish_back;
using SpotiWish_back.Data;
using SpotiWish_back.Model;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SpotiWish_test.Controllers
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SpotiWishDataContext>));
                if (descriptor == null) return;
                services.Remove(descriptor);

                services.AddDbContext<SpotiWishDataContext>(options =>
                {
                    options.UseInMemoryDatabase("SpotiWish_back");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<SpotiWishDataContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        ResetInMemoryDatabase(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, ex.Message);
                    }
                }
            });
        }

        private static void ResetInMemoryDatabase(SpotiWishDataContext db)
        {
            db.RemoveRange(db.Users);
            db.SaveChanges();
            db.AddRange(new List<User>
            {
                new User { Id = 1, UserName = "User1"},
                new User { Id = 2, UserName = "User2"},
                new User { Id = 3, UserName = "User3"},
            });
            db.SaveChanges();
        }
    }

    public class ApiControllerTestBase : CustomWebApplicationFactory<Startup>
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;
        
        [TestInitialize]
        public void SetupTest()
        {
            _factory = new CustomWebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }

        protected async Task<HttpResponseMessage> GetAsync(string url) 
        {
            return await _client.GetAsync(url);
        } 

        protected async Task<T> GetAsync<T>(string url) 
        {
            var response = await _client.GetAsync(url);

            var body = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(body);
        }

        protected async Task<HttpResponseMessage> PostBasicAsync<T>(string url, T body) 
        {
            return await _client.PostAsJsonAsync(url, body);
        }

        protected async Task<HttpResponseMessage> PostFileAsync(string url, byte[] file) 
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(file), "file", "filename");
            return await _client.PostAsync(url, content);
        }

        protected async Task<T> PostAsync<T>(string url, T body) 
        {
            return await PostAsync<T, T>(url, body);
        }

        protected async Task<U> PostAsync<T, U>(string url, T body) 
        {
            var response = await _client.PostAsJsonAsync(url, body);

            return await response.Content.ReadFromJsonAsync<U>();
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string url) 
        {
            var response = await _client.DeleteAsync(url);

            return response.EnsureSuccessStatusCode();
        } 
    }

    public partial class RfcError
    {
        public Uri Type { get; set; }

        public string Title { get; set; }

        public long Status { get; set; }

        public string TraceId { get; set; }

        public Error Errors { get; set; }
    }

    public partial class Error
    {
        public string[] Name { get; set; }
    }
}