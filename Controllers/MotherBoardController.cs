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

     public IActionResult TodasBoard()
    {
        return View("TodasBoard", context.MotherBoards);
    }


     public IActionResult Crear()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Crear(String NumSerieId, string Marca, Estado estado)
    {
        try
        {

            List<MotherBoard> NuevaBoard = new List<MotherBoard>();
            NuevaBoard.Add(new MotherBoard() { NumSerieId = NumSerieId.ToLower(), Marca = Marca.ToUpper(), Tecnologia = Tecnologia.ToUpper(), estado = estado });

            context.MotherBoards.AddRange(NuevaBoard);
            context.SaveChanges();

            return View("Index", NuevaBoard.FirstOrDefault());


        }
        catch (Exception ex)
        {

            return View(ex.Message);
        }


    }

    public IActionResult Editar()
    {

        return View();
    }


     [HttpPost]
    public IActionResult Editar(String NumSerieId, string Marca, Estado estado)
    {
        try
        {

            List<MotherBoard> NuevaBoard = new List<MotherBoard>();
            NuevaBoard.Add(new MotherBoard() { NumSerieId = NumSerieId.ToLower(), Marca = Marca.ToUpper(), Tecnologia = Tecnologia.ToUpper(), estado = estado });

            context.MotherBoards.UpdateRange(NuevaBoard);
            context.SaveChanges();

            return View("TodasBoard", NuevaBoard.FirstOrDefault());


        }
        catch (Exception ex)
        {

            return View(ex.Message);
        }


    }

     public IActionResult Borrar()
    {

        return View();
    }


    [HttpPost]
    public IActionResult Borrar(MotherBoard board)
    {
        try
        {
            context.MotherBoards.Remove(board);

            context.SaveChanges();

            return View("TodasBoard", context.MotherBoards);
        }
        catch (Exception)
        {

            return View(ex.Message);
        }


    }


    public IActionResult BuscarSerie()
    {
        return View("BuscarSerie");
    }

    [HttpPost]
    public IActionResult BuscarSerie(MotherBoard board)
    {

        try
        {
            List<MotherBoard> buscadorBoard = new List<MotherBoard>();

            var buscarBoard = from board in context.MotherBoards
                              where board == board
                              select board;

            buscadorBoard.AddRange(buscarBoard);

            return View("Index", buscadorBoard.FirstOrDefault());
        }
        catch (Exception ex)
        {

            return View(ex.Message);
        }

    }


    public IActionResult BuscarMarca()
    {
        return View();
    }



    [HttpPost]
    public IActionResult BuscarMarca(string marca)
    {

        try
        {
            IEnumerable<MemoriaRam> buscarBoard = from board in context.MotherBoards
                                                  where board.Marca.Substring(0, 3).ToUpper() == marca.Substring(0, 3).ToUpper()
                                                  select board;

            return View("TodasBoard", buscarBoard.ToList());
        }
        catch (Exception ex)
        {

            return View(ex.Message);
        }

    }




}
