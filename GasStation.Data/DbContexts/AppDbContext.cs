using GasStation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using GasStation.Domain.Enums;

namespace GasStation.Data.DbContexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Station> Stations { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FullName = "Oybek Muxtaraliyev",
                Email = "oybekmuxtaraliyev@gmail.com",
                Password = "123456", // Will be change to Hashed password
                IsVerified = true,
                CarNumber = "60 X 856 OM",
                Role = UserRole.Admin,
                X = 0,
                Y = 0
            });
    }
}
