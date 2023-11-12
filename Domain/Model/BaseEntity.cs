namespace Domain.Model;

public abstract class BaseEntity : Base
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    
}