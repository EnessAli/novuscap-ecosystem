using Microsoft.EntityFrameworkCore;
using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.DAL
{
    public class ContactMessageRepository : IContactMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactMessage>> GetAllAsync()
        {
            return await _context.ContactMessages
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<ContactMessage?> GetByIdAsync(int id)
        {
            return await _context.ContactMessages.FindAsync(id);
        }

        public async Task<IEnumerable<ContactMessage>> GetUnreadAsync()
        {
            return await _context.ContactMessages
                .Where(c => !c.IsRead)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<ContactMessage> AddAsync(ContactMessage contactMessage)
        {
            contactMessage.CreatedAt = DateTime.Now;
            contactMessage.IsRead = false;
            contactMessage.IsArchived = false;
            
            await _context.ContactMessages.AddAsync(contactMessage);
            await _context.SaveChangesAsync();
            return contactMessage;
        }

        public async Task UpdateAsync(ContactMessage contactMessage)
        {
            _context.Entry(contactMessage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ArchiveAsync(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                message.IsArchived = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                _context.ContactMessages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }
    }
} 