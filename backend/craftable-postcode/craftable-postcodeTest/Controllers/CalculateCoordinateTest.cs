using System;
using GeoCoordinatePortable;
using craftable_postcode.Services;
using Xunit;

namespace craftable_postcode.Tests.Services
{
    public class CalculateCoordinateTests
    {
        private readonly CalculateCoordinate _calculateCoordinate;

        public CalculateCoordinateTests()
        {
            _calculateCoordinate = new CalculateCoordinate();
        }

        [Fact]
        public void Calculate_ValidCoordinates_ReturnsSameDistance()
        {
            // Arrange
            double latitudeRemote = 51.4700223;
            double longitudeRemote = -0.4542955;

            // Act
            var result = _calculateCoordinate.Calculate(latitudeRemote, longitudeRemote);

            // Assert
            Assert.Equal(result,0);
        }

        [Fact]
        public void Calculate_ValidCoordinates_ReturnsDistance()
        {
            // Arrange
            double latitudeRemote = 51.560414;
            double longitudeRemote = -0.116805;

            // Act
            var result = _calculateCoordinate.Calculate(latitudeRemote, longitudeRemote);

            // Assert
            Assert.True(result > 0);
        }

        [Fact]
        public void Calculate_NullCoordinates_ReturnsZeroDistance()
        {
            // Arrange
            double? latitudeRemote = null;
            double? longitudeRemote = null;

            // Act
            var result = _calculateCoordinate.Calculate(latitudeRemote, longitudeRemote);

            // Assert
            Assert.Equal(0.0, result);
        }

        [Fact]
        public void GetDistanceInKM_ValidDistance_ReturnsDistanceInKM()
        {
            // Arrange
            double distance = 5000;

            // Act
            var result = _calculateCoordinate.GetDistanceInKM(distance);

            // Assert
            Assert.Equal(5.0, result);
        }

        [Fact]
        public void GetDistanceInMiles_ValidDistance_ReturnsDistanceInMiles()
        {
            // Arrange
            double distance = 5000;

            // Act
            var result = _calculateCoordinate.GetDistanceInMiles(distance);

            // Assert
            Assert.Equal(3.11, result, 2);
        }
    }
}