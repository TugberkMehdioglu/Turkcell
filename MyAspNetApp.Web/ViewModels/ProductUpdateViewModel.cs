using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetApp.Web.ViewModels
{
    public class ProductUpdateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim alanı zorunludur")]
        [StringLength(50, MinimumLength = 0, ErrorMessage ="İsim alanına en fazla 50 karakter girilebilir")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Fiyat alanı zorunludur")]
        [Range(0, 1000, ErrorMessage ="Fiyat 0 ile 100 arasında olmalıdır")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})", ErrorMessage ="Fiyat alanında noktadan sonra en fazla 2 basamak olmalıdır")] //regex'te: \ olduğundan başına @ koyduk.
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Stok alanı zorunludur")]
        [Range(0, 200, ErrorMessage ="Stok 0 ile 200 arasında olmalıdır")]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "Renk alanı zorunludur")]
        public string? Color { get; set; }

        public bool İsPublish { get; set; }

        [Required(ErrorMessage = "Yayında kalma süresi alanı zorunludur")]
        public int? Expire { get; set; }

        [Required(ErrorMessage = "Açıklama alanı zorunludur")]
        [StringLength(100, MinimumLength = 0, ErrorMessage ="Açıklama alanına en fazla 100 karakter girilebilir")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Yayınlanma tarihi alanı zorunludur")]
        public DateTime? PublishDate { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; } //Db'den null olan data gelirse, gösterirken sıkıntı olmasın diye nullable yaptık
        //ürün eklerken fotosuz ürün gelirse diye yaptık

        [ValidateNever]
        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "Kategori seçiniz")]
        public int CategoryId { get; set; }

    }
}
