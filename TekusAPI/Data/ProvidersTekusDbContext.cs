using Microsoft.EntityFrameworkCore;
using TekusAPI.Models;

namespace TekusAPI.Data
{
    public class ProvidersTekusDbContext : DbContext
    {
        public ProvidersTekusDbContext(DbContextOptions<ProvidersTekusDbContext> options) : base(options) { }

        public DbSet<ProvidersTekus> ProvidersTekus { get; set; }
        public DbSet<ServicesProvider> ServicesProvider { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<CustomField> CustomFields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<ServicesProvider>()
            .HasMany(sp => sp.Countries)
            .WithMany(c => c.Services)
            .UsingEntity<Dictionary<string, object>>(
                "ServiceCountry",
                j => j
                    .HasOne<Country>()
                    .WithMany()
                    .HasForeignKey("CountryId"),
                j => j
                    .HasOne<ServicesProvider>()
                    .WithMany()
                    .HasForeignKey("ServiceId")
            );

            modelBuilder.Entity<ServicesProvider>()
                .Property(sp => sp.HourlyRate)
                .HasColumnType("decimal(18,2)");

            
            modelBuilder.Entity<Country>()
                .Property(c => c.CommonName)
                .IsRequired();

            modelBuilder.Entity<Country>()
                .Property(c => c.OfficialName)
                .IsRequired(); 


        }


    }
}
