using dotnet_store.Data;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_store.ViewComponents;

public class Navbar : ViewComponent
{
    private readonly DataContext _context;

    public Navbar(DataContext context)
    {
        _context = context;
    }
    public IViewComponentResult Invoke()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }
}