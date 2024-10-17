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

        // Add DbSets for our entities, for example:
        // public DbSet<User> Users { get; set; }
        // public DbSet<Properties> Properties { get; set; }
    }
}
