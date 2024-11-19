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
            .IsRequired(true);

        builder
            .Property(user => user.Password)
            .IsRequired(true);

        builder
            .Property(user => user.LastName)
            .IsRequired(true);
        
        builder
            .Property(user => user.FirstName)
            .IsRequired(true);
        
        builder
            .Property(user => user.MiddleName)
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
    }
}