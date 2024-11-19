namespace Domain.Entities;

public class Book : BaseEntity
{
    public string ISBN { get; set; }
    
    public string Title { get; set; }
    
    public string? Description { get; set; }
    
    public virtual IEnumerable<Author>? Authors { get; set; }
    
    public virtual IEnumerable<Genre>? Genres { get; set; }
    
    public virtual IEnumerable<Reservation>? Reservations { get; set; }
}