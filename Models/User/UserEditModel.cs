using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class UserEditModel : UserBaseModel
{
    [Display(Name = "Şifre")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Şifre Tekrar")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor!")]
    public string? ConfirmPassword { get; set; }

    public IList<String>? SelectedRoles { get; set; }
}