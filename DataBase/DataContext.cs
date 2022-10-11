using Microsoft.EntityFrameworkCore;

namespace TwoPoi.DataBase;

internal class DataContext : DbContext
{
    public DbSet<PointOfInterest> PointsOfInterest { get; set; }

    public DataContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "PointsOfInterest.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}
