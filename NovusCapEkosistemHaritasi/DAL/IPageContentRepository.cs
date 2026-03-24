using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.DAL
{
    public interface IPageContentRepository
    {
        Task<IEnumerable<PageContent>> GetAllAsync();
        Task<PageContent?> GetByIdAsync(int id);
        Task<PageContent?> GetByPageNameAsync(string pageName);
        Task<PageContent> AddAsync(PageContent pageContent);
        Task UpdateAsync(PageContent pageContent);
        Task DeleteAsync(int id);
    }
} 