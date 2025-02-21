using Microsoft.AspNetCore.Identity;

public class Role : IdentityRole
{
    public int RoleID { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
}
