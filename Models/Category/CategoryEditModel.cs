using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class CategoryEditModel(){

    public int Id { get; set; }

    [Display(Name = "Kategori adı")]
    [Required(ErrorMessage = "{0} boş geçilemez.")]
    [StringLength(30, ErrorMessage = "{0} için {2}-{1} karakter aralığında değer girmelisiniz.", MinimumLength = 2)]
    public string CategoryName { get; set;} = null!;
 
    [Display(Name = "URL")]
    [Required(ErrorMessage = "{0} boş geçilemez.")]
    [StringLength(30, ErrorMessage = "{0} maksimum {1} karakter olmalıdır.")]
    public string Url { get; set;} = null!;
}