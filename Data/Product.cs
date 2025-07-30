using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Data;
public class Product {
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int Stock { get; set; }
    
    [Precision(18, 2)]
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public bool IsHome { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}