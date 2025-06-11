using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : Controller
    {
        private readonly AutheService _autheService;
        private readonly UserManager<User> _userManager;

        public AuthController(AutheService autheService, UserManager<User> userManager)
        {
            _autheService = autheService;
            _userManager = userManager;
        }

        [HttpPost("sigin")]
        public async Task<ActionResult> Sigin(SiginDTO user)
        {
            await _autheService.Sigin(user);
            return Ok();
           

        }
        [HttpPost("role")]
        public async Task<ActionResult> RoleAsync(RoleDTO role)
        {
            await _autheService.RoleAsync(role);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            var token = await _autheService.Login(login);
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Cet email est déjà utilisé." });
            }

            var user = new User
            {
                UserName = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Role = Enum.TryParse<UserRole>(registerDto.Role, true, out var parsedRole)
                    ? parsedRole
                    : UserRole.Client,

                EmailConfirmed = true,

                // 🔥 On force la normalisation :
                NormalizedEmail = registerDto.Email.ToUpperInvariant(),
                NormalizedUserName = registerDto.Email.ToUpperInvariant()
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    message = "Erreur lors de la création de l'utilisateur.",
                    errors = result.Errors
                });
            }

            // Optionnel : On peut forcer une Update (sécurité max, mais pas obligatoire ici)
            // await _userManager.UpdateAsync(user);

            return Ok(new { message = "Utilisateur créé avec succès." });
        }
    }
}
