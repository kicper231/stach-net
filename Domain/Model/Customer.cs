namespace Domain.Model;

public class Order : BaseEntity
{
    public DateTime DueDate { get; set; }
    public Customer Customer { get; set; }
}
public class Customer : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public Gender Gender { get; set; }
}

public enum Gender
{
    Male,
    Female
}
