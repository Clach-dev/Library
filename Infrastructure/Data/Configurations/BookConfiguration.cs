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
            .Property(book => book.ISBN)
            .IsRequired(true);

        builder
            .Property(book => book.Title)
            .IsRequired(true);

        builder
            .Property(book => book.Description)
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
    }
}