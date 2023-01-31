using System.ComponentModel.DataAnnotations;

namespace MyAspNetApp.Web.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} alanı zorunludur"), Display(Name ="İsim")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} alanı {2} ile {1} karakter arasında olmalıdır")]
        public string Name { get; set; } = null!;
    }
}
