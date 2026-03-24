using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NovusCapEkosistemHaritasi.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Kategori adı en fazla 50 karakter olabilir.")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "Açıklama en fazla 100 karakter olabilir.")]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "İkon adı en fazla 100 karakter olabilir.")]
        public string Icon { get; set; } = string.Empty;
        
        // Navigation property
        [JsonIgnore]
        public ICollection<EcosystemPoint> EcosystemPoints { get; set; } = new List<EcosystemPoint>();
    }
} 