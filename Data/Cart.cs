namespace dotnet_store.Data;

public class Cart
{
    public int CartId { get; set; }
    public string CustomerId { get; set; } = null!;
    public List<CartItem> CartItems { get; set; } = new();

    // Ara Toplam 
    // => Fiyat * adet 
    // Vergisiz Fiyatı
    public decimal SubTotal => CartItems.Sum(x => x.Product.Price * x.Quantity);

    // Kargo ücreti => 999 tl'ye kadar 50 tl, sonrası ücretsiz
    public decimal ShippingFee => SubTotal >= 999 ? 0 : 50;

    // Vergi Oranı %20
    public decimal TaxRate => 0.2m;

    public decimal Tax => SubTotal * TaxRate;

    // Herşey dahil Toplam
    // Kargo ücreti + vergi 
    public decimal Total => SubTotal + ShippingFee + Tax;

    public void AddItem(Product product, int quantity)
    {
        var item = CartItems.Where(i => i.ProductId == product.Id).FirstOrDefault();

        if (item != null)
        {
            //Ürün sepette bulunuyorsa sadece miktarını arttır.
            item.Quantity += quantity;
        }
        else
        {
            //Ürün sepete ilk defa yükleniyorsa.
            CartItems.Add(new CartItem
            {
                Product = product,
                Quantity = quantity
            });
        }
    }

    public void DeleteItem(int productId, int quantity)
    {
        var item = CartItems.Where(i => i.ProductId == productId).FirstOrDefault();

        if (item != null)
        {
            item.Quantity -= quantity;

            if (item.Quantity <= 0)
            {
                CartItems.Remove(item);
            }
        }

    }
}

public class CartItem
{
    public int CartItemId { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int CartId { get; set; }
    public Cart Cart { get; set; } = null!;

    public int Quantity { get; set; }
}