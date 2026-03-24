using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.DAL
{
    public interface IContactMessageRepository
    {
        Task<IEnumerable<ContactMessage>> GetAllAsync();
        Task<ContactMessage?> GetByIdAsync(int id);
        Task<IEnumerable<ContactMessage>> GetUnreadAsync();
        Task<ContactMessage> AddAsync(ContactMessage contactMessage);
        Task UpdateAsync(ContactMessage contactMessage);
        Task MarkAsReadAsync(int id);
        Task ArchiveAsync(int id);
        Task DeleteAsync(int id);
    }
} 