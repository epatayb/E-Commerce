using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Data;

public class Order
{
    public int Id { get; set; }

    // Kullanıcı Bilgisi eposta
    public string CustomerId { get; set; } = null!;

    public DateTime OrderDate { get; set; } = DateTime.Now;

    // Teslimat Bilgileri
    public string ReceiverFullName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? OrderNote { get; set; }

    [Precision(18, 2)]
    public decimal TotalPrice { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new();

    public decimal SubTotal => OrderItems.Sum(x => x.UnitPrice * x.Quantity);
    public decimal ShippingFee => SubTotal >= 999 ? 0 : 50;
    public decimal TaxRate => 0.2m;
    public decimal Tax => SubTotal * TaxRate;
    public decimal Total => SubTotal + ShippingFee + Tax;
}

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }
    
    [Precision(18, 2)]
    public decimal UnitPrice { get; set; } // sipariş anındaki fiyat
    public decimal TotalPrice => Quantity * UnitPrice;
}