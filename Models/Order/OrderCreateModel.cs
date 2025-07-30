using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class OrderCreateModel
{
    [Display(Name = "Ad Soyad")]
    [Required(ErrorMessage = "{0} bilgisi boş geçilemez.")]
    [StringLength(50, ErrorMessage = "{0} için {2}-{1} karakter aralığında değer girmelisiniz.", MinimumLength = 5)]
    public string ReceiverFullName { get; set; } = null!;

    [Display(Name = "Şehir")]
    [Required(ErrorMessage = "{0} bilgisi boş geçilemez.")]
    public string City { get; set; } = null!;

    [Display(Name = "Açık Adres")]
    [Required(ErrorMessage = "{0} bilgisi boş geçilemez.")]
    [StringLength(150, ErrorMessage = "{0} için {2}-{1} karakter aralığında değer girmelisiniz.", MinimumLength = 20)]
    public string Address { get; set; } = null!;

    [Display(Name = "Posta Kodu")]
    [Required(ErrorMessage = "{0} boş geçilemez.")]
    public string PostalCode { get; set; } = null!;

    [Display(Name = "Telefon Numarası")]
    [Required(ErrorMessage = "{0} boş geçilemez.")]
    public string Phone { get; set; } = null!;

    [Display(Name = "Sipariş Notu")]
    [StringLength(500, ErrorMessage = "Maksimum {1} karakter ile sınırlıdır.")]
    public string? OrderNote { get; set; }


    [Display(Name = "Kart üzerine isim")]
    public string CardHolderName { get; set; } = null!;

    [Display(Name = "Kart Numarası")]
    public string CardNumber { get; set; } = null!;

    [Display(Name = "Ay")]
    public string CardExpireMonth { get; set; } = null!;

    [Display(Name = "Yıl")]
    public string CardExpireYear { get; set; } = null!;

    [Display(Name = "CVV")]
    public string CardCvv { get; set; } = null!;

}