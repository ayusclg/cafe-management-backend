using backend_01.Core.Category.Model;
using backend_01.Core.Menu.Model;
using backend_01.Core.User.Model;
using Microsoft.EntityFrameworkCore;

namespace backend_01.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<StaffModel> Staffs { get; set; }
        public DbSet<MenuModel> Menus { get; set; }

        public DbSet<CategoryModel> Category{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StaffModel>().Property(O => O.Role).HasConversion<string>();
        }
    }
    
}