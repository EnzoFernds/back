using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Service
{
    public class AutheServiceBase
    {
        //private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
    }
}