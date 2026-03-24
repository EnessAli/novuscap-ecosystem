using NovusCapEkosistemHaritasi.DAL;
using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            return await _menuRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Menu>> GetMainMenuItemsAsync()
        {
            return await _menuRepository.GetMainMenuItemsAsync();
        }

        public async Task<IEnumerable<Menu>> GetSubMenuItemsAsync(int parentId)
        {
            return await _menuRepository.GetSubMenuItemsAsync(parentId);
        }

        public async Task<Menu?> GetMenuByIdAsync(int id)
        {
            return await _menuRepository.GetByIdAsync(id);
        }

        public async Task<Menu> CreateMenuAsync(Menu menu)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(menu.Title))
            {
                throw new ArgumentException("Başlık alanı zorunludur.");
            }

            // If it's a submenu, validate parent exists
            if (menu.ParentId.HasValue)
            {
                var parent = await _menuRepository.GetByIdAsync(menu.ParentId.Value);
                if (parent == null)
                {
                    throw new ArgumentException("Geçersiz üst menü ID'si.");
                }
            }

            return await _menuRepository.AddAsync(menu);
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(menu.Title))
            {
                throw new ArgumentException("Başlık alanı zorunludur.");
            }

            // If it's a submenu, validate parent exists
            if (menu.ParentId.HasValue)
            {
                // Check for circular reference
                if (menu.ParentId.Value == menu.Id)
                {
                    throw new ArgumentException("Bir menü kendisini üst menü olarak referans gösteremez.");
                }

                var parent = await _menuRepository.GetByIdAsync(menu.ParentId.Value);
                if (parent == null)
                {
                    throw new ArgumentException("Geçersiz üst menü ID'si.");
                }
            }

            await _menuRepository.UpdateAsync(menu);
        }

        public async Task DeleteMenuAsync(int id)
        {
            await _menuRepository.DeleteAsync(id);
        }
    }
} 