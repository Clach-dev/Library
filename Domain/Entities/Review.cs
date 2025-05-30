namespace Domain.Entities;

public class Review : BaseEntity
{
    public Guid BookId { get; set; }
    
    public Guid UserId { get; set; }
    
    public decimal Rating { get; set; }
    
    public string? Comment { get; set; }
    
    public virtual Book? Book { get; set; }
    
    public virtual User? User { get; set; }
}