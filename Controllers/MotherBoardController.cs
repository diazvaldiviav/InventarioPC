using Microsoft.AspNetCore.Mvc;
using ProyectoInventarioASP.Models;
using System.Collections.Generic;
namespace ProyectoInventarioASP.Models.Models.net.Controllers;


public class MotherBoardController : Controller
{
  ComputadoraContext context;

    public MotherBoardController(ComputadoraContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        return View(context.MotherBoards);
    }

}
