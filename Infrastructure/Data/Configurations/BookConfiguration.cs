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
            .HasMaxLength(20)
            .IsRequired(true);

        builder
            .Property(book => book.Title)
            .HasMaxLength(50)
            .IsRequired(true);

        builder
            .Property(book => book.Description)
            .HasMaxLength(300)
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