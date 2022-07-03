using Microsoft.AspNetCore.Mvc;
using ProyectoInventarioASP.Models;
using System.Collections.Generic;
namespace ProyectoInventarioASP.Models.Models.net.Controllers;

public class DisplayController : Controller
{
    ComputadoraContext context;

    public DisplayController(ComputadoraContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        return View(context.Displays);
    }

}
