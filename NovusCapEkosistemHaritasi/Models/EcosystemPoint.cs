using System.ComponentModel.DataAnnotations;

namespace NovusCapEkosistemHaritasi.Models
{
    public class EcosystemPoint
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "İsim alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "İsim en fazla 100 karakter olabilir.")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        public string Description { get; set; } = string.Empty;
        
        [Url(ErrorMessage = "Geçerli bir URL giriniz.")]
        [StringLength(200, ErrorMessage = "Website URL'i en fazla 200 karakter olabilir.")]
        public string Website { get; set; } = string.Empty;
        
        [Range(-90, 90, ErrorMessage = "Enlem değeri -90 ile 90 arasında olmalıdır.")]
        public double Latitude { get; set; }
        
        [Range(-180, 180, ErrorMessage = "Boylam değeri -180 ile 180 arasında olmalıdır.")]
        public double Longitude { get; set; }
        
        [Required(ErrorMessage = "Kategori alanı zorunludur.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        
        [StringLength(100, ErrorMessage = "İkon adı en fazla 100 karakter olabilir.")]
        public string Icon { get; set; } = string.Empty;
        
        [StringLength(200, ErrorMessage = "Adres en fazla 200 karakter olabilir.")]
        public string Address { get; set; } = string.Empty;
        
        [StringLength(50, ErrorMessage = "Şehir adı en fazla 50 karakter olabilir.")]
        public string City { get; set; } = string.Empty;
        
        [StringLength(50, ErrorMessage = "İlçe adı en fazla 50 karakter olabilir.")]
        public string District { get; set; } = string.Empty;
    }
} 