using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder
            .HasKey(review => review.Id);

        builder
            .Property(review => review.Rating)
            .IsRequired(true);

        builder
            .Property(review => review.Comment)
            .HasMaxLength(300)
            .IsRequired(false);

        builder
            .HasOne(review => review.Book)
            .WithMany(book => book.Reviews);

        builder
            .HasOne(review => review.User)
            .WithMany(user => user.Reviews);
    }
}