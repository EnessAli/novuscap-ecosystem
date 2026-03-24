using System.ComponentModel.DataAnnotations;

namespace NovusCapEkosistemHaritasi.Models
{
    public class PageContent
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Sayfa adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Sayfa adı en fazla 50 karakter olabilir.")]
        public string PageName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string Title { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "İçerik zorunludur.")]
        public string Content { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "Alt başlık en fazla 100 karakter olabilir.")]
        public string? Subtitle { get; set; }
        
        [StringLength(200, ErrorMessage = "Meta açıklaması en fazla 200 karakter olabilir.")]
        public string? MetaDescription { get; set; }
        
        [StringLength(100, ErrorMessage = "Meta anahtar kelimeleri en fazla 100 karakter olabilir.")]
        public string? MetaKeywords { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
    }
} 