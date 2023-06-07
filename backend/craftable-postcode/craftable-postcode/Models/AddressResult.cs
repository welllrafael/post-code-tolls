using Newtonsoft.Json;

namespace craftable_postcode.Models
{
    public class AddressResult
    {
        public int Status { get; set; }
        public AddressDeets? Result { get; set; }
    }

    public class AddressDeets
    {
        public string? Postcode { get; set; }
        public int Quality { get; set; }
        public int Eastings { get; set; }
        public int Northings { get; set; }
        public string? Country { get; set; }
        public string? NhsHa { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? EuropeanElectoralRegion { get; set; }
        public string? PrimaryCareTrust { get; set; }
        public string? Region { get; set; }
        public string? Lsoa { get; set; }
        public string? Msoa { get; set; }
        public string? Incode { get; set; }
        public string? Outcode { get; set; }
        public string? ParliamentaryConstituency { get; set; }
        [JsonProperty("admin_district")]
        public string? AdminDistrict { get; set; }
        public string? Parish { get; set; }
        public string? AdminCounty { get; set; }
        public string? DateOfIntroduction { get; set; }
        public string? AdminWard { get; set; }
        public string? Ced { get; set; }
        public string? Ccg { get; set; }
        public string? Nuts { get; set; }
        public string? Pfa { get; set; }
        public Codes? Codes { get; set; }
    }

    public class Codes
    {
        public string? AdminDistrict { get; set; }
        public string? AdminCounty { get; set; }
        public string? AdminWard { get; set; }
        public string? Parish { get; set; }
        [JsonProperty("parliamentary_constituency")]
        public string? ParliamentaryConstituency { get; set; }
        public string? Ccg { get; set; }
        public string? CcgId { get; set; }
        public string? Ced { get; set; }
        public string? Nuts { get; set; }
        public string? Lsoa { get; set; }
        public string? Msoa { get; set; }
        public string? Lau2 { get; set; }
        public string? Pfa { get; set; }
    }
}
