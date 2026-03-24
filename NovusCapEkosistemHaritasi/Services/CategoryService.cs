using NovusCapEkosistemHaritasi.DAL;
using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _categoryRepository.GetByNameAsync(name);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            // Check if category with same name already exists
            var existingCategory = await _categoryRepository.GetByNameAsync(category.Name);
            if (existingCategory != null)
            {
                throw new ArgumentException("Bu isimde bir kategori zaten mevcut.");
            }

            return await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            // Check if category with same name already exists (except this one)
            var existingCategory = await _categoryRepository.GetByNameAsync(category.Name);
            if (existingCategory != null && existingCategory.Id != category.Id)
            {
                throw new ArgumentException("Bu isimde bir kategori zaten mevcut.");
            }

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
} 