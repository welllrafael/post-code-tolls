using craftable_postcode.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

namespace craftable_postcode.Data
{
    public interface IAddressApiDbContext
    {
        public DbSet<Address> Addresses { get; set; }
        Task<int> SaveChangesAsync();
        Task AddAsync(Address address);
        Task<IList<Address>> ToListAsync();
    }
    public class AddressApiDbContext : DbContext, IAddressApiDbContext
    {
        public AddressApiDbContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<Address> Addresses { get; set; }

        public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();

        public async Task AddAsync(Address address) => await base.AddAsync(address);

        public async Task<IList<Address>> ToListAsync() => await Addresses.OrderByDescending(x=>x.Id).Take(3).ToListAsync();
    }
}
