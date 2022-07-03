using Microsoft.AspNetCore.Mvc;
using ProyectoInventarioASP.Models;
using System.Collections.Generic;
namespace ProyectoInventarioASP.Models.Models.net.Controllers;

public class UsuarioController : Controller
{
    ComputadoraContext context;

    public UsuarioController(ComputadoraContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        return View(context.Usuarios);
    }

}
