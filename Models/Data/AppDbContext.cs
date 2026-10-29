using Microsoft.EntityFrameworkCore;


namespace LibraryManagement.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Book> books { get; set; }
    }
}
