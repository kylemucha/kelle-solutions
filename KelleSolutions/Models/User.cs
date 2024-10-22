using Microsoft.AspNetCore.Identity;

namespace KelleSolutions.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Affiliate { get; set; }
    }
}
