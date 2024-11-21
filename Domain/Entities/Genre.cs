namespace Domain.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public virtual IEnumerable<Book>? Books { get; set; }
}