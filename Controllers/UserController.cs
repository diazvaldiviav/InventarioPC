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

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Register(User _user)
    {
        List<User> nuevoUsuario = new List<User>();
        nuevoUsuario.Add(new User()
        {
            UserId = Guid.NewGuid().ToString(),
            username = _user.username,
            Nombre = _user.Nombre,
            password = _user.password,
            Email = _user.Email,
            permisos = "admin"
        });
        _context.AddRange(nuevoUsuario);
        await _context.SaveChangesAsync();

        var autentUser = nuevoUsuario.Where(item => item.Email == _user.Email && item.password == _user.password).FirstOrDefault();

        if (autentUser != null)
        {

            //2.- CONFIGURACION DE LA AUTENTICACION
            List<Claim> claims1 = new List<Claim>
                {
                    new Claim(ClaimTypes.Name , autentUser.Nombre),
                    new Claim("Correo", autentUser.Email),
                };
            #region AUTENTICACTION
            var claims = claims1;

            claims.Add(new Claim(ClaimTypes.Role, autentUser.permisos));

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

    //USAR REFERENCIAS Models y Data
    [HttpPost]
    public async Task<IActionResult> Index(User _user)
    {
        //return cargarUserNames().Where(item => item.Email == _correo && item.password == _clave).FirstOrDefault();
        List<User> usuario = new List<User>();
        var newuser = from user in _context.Users
                      where user.Email == _user.Email && user.password == _user.password
                      select user;

        usuario.AddRange(newuser);

        var usuariofinal = usuario.Where(item => item.Email == _user.Email && item.password == _user.password).FirstOrDefault();



        if (usuariofinal != null)
        {

            //2.- CONFIGURACION DE LA AUTENTICACION
            List<Claim> claims1 = new List<Claim>
                {
                    new Claim(ClaimTypes.Name , usuariofinal.Nombre),
                    new Claim("Correo", usuariofinal.Email),
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

        return RedirectToAction("Index");

    }

}