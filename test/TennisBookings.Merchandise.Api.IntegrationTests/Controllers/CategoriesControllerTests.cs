using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TennisBookings.Merchandise.Api.IntegrationTests.Models;
using TennisBookings.Merchandise.Api.IntegrationTests.TestHelpers;
using Xunit;

namespace TennisBookings.Merchandise.Api.IntegrationTests.Controllers
{
    public class CategoriesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CategoriesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateDefaultClient(new Uri("http://localhost/api/categories"));
        }


        [Fact]
        public async Task GetAll_ReturnsExpectedResponse()
        {
            var expected = new List<string> { "Accessories", "Bags", "Balls", "Clothing", "Rackets" };

            var model = await _client.GetFromJsonAsync<ExpectedCategoriesModel>("");

            Assert.NotNull(model?.AllowedCategories);
            Assert.Equal(expected.OrderBy(s => s), model.AllowedCategories.OrderBy(s => s));

        }

        [Fact]
        public async Task GetAll_ExpectedCacheControlHeader()
        {
            var response = await _client.GetAsync("");

            var header = response.Headers.CacheControl;

            Assert.True(header.MaxAge.HasValue);
            Assert.Equal(TimeSpan.FromMinutes(5), header.MaxAge);
            Assert.True(header.Public);
        }
    }
}
