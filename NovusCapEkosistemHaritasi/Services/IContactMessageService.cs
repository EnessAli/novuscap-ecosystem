using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.Services
{
    public interface IContactMessageService
    {
        Task<IEnumerable<ContactMessage>> GetAllContactMessagesAsync();
        Task<ContactMessage?> GetContactMessageByIdAsync(int id);
        Task<IEnumerable<ContactMessage>> GetUnreadContactMessagesAsync();
        Task<ContactMessage> CreateContactMessageAsync(ContactMessage contactMessage);
        Task MarkContactMessageAsReadAsync(int id);
        Task ArchiveContactMessageAsync(int id);
        Task DeleteContactMessageAsync(int id);
    }
} 