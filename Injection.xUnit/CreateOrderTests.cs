using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Injection.API.Controllers;
using Injection.Services;
using Xunit;
using Injection.Services.Interface;

namespace Injection.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _orderServiceMock;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _controller = new OrderController(_orderServiceMock.Object);

            // Set up user claims for the User property
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.SerialNumber, "AB9AFB21-0BCE-4D63-8855-38A180A054E3"),
                        new Claim(ClaimTypes.Role, "admin")
                    }, "mock"))
                }
            };
        }

        [Fact]
        public async Task CreateOrder_AdminRole_Success()
        {
            // Arrange
            var orderRequest = new OrderRequest { /* Set your order request properties here */ };
            _orderServiceMock.Setup(x => x.CreateOrder(orderRequest, It.IsAny<string>()))
                             .ReturnsAsync((true, "Order created successfully"));

            // Act
            var result = await _controller.CreateOrder(orderRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseData = Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(200, responseData.StatusCode);
        }

        [Fact]
        public async Task CreateOrder_NonAdminRole_Unauthorized()
        {
            // Arrange
            var orderRequest = new OrderRequest { /* Set your order request properties here */ };

            var userClaims = new[]
            {
                new Claim(ClaimTypes.SerialNumber, "AB9AFB21-0BCE-4D63-8855-38A180A054E3"),
                new Claim(ClaimTypes.Role, "user") // Change the role here as needed
            };

            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userPrincipal
                }
            };

            // Act
            var result = await _controller.CreateOrder(orderRequest);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var responseData = Assert.IsType<UnauthorizedObjectResult>(unauthorizedResult);
            Assert.Equal(401, responseData.StatusCode);
        }

        [Fact]
        public async Task CreateOrder_ServiceFailure_BadRequest()
        {
            // Arrange
            var orderRequest = new OrderRequest { /* Set your order request properties here */ };
            _orderServiceMock.Setup(x => x.CreateOrder(orderRequest, It.IsAny<string>()))
                             .ReturnsAsync((false, "Failed to create order"));

            // Act
            var result = await _controller.CreateOrder(orderRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var responseData = Assert.IsType<BadRequestObjectResult>(badRequestResult);
            Assert.Equal(400, responseData.StatusCode);
        }

        [Fact]
        public async Task CreateOrder_Exception_InternalServerError()
        {
            // Arrange
            var orderRequest = new OrderRequest { /* Set your order request properties here */ };
            _orderServiceMock.Setup(x => x.CreateOrder(orderRequest, It.IsAny<string>()))
                             .ThrowsAsync(new Exception("Something went wrong"));

            // Act
            var result = await _controller.CreateOrder(orderRequest);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            // You can further check the response content if needed
        }
    }
    public class ResponseData
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}