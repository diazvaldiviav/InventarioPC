using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoInventarioASP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ProyectoInventarioASP.Models;

using System.Security.Claims;

namespace ProyectoInventarioASP.Controllers;

public class UserController : Controller
{

    private readonly ComputadoraContext _context;

    public UserController(ComputadoraContext context)
    {
        _context = context;
    }

    public IActionResult Access()
    {
        return View();
    }

    [Authorize(Roles = "admin")]
    public IActionResult ListaUser()
    {
        return View("ListaUser", _context.Users);
    }
    [Authorize(Roles = "admin")]
    public IActionResult Register()
    {
        return View();
    }
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(int id)
    {
        if (id == null || _context.Users == null)
        {
            return NotFound();
        }

        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0 || _context.Users == null)
        {
            return NotFound();
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(m => m.UserId == id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [Authorize(Roles = "admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Users == null)
        {
            return Problem("Entity set 'ComputadoraContext.Users'  is null.");
        }

        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(ListaUser));


    }


    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, User _user)
    {
        if (id != _user.UserId)
        {
            return NotFound();
        }

        if (_user.password != _user.ConfirmPassword)
        {
             ViewBag.Message = "No coincide la contraseña con la confirmacion";
             return View(_user);
        }

        if (_user != null)
        {
            try
            {
                if (_user.Email == null || _user.Nombre == null || _user.password == null || _user.permisos == null)
                {
                    return View(_user);
                }
                _context.Update(_user);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(_user.UserId))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }


            return RedirectToAction(nameof(ListaUser));
        }


        return View(_user);

    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> Register(User _user)
    {
        try
        {
            if (_user.Email == null || _user.Nombre == null || _user.password == null || _user.permisos == null)
            {
                return View(_user);
            }

            if (_user.password != _user.ConfirmPassword)
             {
               ViewBag.Message = "No coincide la contraseña con la confirmacion";
               return View(_user);
             }
            _context.Add(_user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListaUser));
        }
        catch (System.Exception)
        {

            return RedirectToAction("Index", "Home");
        }

    }

    //USAR REFERENCIAS Models y Data
    [HttpPost]
    public async Task<IActionResult> Access(User _user)
    {
        //return cargarUserNames().Where(item => item.Email == _correo && item.password == _clave).FirstOrDefault();

        //var usuariofinal =  _context.Users.ToList().Where(item => item.username == _user.username && item.password == _user.password).FirstOrDefault();

        var usuariofinal = await _context.Users.FirstOrDefaultAsync(m => m.username == _user.username && m.password == _user.password);


        if (usuariofinal != null)
        {

            //2.- CONFIGURACION DE LA AUTENTICACION
            List<Claim> claims1 = new List<Claim>
                {
                    new Claim(ClaimTypes.Name , usuariofinal.Nombre),
                    new Claim("UserName", usuariofinal.username),
                };
            #region AUTENTICACTION
            var claims = claims1;

            claims.Add(new Claim(ClaimTypes.Role, usuariofinal.permisos));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            #endregion


            return RedirectToAction("Index", "Computadora");
        }
        else
        {
            return View();
        }

    }

    public async Task<IActionResult> Salir()
    {
        //3.- CONFIGURACION DE LA AUTENTICACION
        #region AUTENTICACTION
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        #endregion

        return RedirectToAction("Access");

    }


    private bool UserExists(int id)
    {
        return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
    }

}