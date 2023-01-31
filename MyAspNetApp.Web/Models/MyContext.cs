using Microsoft.EntityFrameworkCore;

namespace MyAspNetApp.Web.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
