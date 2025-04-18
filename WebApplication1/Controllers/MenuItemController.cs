using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult> CreateMenuItem([FromBody] CreateMenuItemDTO menuItemDTO)
        {
            // Vérifier si un menu avec le même nom existe déjà pour éviter les doublons
            var existingMenuItem = _menuItemRepository.GetAll().FirstOrDefault(m => m.Name == menuItemDTO.Name && m.RestaurantId == menuItemDTO.RestaurantId);
            if (existingMenuItem != null)
            {
                return BadRequest(new { message = "Un élément du menu avec ce nom existe déjà pour ce restaurant." });
            }

            // Création de l'entité MenuItem à partir du DTO
            var menuItem = new MenuItem
            {
                Name = menuItemDTO.Name,
                Description = menuItemDTO.Description,
                Price = menuItemDTO.Price,
                Category = menuItemDTO.Category,
                IsAvailable = menuItemDTO.IsAvailable,
                RestaurantId = menuItemDTO.RestaurantId
            };

            // Ajout à la base de données
            _menuItemRepository.Add(menuItem);

            // Retourne une réponse avec les données essentielles
            return CreatedAtAction(nameof(GetMenuItemById), new { id = menuItem.MenuItemId }, new
            {
                menuItem.MenuItemId,
                menuItem.Name,
                menuItem.Description,
                menuItem.Price,
                menuItem.Category,
                menuItem.IsAvailable,
                menuItem.RestaurantId
            });
        }

        [HttpPut("{id}")]
        public ActionResult UpdateMenuItem(int id, [FromBody] CreateMenuItemDTO dto)
        {
            var existing = _menuItemRepository.GetById(id);
            if (existing == null) return NotFound();

            // Mise à jour des champs
            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.Category = dto.Category;
            existing.IsAvailable = dto.IsAvailable;
            existing.RestaurantId = dto.RestaurantId;

            _menuItemRepository.Update(existing);

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
