using System.Threading.Tasks;
using dotnet_store.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Services;

public interface ICartService
{
    string GetCustomerId();
    Task<Cart> GetCart(string customerId);
    Task<ServiceResult> AddToCart(int productId, int quantity = 1);
    Task<ServiceResult> RemoveItem(int productId, int quantity = 1);
    Task TransferCartToUser(string userName);
    Task<ServiceResult> UpdateQuantity(int productId, int quantity);
    Task<int> GetTotalItemCount(string userId);
}

public class CartService : ICartService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Cart> GetCart(string custId)
    {
        var cart = await _context.Carts
                            .Include(i => i.CartItems)
                            .ThenInclude(i => i.Product)
                            .Where(i => i.CustomerId == custId)
                            .FirstOrDefaultAsync();

        if (cart == null)
        {
            var customerId = _httpContextAccessor.HttpContext?.User.Identity?.Name;

            if (string.IsNullOrEmpty(customerId))
            {
                customerId = Guid.NewGuid().ToString();

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddMonths(1),
                    IsEssential = true,
                };
                _httpContextAccessor.HttpContext?.Response.Cookies.Append("customerId", customerId, cookieOptions);
            }
            cart = new Cart { CustomerId = customerId };
            _context.Carts.Add(cart);
        }
        return cart;
    }

    public string GetCustomerId()
    {
        var context = _httpContextAccessor.HttpContext;
        return context?.User.Identity?.Name ?? context?.Request.Cookies["customerId"]!;
    }

    public async Task<ServiceResult> AddToCart(int productId, int quantity = 1)
    {
        var cart = await GetCart(GetCustomerId());

        var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == productId);

        if (product != null)
        {
            cart.AddItem(product, quantity);
            await _context.SaveChangesAsync();

            return new ServiceResult
            {
                Success = true,
                Message = $"{product.ProductName} sepete eklendi",
                Type = "success"
            };
        }
        return new ServiceResult
        {
            Success = false,
            Message = "Ürün sepete eklenirken bir hata oluştu. Tekrar deneyiniz.",
            Type = "error"
        };
    }

    public async Task<ServiceResult> RemoveItem(int productId, int quantity = 1)
    {
        var cart = await GetCart(GetCustomerId());

        var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == productId);

        if (product != null)
        {
            cart.DeleteItem(productId, quantity);
            await _context.SaveChangesAsync();

            return new ServiceResult
            {
                Success = true,
                Message = $"{product.ProductName} sepetinizden silindi.",
                Type = "warning"
            };
        }
        return new ServiceResult
        {
            Success = false,
            Message = "Ürün sepetinizden silinirken bir hata oluştu. Tekrar Deneyiniz.",
            Type = "error"
        };
    }

    public async Task TransferCartToUser(string userName)
    {
        var userCart = await GetCart(userName);

        var cookieCart = await GetCart(_httpContextAccessor.HttpContext?.Request.Cookies["customerId"]!);

        foreach (var item in cookieCart?.CartItems!)
        {
            var cartItem = userCart?.CartItems.Where(i => i.ProductId == item.ProductId).FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.Quantity += item.Quantity;
            }
            else
            {
                userCart?.CartItems.Add(new CartItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                });
            }
        }

        _context.Carts.Remove(cookieCart);

        await _context.SaveChangesAsync();
    }

    public async Task<ServiceResult> UpdateQuantity(int productId, int quantity)
    {
        var cart = await GetCart(GetCustomerId());
        var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);

        if (item == null)
        {
            return new ServiceResult
            {
                Success = false,
                Message = "Ürün bulunamadı",
                Type = "error"
            };
        }

        item.Quantity = quantity;
        await _context.SaveChangesAsync();

        return new ServiceResult
        {
            Success = true,
            Message = "Ürün miktarı güncellendi",
            Type = "success"
        };
    }

    public async Task<int> GetTotalItemCount(string userId)
    {
        var cart = await GetCart(userId);
        return cart?.CartItems.Sum(i => i.Quantity) ?? 0;
    }
}