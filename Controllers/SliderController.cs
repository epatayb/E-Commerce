using dotnet_store.Helpers;
using dotnet_store.Models;
using dotnet_store.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_store.Controllers;

[Authorize(Roles = "Admin")]

public class SliderController : Controller
{
    private readonly DataContext _context;
    public SliderController(DataContext context)
    {
        _context = context;
    }
    public ActionResult Index()
    {
        var sliders = _context.Sliders.Select(x => new SliderGetModel
        {
            Id = x.Id,
            Title = x.Title,
            Image = x.Image,
            Index = x.Index,
            IsActive = x.IsActive,

        }).ToList();
        return View(sliders);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(SliderCreateModel model)
    {
        if (model.Image == null || model.Image.Length == 0)
        {
            ModelState.AddModelError("Image", "Resim seçmelisiniz");
        }

        if (ModelState.IsValid)
        {
            var isDuplicate = _context.Sliders.Any(s => s.Index == model.Index);

            if (isDuplicate)
            {
                ModelState.AddModelError("Index", "Bu index başka bir slider tarafından kullanılıyor.");
                return View(model);
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
            var fileName = await ImageHelper.SaveResizedImageAsync(model.Image!, folderPath, 960, 356);

            var entity = new Slider
            {
                Title = model.Title,
                Description = model.Description,
                Index = model.Index,
                IsActive = model.IsActive,
                Image = fileName,
            };
            _context.Sliders.Add(entity);
            _context.SaveChanges();

            TempData["Message"] = $"{entity.Title} slider başarıyla oluşturuldu.";

            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpGet]
    public ActionResult Edit(int id)
    {
        var entity = _context.Sliders.Select(i => new SliderEditModel
        {
            Id = i.Id,
            Title = i.Title,
            Index = i.Index,
            Description = i.Description,
            ImageName = i.Image,
            IsActive = i.IsActive,
        }).FirstOrDefault(i => i.Id == id);

        return View(entity);
    }


    [HttpPost]
    public async Task<ActionResult> Edit(int id, SliderEditModel model)
    {
        if (id != model.Id)
        {
            return RedirectToAction("Index");
        }

        if (ModelState.IsValid)
        {
            var isDuplicate = _context.Sliders.Any(s => s.Index == model.Index && s.Id != model.Id);

            if (isDuplicate)
            {
                ModelState.AddModelError("Index", "Bu index başka bir slider tarafından kullanılıyor.");
                return View(model);
            }
            var entity = _context.Sliders.FirstOrDefault(i => i.Id == model.Id);

            if (entity != null)
            {
                if (model.Image != null)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                    var fileName = await ImageHelper.SaveResizedImageAsync(model.Image!, folderPath, 960, 356);

                    entity.Image = fileName;
                }
                entity.Description = model.Description;
                entity.Title = model.Title;
                entity.Index = model.Index;
                entity.IsActive = model.IsActive;

                _context.SaveChanges();

                TempData["Message"] = $"{entity.Title} Slider güncellendi.";

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

        var entity = _context.Sliders.FirstOrDefault(i => id == i.Id);

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

        var entity = _context.Sliders.FirstOrDefault(i => id == i.Id);

        if (entity != null)
        {
            _context.Sliders.Remove(entity);
            _context.SaveChanges();

            TempData["Message"] = $"{entity.Title} Slider silindi.";
        }
        return RedirectToAction("Index");
    }
}