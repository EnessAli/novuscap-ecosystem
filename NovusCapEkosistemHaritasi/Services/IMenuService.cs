using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.Services
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetAllMenusAsync();
        Task<IEnumerable<Menu>> GetMainMenuItemsAsync();
        Task<IEnumerable<Menu>> GetSubMenuItemsAsync(int parentId);
        Task<Menu?> GetMenuByIdAsync(int id);
        Task<Menu> CreateMenuAsync(Menu menu);
        Task UpdateMenuAsync(Menu menu);
        Task DeleteMenuAsync(int id);
    }
} 