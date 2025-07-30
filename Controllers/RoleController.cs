using dotnet_store.Models;
using dotnet_store.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace dotnet_store.Controllers;

[Authorize(Roles = "Admin")]

public class RoleController : Controller
{
    private RoleManager<AppRole> _roleManager;
    private UserManager<AppUser> _userManager;

    public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View(_roleManager.Roles);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoleCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var role = new AppRole { Name = model.RoleName };
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                TempData["Message"] = $"{role.Name} rolü eklendi.";

                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    [HttpGet]
    public async Task<ActionResult> Edit(string id)
    {
        var entity = await _roleManager.FindByIdAsync(id);

        if (entity != null)
        {
            return View(new RoleEditModel { Id = entity.Id, RoleName = entity.Name! });
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<ActionResult> Edit(string id, RoleEditModel model)
    {
        if (id != model.Id)
        {
            return RedirectToAction("Index");
        }

        if (ModelState.IsValid)
        {
            var entity = await _roleManager.FindByIdAsync(id);
            if (entity != null)
            {
                entity.Name = model.RoleName;

                var result = await _roleManager.UpdateAsync(entity);

                if (result.Succeeded)
                {
                    TempData["Message"] = $"{entity.Name} rol bilgisi güncellendi.";
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View(model);
    }

    [HttpGet]
    public async Task<ActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = await _roleManager.FindByIdAsync(id);

        if (entity != null)
        {
            ViewBag.Users = await _userManager.GetUsersInRoleAsync(entity.Name!);
            return View(entity);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<ActionResult> DeleteConfirm(string? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = await _roleManager.FindByIdAsync(id);

        if (entity != null)
        {
            await _roleManager.DeleteAsync(entity);

            TempData["Message"] = $"{entity.Name} rolü silindi.";
        }
        return RedirectToAction("Index");
    }


}