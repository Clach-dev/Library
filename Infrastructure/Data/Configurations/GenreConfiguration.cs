using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder
            .HasKey(genre => genre.Id);

        builder
            .Property(genre => genre.Name)
            .HasMaxLength(50)
            .IsRequired(true);

        builder
            .Property(genre => genre.Description)
            .HasMaxLength(300)
            .IsRequired(false);
        
        builder
            .HasMany(genre => genre.Books)
            .WithMany(book => book.Genres);
    }
}