using System;
using System.Collections.Generic;

namespace RestaurantManagement.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User CreateUser(User user)
        {
            if (_userRepository.GetByEmail(user.Email) != null)
                throw new Exception("Email already in use.");

            _userRepository.Add(user);
            return user;
        }

        public User GetUserById(int id)
        {
            return _userRepository.Get(id);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public void UpdateUser(int id, User user)
        {
            var existingUser = _userRepository.Get(id);
            if (existingUser == null)
                throw new Exception("User not found.");

            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
                throw new Exception("User not found.");

            _userRepository.Delete(id);
        }
    }
}
