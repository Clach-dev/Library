using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    
    public DbSet<Book> Books { get; set; }
    
    public DbSet<Genre> Genres { get; set; }
    
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    public DbSet<Reservation> Reservations { get; set; }
    
    public DbSet<Review> Reviews { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}