using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;
public class ProductBaseModel
{    
    [Display(Name = "Ürün adı")]
    [Required(ErrorMessage = "{0} boş geçilemez.")]
    [StringLength(40, ErrorMessage = "{0} için {2}-{1} karakter aralığında değer girmelisiniz.", MinimumLength = 6)]
    public string ProductName { get; set; } = null!;
    
    [Display(Name = "Ürün açıklaması")]
    [Required(ErrorMessage = "{0} boş geçilemez.")]
    [StringLength(500, ErrorMessage = "{0} için {2}-{1} karakter aralığında değer girmelisiniz.", MinimumLength = 10)]
    public string? Description { get; set; }

    [Display(Name = "Ürün resmi")]
    public IFormFile? Image { get; set; }

    [Display(Name = "Stok")]
    [Required(ErrorMessage = "{0} bilgisi boş geçilemez.")]
    [Range(0, 100, ErrorMessage = "{0} bilgisi {1} - {2} arasında olmalıdır.")]
    public int? Stock { get; set; }

    [Display(Name = "Fiyat")]
    [Required(ErrorMessage = "{0} bilgisi boş geçilemez.")]
    [Range(0, 100000, ErrorMessage = "{0} bilgisi {1} ile {2} TL arasında olmamalıdır.")]
    public decimal? Price { get; set; }

    [Display(Name = "Aktif")]
    public bool IsActive { get; set; }

    [Display(Name = "Anasayfa")]
    public bool IsHome { get; set; }

    [Display(Name = "Kategori")]
    [Required(ErrorMessage = "{0} bilgisi boş geçilemez.")]
    public int? CategoryId { get; set; }
}