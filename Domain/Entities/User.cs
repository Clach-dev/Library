using Domain.Enums;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string? MiddleName { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public Roles Role { get; set; }
    
    public virtual IEnumerable<Reservation>? Reservations { get; set; }
}