using Microsoft.AspNetCore.Mvc;
using ProyectoInventarioASP.Models;
using System.Collections.Generic;
namespace ProyectoInventarioASP.Models.Models.net.Controllers;

public class MicroController : Controller
{

    ComputadoraContext context;

    public MicroController(ComputadoraContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        return View(context.MicroProcesadores);
    }

}
