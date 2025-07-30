using dotnet_store.Data;
using dotnet_store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Controllers;

[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private readonly DataContext _context;
    public CategoryController(DataContext context)
    {
        _context = context;
    }
    public ActionResult Index()
    {
        var kategoriler = _context.Categories.Select(i => new CategoryGetModel
        {
            Id = i.Id,
            CategoryName = i.CategoryName,
            Url = i.Url,
            TotalProducts = i.Products.Count
        }).ToList();
        return View(kategoriler);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(CategoryCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var entity = new Category
            {
                CategoryName = model.CategoryName,
                Url = model.Url,
            };

            _context.Categories.Add(entity);
            _context.SaveChanges();

            TempData["Message"] = $"{entity.CategoryName} kategorisi oluşturuldu.";

            return RedirectToAction("Index");
        }
        return View(model);
    }


    [HttpGet]
    public ActionResult Edit(int id)
    {
        var entity = _context.Categories.Select(i => new CategoryEditModel
        {
            Id = i.Id,
            CategoryName = i.CategoryName,
            Url = i.Url
        }).FirstOrDefault(i => i.Id == id);
        return View(entity);
    }

    [HttpPost]
    public ActionResult Edit(int id, CategoryEditModel model)
    {
        if (id != model.Id)
        {
            return RedirectToAction("Index");
        }

        if (ModelState.IsValid)
        {
            var entity = _context.Categories.FirstOrDefault(i => i.Id == model.Id);
            if (entity != null)
            {
                entity.CategoryName = model.CategoryName;
                entity.Url = model.Url;

                _context.SaveChanges();

                TempData["Message"] = $"{entity.CategoryName} kategorisi güncellendi.";

                return RedirectToAction("Index");
            }
        }
        return View(model);
    }

    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Categories.FirstOrDefault(i => id == i.Id);

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

        var entity = _context.Categories.FirstOrDefault(i => id == i.Id);

        if (entity != null)
        {
            _context.Categories.Remove(entity);
            _context.SaveChanges();

            TempData["Message"] = $"{entity.CategoryName} kategorisi silindi.";
        }
        return RedirectToAction("Index");
    }

}