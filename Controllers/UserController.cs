using dotnet_store.Data;
using dotnet_store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace dotnet_store.Controllers;

using Microsoft.AspNetCore.Mvc.Rendering;


[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private UserManager<AppUser> _userManager;
    private RoleManager<AppRole> _roleManager;

    public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index(string role)
    {
        var model = new List<UserBaseModel>();
        IEnumerable<AppUser> users;

        if (!string.IsNullOrEmpty(role))
        {
            users = await _userManager.GetUsersInRoleAsync(role);
        }
        else
        {
            users = _userManager.Users.ToList();
        }

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            model.Add(new UserBaseModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                Role = string.Join(", ", roles.OrderBy(x => x))
            });
        }

        ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name", role);
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Roles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser { UserName = model.Email, Email = model.Email, FullName = model.FullName };

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.SelectedRoles))
                {
                    await _userManager.AddToRoleAsync(user, model.SelectedRoles);
                }
                TempData["Message"] = $"{user.FullName} kullanıcısı eklendi.";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }

    [HttpGet]
    public async Task<ActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            TempData["Message"] = "Kullanıcı bulunamadı.";
            return RedirectToAction("Index");
        }

        ViewBag.Roles = await _roleManager.Roles.Select(i => i.Name).ToListAsync();

        return View(
            new UserEditModel
            {
                FullName = user.FullName,
                Email = user.Email!,
                SelectedRoles = await _userManager.GetRolesAsync(user)
            }
        );
    }

    [HttpPost]
    public async Task<ActionResult> Edit(string id, UserEditModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.Email = model.Email;
                user.FullName = model.FullName;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, model.Password);

                    TempData["Message"] = $"{user.FullName} kullanıcısı güncellendi.";
                    return RedirectToAction("Index");
                }


                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, roles);
                    if (model.SelectedRoles != null)
                    {
                        await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                    }
                    TempData["Message"] = $"{user.FullName} kullanıcısı güncellendi.";
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

    public async Task<ActionResult> Delete(string id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var user = await _userManager.FindByIdAsync(id);

        if (user != null)
        {
            return View(user);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<ActionResult> DeleteConfirm(string id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var user = await _userManager.FindByIdAsync(id);

        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["Message"] = $"{user.FullName} isimli kullanıcı silindi.";
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return RedirectToAction("Index");
    }
}