using NovusCapEkosistemHaritasi.DAL;
using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.Services
{
    public class EcosystemPointService : IEcosystemPointService
    {
        private readonly IEcosystemPointRepository _ecosystemPointRepository;
        private readonly ICategoryRepository _categoryRepository;

        public EcosystemPointService(
            IEcosystemPointRepository ecosystemPointRepository,
            ICategoryRepository categoryRepository)
        {
            _ecosystemPointRepository = ecosystemPointRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<EcosystemPoint>> GetAllEcosystemPointsAsync()
        {
            return await _ecosystemPointRepository.GetAllAsync();
        }

        public async Task<EcosystemPoint?> GetEcosystemPointByIdAsync(int id)
        {
            return await _ecosystemPointRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<EcosystemPoint>> GetEcosystemPointsByCategoryAsync(int categoryId)
        {
            return await _ecosystemPointRepository.GetByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<EcosystemPoint>> GetEcosystemPointsByCityAsync(string city)
        {
            return await _ecosystemPointRepository.GetByCityAsync(city);
        }

        public async Task<IEnumerable<EcosystemPoint>> GetEcosystemPointsByDistrictAsync(string district)
        {
            return await _ecosystemPointRepository.GetByDistrictAsync(district);
        }

        public async Task<IEnumerable<EcosystemPoint>> SearchEcosystemPointsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllEcosystemPointsAsync();

            return await _ecosystemPointRepository.SearchAsync(searchTerm);
        }

        public async Task<IEnumerable<string>> GetAllCitiesAsync()
        {
            var ecosystemPoints = await _ecosystemPointRepository.GetAllAsync();
            return ecosystemPoints.Select(e => e.City).Distinct().OrderBy(c => c);
        }

        public async Task<IEnumerable<string>> GetDistrictsByCityAsync(string city)
        {
            var ecosystemPoints = await _ecosystemPointRepository.GetByCityAsync(city);
            return ecosystemPoints.Select(e => e.District).Distinct().OrderBy(d => d);
        }

        public async Task<EcosystemPoint> CreateEcosystemPointAsync(EcosystemPoint ecosystemPoint)
        {
            // Validate category exists
            var category = await _categoryRepository.GetByIdAsync(ecosystemPoint.CategoryId);
            if (category == null)
            {
                throw new ArgumentException("Geçersiz kategori ID'si.");
            }

            return await _ecosystemPointRepository.AddAsync(ecosystemPoint);
        }

        public async Task UpdateEcosystemPointAsync(EcosystemPoint ecosystemPoint)
        {
            // Validate category exists
            var category = await _categoryRepository.GetByIdAsync(ecosystemPoint.CategoryId);
            if (category == null)
            {
                throw new ArgumentException("Geçersiz kategori ID'si.");
            }

            await _ecosystemPointRepository.UpdateAsync(ecosystemPoint);
        }

        public async Task DeleteEcosystemPointAsync(int id)
        {
            await _ecosystemPointRepository.DeleteAsync(id);
        }
    }
} 