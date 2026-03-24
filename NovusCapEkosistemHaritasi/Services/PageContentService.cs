using NovusCapEkosistemHaritasi.DAL;
using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.Services
{
    public class PageContentService : IPageContentService
    {
        private readonly IPageContentRepository _pageContentRepository;

        public PageContentService(IPageContentRepository pageContentRepository)
        {
            _pageContentRepository = pageContentRepository;
        }

        public async Task<IEnumerable<PageContent>> GetAllPageContentsAsync()
        {
            return await _pageContentRepository.GetAllAsync();
        }

        public async Task<PageContent?> GetPageContentByIdAsync(int id)
        {
            return await _pageContentRepository.GetByIdAsync(id);
        }

        public async Task<PageContent?> GetPageContentByNameAsync(string pageName)
        {
            return await _pageContentRepository.GetByPageNameAsync(pageName);
        }

        public async Task<PageContent> CreatePageContentAsync(PageContent pageContent)
        {
            // Check if page with same name already exists
            var existingPage = await _pageContentRepository.GetByPageNameAsync(pageContent.PageName);
            if (existingPage != null)
            {
                throw new ArgumentException("Bu isimde bir sayfa içeriği zaten mevcut.");
            }

            return await _pageContentRepository.AddAsync(pageContent);
        }

        public async Task UpdatePageContentAsync(PageContent pageContent)
        {
            // Check if page with same name already exists (except this one)
            var existingPage = await _pageContentRepository.GetByPageNameAsync(pageContent.PageName);
            if (existingPage != null && existingPage.Id != pageContent.Id)
            {
                throw new ArgumentException("Bu isimde bir sayfa içeriği zaten mevcut.");
            }

            await _pageContentRepository.UpdateAsync(pageContent);
        }

        public async Task DeletePageContentAsync(int id)
        {
            await _pageContentRepository.DeleteAsync(id);
        }
    }
} 