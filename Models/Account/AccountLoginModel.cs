using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class AccountLoginModel
{

    [Display(Name = "Eposta")]
    [Required(ErrorMessage = "{0} zorunlu alan")]
    [EmailAddress(ErrorMessage = "Geçerli mail adresi giriniz")]
    public string Email { get; set; } = null!;

    [Display(Name = "Şifre")]
    [Required(ErrorMessage = "{0} zorunlu alan")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Beni Hatırla")]
    public bool rememberMe { get; set; } = true;

}