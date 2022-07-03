using Microsoft.AspNetCore.Mvc;
using ProyectoInventarioASP.Models;
using System.Collections.Generic;
namespace ProyectoInventarioASP.Models.Models.net.Controllers;


public class MemoriaRamController : Controller
{


    ComputadoraContext context;

    public MemoriaRamController(ComputadoraContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        return View(context.MemoriasRam);
    }


}
