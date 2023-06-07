using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using craftable_postcode.Data;
using craftable_postcode.Models;
using craftable_postcode.Respositories;
using craftable_postcode.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace craftable_postcode.Tests.Services
{
    public class AddressServicesTests
    {
        private readonly AddressServices _addressServices;
        private readonly Mock<IAddressRepository> _addressRepoMock;
        private readonly Mock<IAddressApiDbContext> _dbContextMock;
        private readonly Mock<ICalculateCoordinate> _calculateMock;

        public AddressServicesTests()
        {
            _addressRepoMock = new Mock<IAddressRepository>();
            _dbContextMock = new Mock<IAddressApiDbContext>();
            _calculateMock = new Mock<ICalculateCoordinate>();
            _addressServices = new AddressServices(_addressRepoMock.Object, _dbContextMock.Object, _calculateMock.Object);
        }

        [Fact]
        public async Task GetAddress_ValidPostCode_ReturnsAddressResult()
        {
            // Arrange
            string postCode = "12345";
            var mockAddressResult = new AddressResult { /* initialize mock address result properties */ };
            _addressRepoMock.Setup(repo => repo.GetAddress(postCode)).ReturnsAsync(mockAddressResult);

            // Act
            var result = await _addressServices.GetAddress(postCode);

            // Assert
            Assert.Equal(mockAddressResult, result);
        }

        [Fact]
        public async Task GetAddress_NullPostCode_ThrowsArgumentNullException()
        {
            // Arrange
            string postCode = null;

            // Act + Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _addressServices.GetAddress(postCode));
        }

        [Fact]
        public async Task AddAddress_ValidParameters_AddsAddressToDbContext()
        {
            // Arrange
            var mockAddressResult = new AddressResult { Status= 200,Result= new AddressDeets { Latitude = 51.560414 , Longitude = -0.116805 }  };
            var mockAddress = new Address { /* initialize mock address properties */ };
            var distance = 10; // example distance value
            _calculateMock.Setup(calc => calc.Calculate(mockAddressResult.Result.Latitude, mockAddressResult.Result.Longitude))
                .Returns(distance);
            _calculateMock.Setup(calc => calc.GetDistanceInKM(distance)).Returns(5); // example distance in km
            _calculateMock.Setup(calc => calc.GetDistanceInMiles(distance)).Returns(3); // example distance in miles

            // Act
            await _addressServices.AddAddress(mockAddressResult, "12345");

            // Assert
            _dbContextMock.Verify(db => db.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetLisAddress_NoConditions_ReturnsListOfAddresses()
        {
            // Arrange
            var mockAddresses = new List<Address> { /* initialize mock addresses */ };
            _dbContextMock.Setup(db => db.ToListAsync()).ReturnsAsync(mockAddresses);

            // Act
            var result = await _addressServices.GetLisAddress();

            // Assert
            Assert.Equal(mockAddresses, result);
        }

        [Fact]
        public async Task AddChanges_ValidAddress_AddsAddressToDbContext()
        {
            // Arrange
            var mockAddress = new Address { /* initialize mock address properties */ };

            // Act
            await _addressServices.AddChanges(mockAddress);

            // Assert
            _dbContextMock.Verify(db => db.AddAsync(mockAddress), Times.Once);
            _dbContextMock.Verify(db => db.SaveChangesAsync(), Times.Once);
        }
    }
}