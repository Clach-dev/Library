using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(user => user.Id);
        
        builder
            .Property(user => user.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired(true);

        builder
            .Property(user => user.Password)
            .HasMaxLength(300)
            .IsRequired(true);

        builder
            .Property(user => user.LastName)
            .HasMaxLength(50)
            .IsRequired(true);
        
        builder
            .Property(user => user.FirstName)
            .HasMaxLength(50)
            .IsRequired(true);
        
        builder
            .Property(user => user.MiddleName)
            .HasMaxLength(50)
            .IsRequired(false);
        
        builder
            .Property(user => user.BirthDate)
            .IsRequired(true);

        builder
            .Property(user => user.ProfileImage)
            .HasConversion(
                uri => uri == null ? null : uri.ToString(),
                str => string.IsNullOrWhiteSpace(str) ? null : new Uri(str))
            .IsRequired(false);
        
        builder
            .Property(user => user.Role)
            .IsRequired(true);

        builder
            .HasMany(user => user.Reservations)
            .WithOne(reservation => reservation.User)
            .HasForeignKey(reservation => reservation.UserId);

        builder
            .HasMany(user => user.Reviews)
            .WithOne(review => review.User)
            .HasForeignKey(review => review.UserId);
        
        builder
            .HasOne(user => user.RefreshToken)
            .WithOne(refreshToken => refreshToken.User);
    }
}