using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication1.Data;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantRepository _restaurantRepository;
        private readonly RestaurantContext _context;
        public RestaurantController(RestaurantRepository restaurantRepository, RestaurantContext context)
        {
            _restaurantRepository = restaurantRepository;
            _context = context;

        }
        
        [HttpGet]
        public ActionResult<List<Restaurant>> GetAllRestaurants()
        {
            return Ok(_restaurantRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                Console.WriteLine($"Restaurant {id} introuvable !");
                return NotFound();
            }

            return restaurant;
        }


        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(CreateRestaurantDTO dto)
        {
            var restaurant = new Restaurant
            {
                Name = dto.Name,
                Address = dto.Address,
                OwnerId = dto.OwnerId
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.RestaurantId }, restaurant);
        }

        [Authorize(Roles = "Restaurateur")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, UpdateRestaurantDTO dto)
        {
            if (id != dto.RestaurantId)
                return BadRequest();

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
                return NotFound();

            restaurant.Name = dto.Name;
            restaurant.Address = dto.Address;
            restaurant.OwnerId = dto.OwnerId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
                return NotFound();

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
