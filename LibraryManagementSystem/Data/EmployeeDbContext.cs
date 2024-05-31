using Microsoft.EntityFrameworkCore;
using Library.Models.Domain;

namespace Library.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Book> Book { get; set; }
    }
}
