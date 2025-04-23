using System;
using System.Collections.Generic;

namespace RestaurantManagement.Services
{
    public class MenuItemService
    {
        private readonly MenuItemRepository _menuItemRepository;

        public MenuItemService(MenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public MenuItem CreateMenuItem(MenuItem menuItem)
        {
            var existingItem = _menuItemRepository.GetByName(menuItem.Name, menuItem.RestaurantId);
            if (existingItem != null)
                throw new Exception("Menu item already exists.");

            _menuItemRepository.Add(menuItem);
            return menuItem;
        }

        public MenuItem GetMenuItemById(int id)
        {
            return _menuItemRepository.Get(id);
        }

        public List<MenuItem> GetAllMenuItems()
        {
            return _menuItemRepository.GetAll();
        }

        public void UpdateMenuItem(int id, MenuItem menuItem)
        {
            var existingItem = _menuItemRepository.Get(id);
            if (existingItem == null)
                throw new Exception("Menu item not found.");

            _menuItemRepository.Update(menuItem);
        }

        public void DeleteMenuItem(int id)
        {
            var menuItem = _menuItemRepository.Get(id);
            if (menuItem == null)
                throw new Exception("Menu item not found.");

            _menuItemRepository.Delete(id);
        }

        public void ToggleMenuItemAvailability(int id, bool isAvailable)
        {
            var menuItem = _menuItemRepository.Get(id);
            if (menuItem == null)
                throw new Exception("Menu item not found.");

            menuItem.IsAvailable = isAvailable;
            _menuItemRepository.Update(menuItem);
        }
    }
}
