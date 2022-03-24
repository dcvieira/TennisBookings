using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.IntegrationTests.Extensions
{
    public static class WebApplicationFactoryExtensions
    {
        public static HttpClient CreateClientWithMemberAndDbSetup(this WebApplicationFactory<Startup> factory,
            Action<TennisBookingDbContext> configure)
        {
            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.WithMemberUser().ConfigureTestDatabase(db =>
                {
                    configure(db);
                });
            }).CreateClient();


            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            return client;
        }
    }

}
