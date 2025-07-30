namespace dotnet_store.Models;

public class ProductGetModel
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string? Image { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public bool IsHome { get; set; }
    public string CategoryName { get; set; } = null!;
}