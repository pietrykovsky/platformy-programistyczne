using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Backend.Data.Models;

namespace Backend.Data;

public class JokeContext : DbContext
{
    public JokeContext(DbContextOptions<JokeContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Joke> Jokes { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Favourite> Favourites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Joke>()
            .HasOne(j => j.Rating)
            .WithOne(r => r.Joke)
            .HasForeignKey<Rating>(r => r.JokeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Joke>()
            .HasOne(j => j.Favourite)
            .WithOne(f => f.Joke)
            .HasForeignKey<Favourite>(f => f.JokeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class JokeContextFactory : IDesignTimeDbContextFactory<JokeContext>
{
    public JokeContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<JokeContext>();
        optionsBuilder.UseSqlite("Data Source=JokeDatabase.db");

        return new JokeContext(optionsBuilder.Options);
    }
}
