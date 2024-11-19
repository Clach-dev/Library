namespace Domain.Entities;

public class Book : BaseEntity
{
    public string ISBN { get; set; } = string.Empty;
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public virtual IEnumerable<Author>? Authors { get; set; }
    
    public virtual IEnumerable<Genre> Genres { get; set; }
    
    public virtual IEnumerable<Reservation> Reservations { get; set; }
}