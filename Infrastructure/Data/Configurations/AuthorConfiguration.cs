using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder
            .HasKey(author => author.Id);

        builder
            .Property(author => author.LastName)
            .HasMaxLength(50)
            .IsRequired(true);

        builder
            .Property(author => author.FirstName)
            .HasMaxLength(50)
            .IsRequired(true);
        
        builder
            .Property(author => author.MiddleName)
            .HasMaxLength(50)
            .IsRequired(false);

        builder
            .Property(author => author.Description)
            .HasMaxLength(300)
            .IsRequired(false);
        
        builder
            .HasMany(author => author.Books)
            .WithMany(book => book.Authors);
    }
}