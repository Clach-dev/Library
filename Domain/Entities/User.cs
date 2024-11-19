namespace Domain.Entities;

public class User : BaseEntity
{
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Patronymic { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public string Role { get; set; }
    
    public virtual IEnumerable<Reservation>? Reservations { get; set; }
}