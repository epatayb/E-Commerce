using System.Threading.Tasks;
using dotnet_store.Data;
using dotnet_store.Models;
using dotnet_store.Services;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;

namespace dotnet_store.Controllers;

[Authorize]
public class OrderController : Controller
{
    private ICartService _cartService;
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;

    public OrderController(ICartService cartService, DataContext context, IConfiguration configuration)
    {
        _cartService = cartService;
        _context = context;
        _configuration = configuration;
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Index()
    {
        return View(_context.Orders.ToList());
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Details(int id)
    {
        var order = _context.Orders
                        .Include(i => i.OrderItems)
                        .ThenInclude(i => i.Product)
                        .FirstOrDefault(i => i.Id == id);

        return View(order);
    }

    public async Task<ActionResult> Checkout()
    {
        ViewBag.Cart = await _cartService.GetCart(User.Identity?.Name!);
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Checkout(OrderCreateModel model)
    {
        var username = User.Identity?.Name!;
        var cart = await _cartService.GetCart(username);

        if (cart.CartItems.Count == 0)
        {
            ModelState.AddModelError("", "Sepetinizde ürün yok");
        }
        if (ModelState.IsValid)
        {
            var order = new Order
            {
                ReceiverFullName = model.ReceiverFullName,
                City = model.City,
                Phone = model.Phone,
                Address = model.Address,
                PostalCode = model.PostalCode,
                OrderNote = model.OrderNote,
                OrderDate = DateTime.Now,
                TotalPrice = cart.Total,
                CustomerId = username,
                OrderItems = cart.CartItems.Select(x => new Data.OrderItem
                {
                    ProductId = x.ProductId,
                    UnitPrice = x.Product.Price,
                    Quantity = x.Quantity
                }).ToList()
            };

            var payment = await ProcessPayment(model, cart);
            if (payment.Status == "success")
            {
                _context.Orders.Add(order);
                _context.Carts.Remove(cart);

                await _context.SaveChangesAsync();

                return RedirectToAction("Completed", new { orderId = order.Id });
            }
            else
            {
                ModelState.AddModelError("", "Ödeme işlemi başarısız oldu: " + payment.ErrorMessage);
            }
        }
        ViewBag.Cart = cart;

        return View(model);
    }

    public ActionResult Completed(string orderId)
    {
        return View("Completed", orderId);
    }

    public async Task<ActionResult> OrderList()
    {
        var username = User.Identity?.Name;
        var orders = await _context.Orders
            .Include(i => i.OrderItems)
            .ThenInclude(i => i.Product)
            .ThenInclude(i => i.Category)
            .Where(i => i.CustomerId == username)
            .ToListAsync();

        return View(orders);
    }
    private async Task<Payment> ProcessPayment(OrderCreateModel model, Cart cart)
    {
        Options options = new Options();
        options.ApiKey = _configuration["PaymentAPI:APIKey"];
        options.SecretKey = _configuration["PaymentAPI:SecretKey"];
        options.BaseUrl = "https://sandbox-api.iyzipay.com";

        CreatePaymentRequest request = new CreatePaymentRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = Guid.NewGuid().ToString();
        request.Price = cart.SubTotal.ToString("F2", CultureInfo.InvariantCulture);
        request.PaidPrice = cart.SubTotal.ToString("F2", CultureInfo.InvariantCulture);
        request.Currency = Currency.TRY.ToString();
        request.Installment = 1;
        request.BasketId = "B67832";
        request.PaymentChannel = PaymentChannel.WEB.ToString();
        request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        PaymentCard paymentCard = new PaymentCard();
        paymentCard.CardHolderName = model.CardHolderName;
        paymentCard.CardNumber = model.CardNumber;
        paymentCard.ExpireMonth = model.CardExpireMonth;
        paymentCard.ExpireYear = model.CardExpireYear;
        paymentCard.Cvc = model.CardCvv;
        paymentCard.RegisterCard = 0;
        request.PaymentCard = paymentCard;

        Buyer buyer = new Buyer();
        buyer.Id = "BY789";
        buyer.Name = model.ReceiverFullName;
        buyer.Surname = "Doe";
        buyer.GsmNumber = model.Phone;
        buyer.Email = "email@email.com";
        buyer.IdentityNumber = "74300864791";
        buyer.LastLoginDate = "2015-10-05 12:43:35";
        buyer.RegistrationDate = "2013-04-21 15:12:09";
        buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        buyer.Ip = "85.34.78.112";
        buyer.City = model.City;
        buyer.Country = "Turkey";
        buyer.ZipCode = model.PostalCode;
        request.Buyer = buyer;

        Address address = new Address();
        address.ContactName = model.ReceiverFullName;
        address.City = model.City;
        address.Country = "Turkey";
        address.Description = model.Address;
        address.ZipCode = model.PostalCode;
        request.ShippingAddress = address;
        request.BillingAddress = address;

        List<BasketItem> basketItems = new List<BasketItem>();

        foreach (var item in cart.CartItems)
        {
            BasketItem basketItem = new BasketItem();
            basketItem.Id = item.CartItemId.ToString();
            basketItem.Name = item.Product.ProductName;
            basketItem.Category1 = "Collectibles";
            basketItem.Category2 = "Accessories";
            basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            basketItem.Price = item.Product.Price.ToString("F2", CultureInfo.InvariantCulture);

            basketItems.Add(basketItem);
        }
        request.BasketItems = basketItems;

        return await Payment.Create(request, options);
    }
}