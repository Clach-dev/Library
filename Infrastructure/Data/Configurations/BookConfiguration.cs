using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .HasKey(book => book.Id);

        builder
            .HasIndex(book => book.ISBN)
            .IsUnique(true);

        builder
            .Property(book => book.ISBN)
            .IsRequired(true)
            .HasMaxLength(20);
        
        builder
            .Property(book => book.Title)
            .HasMaxLength(50)
            .IsRequired(true);

        builder
            .Property(book => book.AgeLimit)
            .IsRequired(true);
        
        builder
            .Property(book => book.Description)
            .HasMaxLength(300)
            .IsRequired(false);
        
        builder
            .Property(book => book.Images)
            .HasConversion(
                uris => string.Join(";", uris.Take(10).Select(u => u.ToString())),
                str => (str == ""
                    ? new List<Uri>()
                    : str.Split(';', StringSplitOptions.RemoveEmptyEntries)
                        .Take(10)
                        .Select(s => new Uri(s))
                        .ToList())
            )

            .HasMaxLength(4000)
            .IsRequired(false);
        
        builder
            .HasMany(book => book.Authors)
            .WithMany(author => author.Books);

        builder
            .HasMany(book => book.Genres)
            .WithMany(genre => genre.Books);
        
        builder
            .HasMany(book => book.Reservations)
            .WithOne(reservation => reservation.Book)
            .HasForeignKey(reservation => reservation.BookId);
        
        builder
            .HasMany(book => book.Reviews)
            .WithOne(review => review.Book)
            .HasForeignKey(review => review.BookId);
    }
}