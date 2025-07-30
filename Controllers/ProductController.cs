using dotnet_store.Models;
using dotnet_store.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using dotnet_store.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using dotnet_store.Hubs;

namespace dotnet_store.Controllers;

[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private readonly DataContext _context;
    private readonly IHubContext<ProductHub> _hubProduct;

    public ProductController(DataContext context, IHubContext<ProductHub> hubProduct)
    {
        _context = context;
        _hubProduct = hubProduct;
    }

    public ActionResult Index(int? category)
    {
        var query = _context.Products.AsQueryable();
        if (category != null)
        {
            query = query.Where(x => x.CategoryId == category);
        }

        var products = query.Select(x => new ProductGetModel
        {
            Id = x.Id,
            ProductName = x.ProductName,
            Price = x.Price,
            Stock = x.Stock,
            IsActive = x.IsActive,
            IsHome = x.IsHome,
            CategoryName = x.Category.CategoryName,
            Image = x.Image
        }).ToList();

        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "CategoryName", category);
        return View(products);
    }

    [AllowAnonymous]
    public ActionResult List(string url, string q)
    {
        var query = _context.Products.Where(x => x.IsActive);

        if (!string.IsNullOrEmpty(url))
        {
            query = query.Where(x => x.Category.Url == url);
        }

        if (!string.IsNullOrEmpty(q))
        {
            query = query.Where(x => x.ProductName.Trim().ToLower().Contains(q.ToLower()));

            ViewData["q"] = q;
        }

        return View(query.ToList());
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
        var product = _context.Products.Find(id);

        if (product == null)
        {
            return RedirectToAction("Index", "Home");
        }

        ViewData["BenzerUrunler"] = _context.Products
                                            .Where(i => i.IsActive && i.CategoryId == product.CategoryId && i.Id != id)
                                            .Take(4)
                                            .ToList();
        return View(product);
    }

    [HttpGet]
    public ActionResult Create()
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(ProductCreateModel model)
    {
        if (model.Image == null || model.Image.Length == 0)
        {
            ModelState.AddModelError("Image", "Resim seçmelisiniz");
        }

        if (ModelState.IsValid)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
            var fileName = await ImageHelper.SaveResizedImageAsync(model.Image!, folderPath, 375, 356);


            var entity = new Product
            {
                ProductName = model.ProductName,
                Description = model.Description,
                Price = model.Price ?? 0,
                Stock = model.Stock ?? 0,
                IsActive = model.IsActive,
                IsHome = model.IsHome,
                CategoryId = (int)model.CategoryId!,
                Image = fileName,
            };
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            await _hubProduct.Clients.All.SendAsync("ReceiveProductUpdate", new
            {
                Id = entity.Id,
                ProductName = entity.ProductName,
                Price = entity.Price,
                Stock = entity.Stock,
                Description = entity.Description
            });

            TempData["Message"] = $"{entity.ProductName} ürünü oluşturuldu.";

            return RedirectToAction("Index");
        }
        ViewBag.Categories = _context.Categories.ToList();
        return View(model);
    }

    [HttpGet]
    public ActionResult Edit(int id)
    {
        var entity = _context.Products.Select(i => new ProductEditModel
        {
            Id = i.Id,
            ProductName = i.ProductName,
            Description = i.Description,
            Stock = i.Stock,
            Price = i.Price,
            ImageName = i.Image,
            IsActive = i.IsActive,
            IsHome = i.IsHome,
            CategoryId = i.CategoryId,
        }).FirstOrDefault(i => i.Id == id);

        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "CategoryName");
        return View(entity);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(int id, ProductEditModel model)
    {
        if (id != model.Id)
        {
            return RedirectToAction("Index");
        }

        if (ModelState.IsValid)
        {
            var entity = _context.Products.FirstOrDefault(i => i.Id == model.Id);

            if (entity != null)
            {
                if (model.Image != null)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                    var fileName = await ImageHelper.SaveResizedImageAsync(model.Image!, folderPath, 375, 356);
                    entity.Image = fileName;
                }
                entity.ProductName = model.ProductName;
                entity.Price = model.Price ?? 0;
                entity.Stock = model.Stock ?? 0;
                entity.IsActive = model.IsActive;
                entity.IsHome = model.IsHome;
                entity.Description = model.Description;
                entity.CategoryId = (int)model.CategoryId!;

                await _context.SaveChangesAsync();

                // 💬 SignalR ile fiyat güncellemesini clientlara bildir
                await _hubProduct.Clients.All.SendAsync("ReceiveProductUpdate", new
                {
                    Id = entity.Id,
                    ProductName = entity.ProductName,
                    Price = entity.Price,
                    Stock = entity.Stock,
                    Description = entity.Description,
                });

                TempData["Message"] = $"{entity.ProductName} ürünü güncellendi.";

                return RedirectToAction("Index");
            }
        }
        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "CategoryName");
        return View(model);
    }

    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Products.FirstOrDefault(i => id == i.Id);

        if (entity != null)
        {
            return View(entity);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteConfirm(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Products.FirstOrDefault(i => id == i.Id);

        if (entity != null)
        {
            _context.Products.Remove(entity);
            _context.SaveChanges();

            TempData["Message"] = $"{entity.ProductName} ürünü silindi.";
        }
        return RedirectToAction("Index");
    }

}