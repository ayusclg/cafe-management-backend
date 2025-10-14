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
        public DbSet<UserModel> Users { get; set; }
        public DbSet<MenuModel>Menus{ get; set; }
    }
    
}