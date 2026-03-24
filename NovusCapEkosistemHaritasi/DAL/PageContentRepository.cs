using Microsoft.EntityFrameworkCore;
using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.DAL
{
    public class PageContentRepository : IPageContentRepository
    {
        private readonly ApplicationDbContext _context;

        public PageContentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PageContent>> GetAllAsync()
        {
            return await _context.PageContents.ToListAsync();
        }

        public async Task<PageContent?> GetByIdAsync(int id)
        {
            return await _context.PageContents.FindAsync(id);
        }

        public async Task<PageContent?> GetByPageNameAsync(string pageName)
        {
            return await _context.PageContents
                .FirstOrDefaultAsync(p => p.PageName.ToLower() == pageName.ToLower());
        }

        public async Task<PageContent> AddAsync(PageContent pageContent)
        {
            pageContent.CreatedAt = DateTime.Now;
            await _context.PageContents.AddAsync(pageContent);
            await _context.SaveChangesAsync();
            return pageContent;
        }

        public async Task UpdateAsync(PageContent pageContent)
        {
            pageContent.UpdatedAt = DateTime.Now;
            _context.Entry(pageContent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pageContent = await _context.PageContents.FindAsync(id);
            if (pageContent != null)
            {
                _context.PageContents.Remove(pageContent);
                await _context.SaveChangesAsync();
            }
        }
    }
} 