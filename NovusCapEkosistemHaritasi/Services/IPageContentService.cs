using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.Services
{
    public interface IPageContentService
    {
        Task<IEnumerable<PageContent>> GetAllPageContentsAsync();
        Task<PageContent?> GetPageContentByIdAsync(int id);
        Task<PageContent?> GetPageContentByNameAsync(string pageName);
        Task<PageContent> CreatePageContentAsync(PageContent pageContent);
        Task UpdatePageContentAsync(PageContent pageContent);
        Task DeletePageContentAsync(int id);
    }
} 