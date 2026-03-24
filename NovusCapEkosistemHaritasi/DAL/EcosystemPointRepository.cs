using Microsoft.EntityFrameworkCore;
using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.DAL
{
    public class EcosystemPointRepository : IEcosystemPointRepository
    {
        private readonly ApplicationDbContext _context;

        public EcosystemPointRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EcosystemPoint>> GetAllAsync()
        {
            return await _context.EcosystemPoints
                .Include(e => e.Category)
                .ToListAsync();
        }

        public async Task<EcosystemPoint?> GetByIdAsync(int id)
        {
            return await _context.EcosystemPoints
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<EcosystemPoint>> GetByCategoryAsync(int categoryId)
        {
            return await _context.EcosystemPoints
                .Include(e => e.Category)
                .Where(e => e.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<EcosystemPoint>> GetByCityAsync(string city)
        {
            return await _context.EcosystemPoints
                .Include(e => e.Category)
                .Where(e => e.City.ToLower() == city.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<EcosystemPoint>> GetByDistrictAsync(string district)
        {
            return await _context.EcosystemPoints
                .Include(e => e.Category)
                .Where(e => e.District.ToLower() == district.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<EcosystemPoint>> SearchAsync(string searchTerm)
        {
            return await _context.EcosystemPoints
                .Include(e => e.Category)
                .Where(e => e.Name.Contains(searchTerm) ||
                       e.Description.Contains(searchTerm) ||
                       e.Address.Contains(searchTerm) ||
                       e.City.Contains(searchTerm) ||
                       e.District.Contains(searchTerm) ||
                       e.Category.Name.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<EcosystemPoint> AddAsync(EcosystemPoint ecosystemPoint)
        {
            await _context.EcosystemPoints.AddAsync(ecosystemPoint);
            await _context.SaveChangesAsync();
            return ecosystemPoint;
        }

        public async Task UpdateAsync(EcosystemPoint ecosystemPoint)
        {
            _context.Entry(ecosystemPoint).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ecosystemPoint = await _context.EcosystemPoints.FindAsync(id);
            if (ecosystemPoint != null)
            {
                _context.EcosystemPoints.Remove(ecosystemPoint);
                await _context.SaveChangesAsync();
            }
        }
    }
} 