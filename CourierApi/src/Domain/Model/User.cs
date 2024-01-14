namespace Domain.Model;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string UserId { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public Gender Gender { get; set; }
}

public enum Gender
{
    Male,
    Female  
}