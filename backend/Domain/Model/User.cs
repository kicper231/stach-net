namespace Domain.Model;

public class User : BaseEntity
{
    public int UserId { get; set; }
    public string Auth0Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public int NumberOfLogins { get; set; }
}