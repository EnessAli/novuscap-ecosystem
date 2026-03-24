using NovusCapEkosistemHaritasi.DAL;
using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly IContactMessageRepository _contactMessageRepository;

        public ContactMessageService(IContactMessageRepository contactMessageRepository)
        {
            _contactMessageRepository = contactMessageRepository;
        }

        public async Task<IEnumerable<ContactMessage>> GetAllContactMessagesAsync()
        {
            return await _contactMessageRepository.GetAllAsync();
        }

        public async Task<ContactMessage?> GetContactMessageByIdAsync(int id)
        {
            return await _contactMessageRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ContactMessage>> GetUnreadContactMessagesAsync()
        {
            return await _contactMessageRepository.GetUnreadAsync();
        }

        public async Task<ContactMessage> CreateContactMessageAsync(ContactMessage contactMessage)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(contactMessage.Name))
            {
                throw new ArgumentException("İsim alanı zorunludur.");
            }

            if (string.IsNullOrWhiteSpace(contactMessage.Email))
            {
                throw new ArgumentException("E-posta alanı zorunludur.");
            }

            if (string.IsNullOrWhiteSpace(contactMessage.Subject))
            {
                throw new ArgumentException("Konu alanı zorunludur.");
            }

            if (string.IsNullOrWhiteSpace(contactMessage.Message))
            {
                throw new ArgumentException("Mesaj içeriği zorunludur.");
            }

            return await _contactMessageRepository.AddAsync(contactMessage);
        }

        public async Task MarkContactMessageAsReadAsync(int id)
        {
            await _contactMessageRepository.MarkAsReadAsync(id);
        }

        public async Task ArchiveContactMessageAsync(int id)
        {
            await _contactMessageRepository.ArchiveAsync(id);
        }

        public async Task DeleteContactMessageAsync(int id)
        {
            await _contactMessageRepository.DeleteAsync(id);
        }
    }
} 