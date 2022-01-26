using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions options ): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData(
                new Country { Id = 1, ShortName = "BD", Name = "Bangladesh", CountryCode = 880, GMTOffset = 360 },
               new Country { Id = 2, ShortName = "IND", Name = "India", CountryCode = 91, GMTOffset = 330 }
                );
            builder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Address="CoxsBazar", CountryId= 1, Name="Hotel Cox Today",Rating= 4.7},
                new Hotel { Id = 2, Address = "Rajasthan", CountryId = 2, Name = "Jodha Akbar", Rating = 4.5 }
                );
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
