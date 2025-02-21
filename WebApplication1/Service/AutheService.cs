
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.DTO;


namespace WebApplication1.Service
{
    public class AutheService
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AutheService(Microsoft.AspNetCore.Identity.UserManager<User> userManager, Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task Sigin(SiginDTO user) {
            User userToAdd = new User()
            {
                Email = user.Email,
                LastName = user.Nom,
                FirstName = user.Prenom,
                UserName = user.Email
            };
            var result = await _userManager.CreateAsync(userToAdd,user.Password);
        }
        public async Task RoleAsync(RoleDTO role)
        {
            Role addRoles = new Role()
            {
                Name = role.Nom,
                Description = role.Description
            };

            var result = await _roleManager.CreateAsync(addRoles);

            if (!result.Succeeded)
            {
                var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Erreur inconnue lors de la création du rôle";
                throw new BadHttpRequestException(errorMessage);
            }
        }


        public async Task<string> Login(LoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var token = GenerateJwtToken(user);
                return token;
            }
            else
            {
                throw new UnauthorizedAccessException($"Failed to login{login.Email}");
            }
        }

        public string GenerateJwtToken(User user) 
        {
            var jwtSettings = _configuration.GetSection("jwt");
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}