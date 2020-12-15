using Bocatasion.Backoffice.Models.Food;
using Bocatasion.Backoffice.Services.Food;
using Bocatasion.Backoffice.Testing.Fakes;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Bocatasion.Backoffice.Testing
{
    [ExcludeFromCodeCoverage]
    public class FoodServiceTests
    {
        [Fact]
        public async void GetAllSandwiches_Success()
        {
            // Arrange
            List<SandwichModel> target = FoodBuilder.BuildValidSandwichModelList();

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

        [Fact]
        public async void GetSandwich_Success()
        {
            // Arrange
            int targetId = GeneralBuilder.BuildRandomInt();
            var model = FoodBuilder.BuildValidSandwichModel(id: targetId);

            string content = JsonSerializer.Serialize(model);

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
            SandwichModel result = await service.GetSandwich(targetId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(targetId);
            result.Should().BeAssignableTo<SandwichModel>();
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async void DeleteSandwich_Success()
        {
            // Arrange
            int target = GeneralBuilder.BuildRandomInt();

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
            var result = await service.DeleteSandwich(target);

            // Assert
            result.Should().BeTrue();
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete),
               ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async void CreateSandwich_Success()
        {
            // Arrange
            SandwichCreatableDto target = FoodBuilder.BuildValidSandwichCreatableDto();
            SandwichModel model = FoodBuilder.BuildValidSandwichModel(name: target.Name);

            string content = JsonSerializer.Serialize(model);

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
            var result = await service.CreateSandwich(target);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<SandwichModel>();
            result.Name.Should().Be(target.Name);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
               ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async void UpdateSandwich_Success()
        {
            // Arrange
            SandwichModel model = FoodBuilder.BuildValidSandwichModel();

            string content = JsonSerializer.Serialize(model);

            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
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
            var result = await service.UpdateSandwich(model);

            // Assert
            result.Should().BeTrue();
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put),
               ItExpr.IsAny<CancellationToken>());
        }
    }
}
