using KelleSolutions.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KelleSolutions.Data
{
    public class KelleSolutionsDbContext : IdentityDbContext<User>
    {
        public KelleSolutionsDbContext(DbContextOptions<KelleSolutionsDbContext> options)
            : base(options)
        {
        }

    }
}
