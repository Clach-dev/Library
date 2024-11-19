using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder
            .HasKey(reservation => reservation.Id);

        builder
            .Property(reservation => reservation.ReceiptDate)
            .IsRequired(true);

        builder
            .Property(reservation => reservation.ReturnDate)
            .IsRequired(true);

        builder
            .HasOne(reservation => reservation.User)
            .WithMany(user => user.Reservations);

        builder
            .HasOne(reservation => reservation.Book)
            .WithMany(book => book.Reservations);
    }
}