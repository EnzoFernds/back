//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace RestaurantManagement.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly UserRepository _userRepository;
//        private readonly UserManager<User> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;

//        public UserController(UserRepository userRepository, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
//        {
//            _userRepository = userRepository;
//            _userManager = userManager;
//            _roleManager = roleManager;
//        }

//        // GET: api/users
//        [HttpGet]
//        public ActionResult<List<User>> GetAllUsers()
//        {
//            var users = _userRepository.GetAll();
//            return Ok(users);
//        }

//        // GET: api/users/1
//        [HttpGet("{id}")]
//        public ActionResult<User> GetUserById(int id)
//        {
//            var user = _userRepository.Get(id);
//            if (user == null)
//                return NotFound(new { message = "Utilisateur non trouvé." });

//            return Ok(user);
//        }

//        // POST: api/users
//        [HttpPost]
//        public async Task<ActionResult> CreateUser([FromBody] CreateUserDTO userDto)
//        {
//            if (await _userManager.FindByEmailAsync(userDto.Email) != null)
//            {
//                return BadRequest(new { message = "Cet email est déjà utilisé." });
//            }

//            var user = new User
//            {
//                UserName = userDto.Email,
//                FirstName = userDto.FirstName,
//                LastName = userDto.LastName,
//                Email = userDto.Email,
//                Role = userDto.Role
//            };

//            var result = await _userManager.CreateAsync(user, userDto.Password);

//            if (!result.Succeeded)
//            {
//                return BadRequest(new { message = "Erreur de création de l'utilisateur.", errors = result.Errors });
//            }

//            // Si le rôle existe, l'assigner, sinon créer le rôle.
//            //if (!string.IsNullOrEmpty(userDto.Role) && await _roleManager.RoleExistsAsync(userDto.Role))
//            //{
//            //    await _userManager.AddToRoleAsync(user, userDto.Role);
//            //}

//            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
//        }

//        [HttpPut("{id}")]
//        public ActionResult UpdateUser(string id, User user)
//        {
//            if (id != user.Id)
//            {
//                return BadRequest();
//            }

//            _userRepository.Update(user);
//            return NoContent();
//        }


//        // DELETE: api/users/1
//        [HttpDelete("{id}")]
//        public ActionResult DeleteUser(int id)
//        {
//            var user = _userRepository.Get(id);
//            if (user == null)
//            {
//                return NotFound(new { message = "Utilisateur non trouvé." });
//            }

//            _userRepository.Delete(id);
//            return NoContent();
//        }
//    }
//}
