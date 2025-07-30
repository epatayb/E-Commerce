using dotnet_store.Hubs;
using dotnet_store.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace dotnet_store.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IHubContext<CartHub> _hub;

    public CartController(ICartService cartService, IHubContext<CartHub> hub)
    {
        _cartService = cartService;
        _hub = hub;
    }

    public async Task<ActionResult> Index()
    {
        var customerId = _cartService.GetCustomerId();
        var cart = await _cartService.GetCart(customerId);

        return View(cart);
    }

    [HttpPost]
    public async Task<ActionResult> AddToCart(int productId, int quantity = 1)
    {
        var result = await _cartService.AddToCart(productId, quantity);
        TempData["Message"] = result.Message;
        TempData["MessageType"] = result.Type;

        return RedirectToAction("Index", "Cart");
    }

    [HttpPost]
    public async Task<ActionResult> UpdateQuantity([FromBody] UpdateQuantityDto dto)
    {
        var result = await _cartService.UpdateQuantity(dto.ProductId, dto.Quantity);
        var userID = _cartService.GetCustomerId();
        var itemcount = _cartService.GetTotalItemCount(userID); 
        await _hub.Clients.User(userID).SendAsync("CartUpdated", userID);
        await _hub.Clients.User(userID).SendAsync("QuantityChanged", dto.ProductId, dto.Quantity);
        return Json(new
        {
            success = result.Success,
            message = result.Message,
            type = result.Type,
            userId = userID,
            itemCount = itemcount,
            updatedProductId = dto.ProductId,
            updatedQuantity = dto.Quantity
        });
    }
    [HttpPost]
    public async Task<ActionResult> RemoveItem(int productId, int quantity)
    {
        var result = await _cartService.RemoveItem(productId, quantity);
        TempData["Message"] = result.Message;
        TempData["MessageType"] = result.Type;

        return RedirectToAction("Index", "Cart");
    }

    public class UpdateQuantityDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    [HttpGet]
    public ActionResult GetCartSummary()
    {
        var cart = _cartService.GetCart(_cartService.GetCustomerId()).Result;

        return Json(new
        {
            subTotal = cart.SubTotal,
            tax = cart.Tax,
            shippingFee = cart.ShippingFee,
            total = cart.Total,
        });
    }
}