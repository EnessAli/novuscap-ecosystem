using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.DAL
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllAsync();
        Task<IEnumerable<Menu>> GetMainMenuItemsAsync();
        Task<IEnumerable<Menu>> GetSubMenuItemsAsync(int parentId);
        Task<Menu?> GetByIdAsync(int id);
        Task<Menu> AddAsync(Menu menu);
        Task UpdateAsync(Menu menu);
        Task DeleteAsync(int id);
    }
} 