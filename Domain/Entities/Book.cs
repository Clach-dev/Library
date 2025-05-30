namespace Domain.Entities;

public class Book : BaseEntity
{
    public string ISBN { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public byte AgeLimit { get; set; }

    public string? Description { get; set; }
    
    public IEnumerable<Uri> Images { get; set; } = new List<Uri>();
    
    public virtual IEnumerable<Author>? Authors { get; set; }
    
    public virtual IEnumerable<Genre>? Genres { get; set; }
    
    public virtual IEnumerable<Reservation>? Reservations { get; set; }
    
    public virtual IEnumerable<Review>? Reviews { get; set; }
}