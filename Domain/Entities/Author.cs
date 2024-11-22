namespace Domain.Entities;

public class Author : BaseEntity
{
    public string LastName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    
    public string? MiddleName { get; set; }
    
    public string? Description { get; set; }
    
    public virtual IEnumerable<Book>? Books { get; set; }
}