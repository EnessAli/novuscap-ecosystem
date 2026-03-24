using System.ComponentModel.DataAnnotations;

namespace NovusCapEkosistemHaritasi.Models
{
    public class Menu
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(50, ErrorMessage = "Başlık en fazla 50 karakter olabilir.")]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "URL en fazla 100 karakter olabilir.")]
        public string? Url { get; set; }
        
        [StringLength(50, ErrorMessage = "İkon en fazla 50 karakter olabilir.")]
        public string? Icon { get; set; }
        
        public int Order { get; set; } = 0;
        
        public int? ParentId { get; set; }
        public Menu? Parent { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public ICollection<Menu> SubMenus { get; set; } = new List<Menu>();
    }
} 