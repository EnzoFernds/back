using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantRepository _restaurantRepository;

        public RestaurantController(RestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        [HttpGet]
        public ActionResult<List<Restaurant>> GetAllRestaurants()
        {
            return Ok(_restaurantRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetRestaurantById(int id)
        {
            var restaurant = _restaurantRepository.Get(id);
            if (restaurant == null)
                return NotFound();
            return Ok(restaurant);
        }

        [HttpPost]
        public ActionResult CreateRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Add(restaurant);
            return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.RestaurantId }, restaurant);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.RestaurantId)
                return BadRequest();
            _restaurantRepository.Update(restaurant);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRestaurant(int id)
        {
            var restaurant = _restaurantRepository.Get(id);
            if (restaurant == null)
                return NotFound();
            _restaurantRepository.Delete(id);
            return NoContent();
        }
    }
}
