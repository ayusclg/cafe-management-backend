

using Microsoft.EntityFrameworkCore;
using backend_01.Core.Models;
namespace backend_01.Infrastructure.Data {


    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    }
}