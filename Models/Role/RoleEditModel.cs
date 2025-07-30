using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class RoleEditModel(){

    public string Id { get; set; } = null!;

    [Display(Name = "Role Adı")]
    [Required(ErrorMessage = "{0} boş geçilemez.")]
    [StringLength(30)]
    public string RoleName { get; set;} = null!;

}