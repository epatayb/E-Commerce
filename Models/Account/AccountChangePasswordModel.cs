using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class AccountChangePasswordModel
{   
    [Display(Name = "Mevcut Şifre")]
    [Required(ErrorMessage = "{0} zorunlu alan")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = null!;
 
    [Display(Name = "Yeni Şifre")]
    [Required(ErrorMessage = "{0} zorunlu alan")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Yeni Şifre Tekrar")]
    [Required(ErrorMessage = "{0} zorunlu alan")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor!")]
    public string ConfirmPassword { get; set; } = null!;
}