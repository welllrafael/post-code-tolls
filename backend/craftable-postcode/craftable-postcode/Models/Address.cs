namespace craftable_postcode.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string? PostCode { get; set; }
        public string? Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
    }
}
