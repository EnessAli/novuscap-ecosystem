using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.Services
{
    public interface IEcosystemPointService
    {
        Task<IEnumerable<EcosystemPoint>> GetAllEcosystemPointsAsync();
        Task<EcosystemPoint?> GetEcosystemPointByIdAsync(int id);
        Task<IEnumerable<EcosystemPoint>> GetEcosystemPointsByCategoryAsync(int categoryId);
        Task<IEnumerable<EcosystemPoint>> GetEcosystemPointsByCityAsync(string city);
        Task<IEnumerable<EcosystemPoint>> GetEcosystemPointsByDistrictAsync(string district);
        Task<IEnumerable<EcosystemPoint>> SearchEcosystemPointsAsync(string searchTerm);
        Task<IEnumerable<string>> GetAllCitiesAsync();
        Task<IEnumerable<string>> GetDistrictsByCityAsync(string city);
        Task<EcosystemPoint> CreateEcosystemPointAsync(EcosystemPoint ecosystemPoint);
        Task UpdateEcosystemPointAsync(EcosystemPoint ecosystemPoint);
        Task DeleteEcosystemPointAsync(int id);
    }
} 