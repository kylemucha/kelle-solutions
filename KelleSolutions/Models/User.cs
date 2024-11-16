using Microsoft.AspNetCore.Identity;

namespace KelleSolutions.Models
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Affiliation { get; set; }
        public required string LicenseNumber { get; set; }

    }
}
