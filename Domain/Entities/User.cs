using Domain.Enums;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string Login { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string? MiddleName { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public Roles Role { get; set; }
    
    public virtual IEnumerable<Reservation>? Reservations { get; set; }
}