using CA_InterfaceAdapters_Models;
using CL_EnterpriseLayer;
using Microsoft.EntityFrameworkCore;

namespace CA_interfaceAdapterData
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }
 
        public DbSet<BeerModel> Beers { get; set; }
        public DbSet<UserModel> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            modelBuilder.Entity<BeerModel>().ToTable("Beers");
            modelBuilder.Entity<UserModel>().ToTable("Users");
        }
    }
}
