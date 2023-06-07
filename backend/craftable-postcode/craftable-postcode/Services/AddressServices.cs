using craftable_postcode.Data;
using craftable_postcode.Models;
using craftable_postcode.Respositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace craftable_postcode.Services
{
    public interface IAddressServices
    {
        Task<AddressResult> GetAddress(string postCode);
        Task AddAddress(AddressResult address, string postCode);
        Task<IList<Address>> GetLisAddress();
        Task AddChanges(Address address);
    }

    public class AddressServices : IAddressServices
    {
        private readonly IAddressRepository _addressRepo;
        private readonly IAddressApiDbContext _dbContext;
        private readonly ICalculateCoordinate _calculate;

        public AddressServices(IAddressRepository addressRepo, IAddressApiDbContext dbContext, ICalculateCoordinate calculate)
        {
            this._addressRepo = addressRepo;
            this._dbContext = dbContext;
            this._calculate = calculate;
        }

        public async Task<AddressResult> GetAddress(string postCode)
        {
            if (postCode == null || postCode.Length < 3) { throw new ArgumentNullException("postCode"); } //TO DO - Business Rules

            var address = await this._addressRepo.GetAddress(postCode);

            return address;
        }

        public async Task AddAddress(AddressResult address, string postCode)
        {
            if (address == null) { new ArgumentNullException("address"); } //TO DO - Business Rules

            var distance = this._calculate.Calculate(address?.Result?.Latitude, address?.Result?.Longitude);

            var newAddress = new Address()
            {
                Id = Guid.NewGuid(),
                Title = $"{address?.Result?.Ccg}",
                Content = $"PostCode:{postCode}-{address?.Result?.AdminDistrict} | {address?.Result?.Region} - Latitude: {(address?.Result?.Latitude)}, Longitude: {(address?.Result?.Longitude)}",
                Description = $"{address?.Result?.Country} - distance in a straight line to London Heathrow airport " +
                $"(lat/long: 51.4700223,-0.4542955) is: {this._calculate.GetDistanceInKM(distance)} km - {this._calculate.GetDistanceInMiles(distance)} miles",
                Latitude = (double)(address?.Result?.Latitude),
                Longitude = (double)(address?.Result?.Longitude),
                PostCode = "",
            };

            await this.AddChanges(newAddress);

        }

        public async Task<IList<Address>> GetLisAddress()
        {
            return await _dbContext.ToListAsync();
        }

        public async Task AddChanges(Address address)
        {
            await _dbContext.AddAsync(address);
            await _dbContext.SaveChangesAsync();
        }

    }
}