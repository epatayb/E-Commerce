using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class AccountCreateModel
{   
    [Display(Name = "Ad Soyad")]
    [Required(ErrorMessage = "{0} zorunlu alan")]
    public string Fullname { get; set; } = null!;

    [Display(Name ="Eposta")]
    [Required(ErrorMessage = "{0} zorunlu alan")]
    [EmailAddress(ErrorMessage = "Geçerli mail adresi giriniz")]
    public string Email { get; set; } = null!;
 
    [Display(Name = "Şifre")]
    [Required(ErrorMessage = "{0} zorunlu alan")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Şifre Tekrar")]
    [Required(ErrorMessage = "{0} zorunlu alan")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor!")]
    public string ConfirmPassword { get; set; } = null!;

}