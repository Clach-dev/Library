namespace Domain.Entities;

public class Author : BaseEntity
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string? Patronymic { get; set; }
    
    public string Description { get; set; }
    
    public virtual IEnumerable<Book>? Books { get; set; }
}