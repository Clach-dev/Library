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
            .IsRequired(true);

        builder
            .Property(genre => genre.Description)
            .IsRequired(false);
        
        builder
            .HasMany(genre => genre.Books)
            .WithMany(book => book.Genres);
    }
}