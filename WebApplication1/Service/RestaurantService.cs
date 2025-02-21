using System;
using System.Collections.Generic;

namespace RestaurantManagement.Services
{
    public class RestaurantService
    {
        private readonly RestaurantRepository _restaurantRepository;

        public RestaurantService(RestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public Restaurant CreateRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Add(restaurant);
            return restaurant;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return _restaurantRepository.Get(id);
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return _restaurantRepository.GetAll();
        }

        public void UpdateRestaurant(int id, Restaurant restaurant)
        {
            var existingRestaurant = _restaurantRepository.Get(id);
            if (existingRestaurant == null)
                throw new Exception("Restaurant not found.");

            _restaurantRepository.Update(restaurant);
        }

        public void DeleteRestaurant(int id)
        {
            var restaurant = _restaurantRepository.Get(id);
            if (restaurant == null)
                throw new Exception("Restaurant not found.");

            _restaurantRepository.Delete(id);
        }
    }
}
