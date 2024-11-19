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
            .IsRequired(true);

        builder
            .Property(author => author.FirstName)
            .IsRequired(true);
        
        builder
            .Property(author => author.MiddleName)
            .IsRequired(false);

        builder
            .Property(author => author.Description)
            .IsRequired(false);
        
        builder
            .HasMany(author => author.Books)
            .WithMany(book => book.Authors);
    }
}