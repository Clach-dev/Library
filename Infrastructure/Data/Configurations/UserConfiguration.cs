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
            .Property(user => user.Login)
            .HasMaxLength(50)
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
            .Property(user => user.Role)
            .IsRequired(true);

        builder
            .HasMany(user => user.Reservations)
            .WithOne(reservation => reservation.User)
            .HasForeignKey(reservation => reservation.UserId);
        
        builder
            .HasOne(user => user.RefreshToken)
            .WithOne(refreshToken => refreshToken.User);
    }
}