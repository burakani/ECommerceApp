namespace ECommerceApp.Tests.IntegrationTests
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NUnit.Framework;
    using Microsoft.AspNetCore.Mvc.Testing;
    using ECommerceApp.API;

    [TestFixture]
    public class UserEndpointsTest
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        [Test]
        public async Task Register_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var requestBody = new
            {
                Username = "integrationTestUser2",
                Password = "123456"
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/auth/register", content);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            responseString.Should().Contain("Kayıt başarılı");
        }
    }
}
