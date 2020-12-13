using Bocatasion.Backoffice.Models.Food;
using Bocatasion.Backoffice.Services.Food;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Bocatasion.Backoffice.Testing
{
    public class FoodServiceTests
    {
        [Fact]
        public async void GetAllSandwiches_Success()
        {
            // Arrange
            var target = new List<SandwichModel>
            {
                new SandwichModel
                {
                    Id = 1,
                    Description = "description",
                    ImageUrl = "imageUrl",
                    Disabled = false,
                    Name = "name",
                    Price = 2
                }
            };

            string content = JsonSerializer.Serialize(target);

            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content)
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new System.Uri("http://www.example.com")
            };

            var service = new FoodService(httpClient);

            // Act
            var result = await service.GetAllSandwiches();

            // Assert
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Should().BeAssignableTo<List<SandwichModel>>();
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
        }
    }
}
