using System.Diagnostics;
using Kowmal.API.Models;
using Kowmal.API.Persistance.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Kowmal.API.Persistance;

public class AppDbContext : DbContext
{
    // Magic string.
    public static readonly string RowVersion = nameof(RowVersion);

    // Magic strings.
    public static readonly string KowmalDb = nameof(KowmalDb).ToLower();

    // Inject options.
    // options: The DbContextOptions{ContactContext} for the context.
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Debug.WriteLine($"{ContextId} context created.");
    }
    
    public DbSet<Post> Posts { get; set; }
    public DbSet<Photo> Photos { get; set; }

    // Define the model.
    // modelBuilder: The ModelBuilder.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PhotoEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    // Dispose pattern.
    public override void Dispose()
    {
        Debug.WriteLine($"{ContextId} context disposed.");
        base.Dispose();
    }

    // Dispose pattern.
    public override ValueTask DisposeAsync()
    {
        Debug.WriteLine($"{ContextId} context disposed async.");
        return base.DisposeAsync();
    }
}