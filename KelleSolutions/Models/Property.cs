namespace KelleSolutions.Models
{
    public class Property
    {
        public int Id { get; set; } // Primary key
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public DateTime DateListed { get; set; } = DateTime.Now;
    }
}
