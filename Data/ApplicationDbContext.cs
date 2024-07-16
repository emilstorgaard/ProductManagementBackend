using Microsoft.EntityFrameworkCore;
using ProductManagementBackend.Models.Entities;

namespace ProductManagementBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
