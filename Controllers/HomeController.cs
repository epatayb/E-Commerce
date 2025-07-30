using Microsoft.AspNetCore.Mvc;
using dotnet_store.Data;


namespace dotnet_store.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _context;
    public HomeController(DataContext context)
    {
        _context = context;
    }
    public ActionResult Index()
    {
        var products = _context.Products.Where(x => x.IsHome).ToList();
        ViewData["Categories"] = _context.Categories.ToList();
        return View(products);
    }
}
