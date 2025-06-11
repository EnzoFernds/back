
using Microsoft.AspNetCore.Identity;

public enum UserRole
{
    Client,
    Restaurateur,
    Administrateur
}

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserRole Role { get; set; }

    public virtual List<Order> Orders { get; set; } = new List<Order>();

    public virtual List<Review> Reviews { get; set; } = new List<Review>();
}
