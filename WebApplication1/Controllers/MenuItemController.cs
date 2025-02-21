using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly MenuItemRepository _menuItemRepository;

        public MenuItemController(MenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        [HttpGet]
        public ActionResult<List<MenuItem>> GetAllMenuItems()
        {
            return Ok(_menuItemRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<MenuItem> GetMenuItemById(int id)
        {
            var menuItem = _menuItemRepository.Get(id);
            if (menuItem == null)
                return NotFound();
            return Ok(menuItem);
        }

        [HttpPost]
        public ActionResult CreateMenuItem(MenuItem menuItem)
        {
            _menuItemRepository.Add(menuItem);
            return CreatedAtAction(nameof(GetMenuItemById), new { id = menuItem.MenuItemId }, menuItem);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateMenuItem(int id, MenuItem menuItem)
        {
            if (id != menuItem.MenuItemId)
                return BadRequest();
            _menuItemRepository.Update(menuItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMenuItem(int id)
        {
            var menuItem = _menuItemRepository.Get(id);
            if (menuItem == null)
                return NotFound();
            _menuItemRepository.Delete(id);
            return NoContent();
        }
    }
}
