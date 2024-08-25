using Microsoft.EntityFrameworkCore;

namespace SignalRBlazorSSR.Data;

public class AppDbContext : DbContext
{
    string connectionString =
        "Server=(localdb)\\mssqllocaldb;Database=SignalRBlazorSSR;Trusted_Connection=True;MultipleActiveResultSets=true";

    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }
}
