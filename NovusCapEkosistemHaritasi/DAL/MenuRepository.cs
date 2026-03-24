using Microsoft.EntityFrameworkCore;
using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.DAL
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _context;

        public MenuRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await _context.Menus
                .Include(m => m.SubMenus)
                .OrderBy(m => m.Order)
                .ToListAsync();
        }

        public async Task<IEnumerable<Menu>> GetMainMenuItemsAsync()
        {
            return await _context.Menus
                .Include(m => m.SubMenus)
                .Where(m => m.ParentId == null && m.IsActive)
                .OrderBy(m => m.Order)
                .ToListAsync();
        }

        public async Task<IEnumerable<Menu>> GetSubMenuItemsAsync(int parentId)
        {
            return await _context.Menus
                .Where(m => m.ParentId == parentId && m.IsActive)
                .OrderBy(m => m.Order)
                .ToListAsync();
        }

        public async Task<Menu?> GetByIdAsync(int id)
        {
            return await _context.Menus
                .Include(m => m.SubMenus)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Menu> AddAsync(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task UpdateAsync(Menu menu)
        {
            _context.Entry(menu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                // Check if there are any sub-menus
                var subMenus = await _context.Menus.Where(m => m.ParentId == id).ToListAsync();
                if (subMenus.Any())
                {
                    // Remove all sub-menus
                    _context.Menus.RemoveRange(subMenus);
                }
                
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }
    }
} 