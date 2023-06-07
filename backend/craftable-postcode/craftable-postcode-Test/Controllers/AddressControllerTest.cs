using craftable_postcode.Controllers;
using craftable_postcode.Data;
using craftable_postcode.Models;
using craftable_postcode.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace craftable_postcode_Test.Controllers
{
    public class AddressControllerTest
    {
        private AddressController _sut;
        private Mock<AddressApiDbContext> _dbContextMock = new();
        private Mock<IAddressServices> _addressServiceMock = new();

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<AddressApiDbContext>();
            _addressServiceMock = new Mock<IAddressServices>();
            _sut = new AddressController(_dbContextMock.Object, _addressServiceMock.Object);
        }

        [Test]
        public async Task GetAddressSaved_ValidPostCode_ReturnsOkResult()
        {
            // Arrange
            string postCode = "12345";
            var address = new Address { Id = Guid.NewGuid(), Title = "Test Title", Content = "Test Content", Description = "Test Description" };

            _addressServiceMock
                .Setup(x => x.GetAddress(postCode))
                .ReturnsAsync(address);
            _dbContextMock
                .Setup(x => x.Addresses.ToListAsync())
                .ReturnsAsync(new List<Address>());

            // Act
            var result = await _sut.GetAddressSaved(postCode);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetAddressSaved_NullPostCode_ReturnsBadRequest()
        {
            // Arrange
            string postCode = null;

            // Act
            var result = await _sut.GetAddressSaved(postCode);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task AddAddress_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var addAddressRequest = new AddAddressRequest { Title = "Test Title", Content = "Test Content", Description = "Test Description" };

            // Act
            var result = await _sut.AddAddress(addAddressRequest);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
