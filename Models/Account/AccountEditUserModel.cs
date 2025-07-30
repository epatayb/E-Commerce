using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class AccountEditUserModel
{
    [Display(Name = "Ad Soyad")]
    [Required(ErrorMessage = "{0} zorunlu alan")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Eposta")]
    [Required(ErrorMessage = "{0} zorunlu alan")]
    [EmailAddress(ErrorMessage = "Geçerli mail adresi giriniz")]
    public string Email { get; set; } = null!;
}