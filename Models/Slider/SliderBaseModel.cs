using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class SliderBaseModel
{
    [Display(Name = "Başlık")]
    [Required(ErrorMessage = "{0} boş geçilemez.")]
    [StringLength(25, ErrorMessage = "{0} için max {1} karakter girmelisiniz.")]
    public string? Title { get; set; }

    [Display(Name = "Açıklama")]
    [Required(ErrorMessage = "{0} boş geçilemez.")]
    [StringLength(50, ErrorMessage = "{0} için {2}-{1} karakter aralığında değer girmelisiniz.", MinimumLength = 6)]
    public string? Description { get; set; }

    [Display(Name = "Resim")]
    public IFormFile? Image { get; set; }

    [Display(Name = "Index")]
    [Required(ErrorMessage = "{0} boş geçilemez.")]
    [Range(0, 25, ErrorMessage = "{0} bilgisi {1} - {2} arasında olmalıdır.")]
    public int Index { get; set; }

    [Display(Name = "Aktif")]
    public bool IsActive { get; set; }
}