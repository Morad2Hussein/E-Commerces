
namespace Domain.Entities.OrderModule
{
    public class Address
    {
        public Address()
        {
            
        }
        public Address(string firstName, string lastName, string country, string street, string city)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Street = street;
            City = city;
        }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
}
