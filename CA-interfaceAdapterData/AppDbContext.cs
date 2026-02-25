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
        public DbSet<SaleModel> Sales { get; set; }
        public DbSet<ConceptModel> Concepts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de las tablas para cada entidad
            modelBuilder.Entity<BeerModel>().ToTable("Beers");
            modelBuilder.Entity<UserModel>().ToTable("Users");
            modelBuilder.Entity<SaleModel>().ToTable("Sales");
            modelBuilder.Entity<ConceptModel>().ToTable("Concepts");


            // Configuración de la relación entre SaleModel y ConceptModel con Fluent API
            modelBuilder.Entity<SaleModel>()
                .HasMany(c => c.Concepts)
                .WithOne()
                .HasForeignKey(c => c.IdSale)
                .OnDelete( DeleteBehavior.Cascade);

        }
    }
}
