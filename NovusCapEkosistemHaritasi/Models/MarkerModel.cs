using System.ComponentModel.DataAnnotations;

namespace NovusCapEkosistemHaritasi.Models
{
    // Bu sınıf geriye dönük uyumluluk için vardır.
    // Mevcut kodu bozmamak için EcosystemPoint sınıfını referans alır.
    public class MarkerModel : EcosystemPoint
    {
        // Tüm özellikler EcosystemPoint'tan miras alınır

        // Eski MarkerModel Category özelliğini CategoryId ve Category'nin get metodlarıyla değiştirelim
        [Display(Name = "Kategori")]
        public new string Category
        {
            get
            {
                // Eğer base.Category nesnesi varsa adını döndür, yoksa CategoryId'yi döndür
                if (base.Category != null)
                    return base.Category.Name;
                return $"Kategori {CategoryId}";
            }
            set
            {
                // Bu özellik şu an için geriye dönük uyumluluk içindir,
                // set işlemi gerçekte CategoryId'ye yapılmalı
            }
        }
    }
} 