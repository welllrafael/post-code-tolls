using craftable_postcode.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace craftable_postcode.Respositories
{
    public interface IAddressRepository
    {
        Task<AddressResult> GetAddress(string url);
    }
    public class AddressRepository : IAddressRepository
    {
        private readonly HttpClient _httpClient;

        public AddressRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AddressResult> GetAddress(string postCode)
        {
            if (postCode == string.Empty)
                return null;

            var addressUrl = $"https://api.postcodes.io/postcodes/{postCode}";

            var response = await _httpClient.GetAsync(addressUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.StatusCode.ToString());

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AddressResult>(result);
        }
    }
}
