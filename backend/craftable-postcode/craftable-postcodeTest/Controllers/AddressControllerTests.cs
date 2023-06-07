using System;
using System.Threading.Tasks;
using craftable_postcode.Controllers;
using craftable_postcode.Data;
using craftable_postcode.Models;
using craftable_postcode.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace craftable_postcode.Tests.Controllers
{
    public class AddressControllerTests
    {
        private readonly AddressController _addressController;
        private readonly Mock<IAddressApiDbContext> _dbContextMock;
        private readonly Mock<IAddressServices> _addressServiceMock;

        public AddressControllerTests()
        {
            _dbContextMock = new Mock<IAddressApiDbContext>();
            _addressServiceMock = new Mock<IAddressServices>();
            _addressController = new AddressController(_dbContextMock.Object, _addressServiceMock.Object);
        }

        [Fact]
        public async Task GetAddressSaved_ValidPostCode_ReturnsOkResult()
        {
            // Arrange
            string postCode = "12345";
            var mockAddress = new Address { Title = "Test Title", Content = "Test Content", Description = "Test Description" };
            var addressResult = new AddressResult
            {
                Status = 200,
                Result = new AddressDeets
                {
                    AdminCounty = "London"
                }
            };

            _addressServiceMock.Setup(service => service.GetAddress(postCode)).ReturnsAsync(addressResult);
            _addressServiceMock.Setup(service => service.GetLisAddress()).ReturnsAsync(new List<Address>() { mockAddress });

            // Act
            var result = await _addressController.GetAddressSaved(postCode);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var addresses = Assert.IsAssignableFrom<IList<Address>>(okResult.Value);
            Assert.Single(addresses); // Assuming only one address is returned
        }

        [Fact]
        public async Task GetAddressSaved_NullPostCode_ReturnsBadRequest()
        {
            // Arrange
            string postCode = null;

            // Act
            var result = await _addressController.GetAddressSaved(postCode);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task AddAddress_ValidRequest_ReturnsOkResultWithAddress()
        {
            // Arrange
            var addAddressRequest = new AddAddressRequest { /* initialize request properties */ };
            var expectedAddress = new Address {  Title = "Test Title", Content = "Test Content", Description = "Test Description" };
            _addressServiceMock.Setup(service => service.AddChanges(It.IsAny<Address>())).Returns(Task.CompletedTask);

            // Act
            var result = await _addressController.AddAddress(addAddressRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var address = Assert.IsAssignableFrom<Address>(okResult.Value);

            Guid guidResult;
            Assert.True(Guid.TryParse(address.Id.ToString(), out guidResult));

            // Assert other properties
        }
    }
}