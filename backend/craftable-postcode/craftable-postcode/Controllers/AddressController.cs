using craftable_postcode.Data;
using craftable_postcode.Models;
using craftable_postcode.Respositories;
using craftable_postcode.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace craftable_postcode.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly IAddressApiDbContext _dbContext;
        private readonly IAddressServices _addressService;


        public AddressController(IAddressApiDbContext dbContext, IAddressServices addressService)
        {
            _dbContext = dbContext;
            _addressService = addressService;
        }

        [HttpGet]
        [Route("{postCode}")]
        public async Task<IActionResult> GetAddressSaved(string postCode)
        {
            if (postCode == null)
                return BadRequest();

            var address = await this._addressService.GetAddress(postCode);

            this._addressService.AddAddress(address, postCode);

            return base.Ok(await this._addressService.GetLisAddress());
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(AddAddressRequest addAddressRequest)
        {
            var address = new Address()
            {
                Id = Guid.NewGuid(),
                Title = addAddressRequest.Title,
                Content = addAddressRequest.Content,
                Description = addAddressRequest.Description
            };
            await this._addressService.AddChanges(address);
            return Ok(address);
        }
    }
}