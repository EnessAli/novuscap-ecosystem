using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.DAL
{
    public interface IEcosystemPointRepository
    {
        Task<IEnumerable<EcosystemPoint>> GetAllAsync();
        Task<EcosystemPoint?> GetByIdAsync(int id);
        Task<IEnumerable<EcosystemPoint>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<EcosystemPoint>> GetByCityAsync(string city);
        Task<IEnumerable<EcosystemPoint>> GetByDistrictAsync(string district);
        Task<IEnumerable<EcosystemPoint>> SearchAsync(string searchTerm);
        Task<EcosystemPoint> AddAsync(EcosystemPoint ecosystemPoint);
        Task UpdateAsync(EcosystemPoint ecosystemPoint);
        Task DeleteAsync(int id);
    }
} 