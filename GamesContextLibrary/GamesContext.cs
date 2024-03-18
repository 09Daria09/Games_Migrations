using GamesLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

public class GamesContext : DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<GameMode> GameMode { get; set; } 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("GameDbConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
}
