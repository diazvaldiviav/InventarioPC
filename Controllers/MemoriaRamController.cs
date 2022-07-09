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
        return View(context.MemoriasRam.FirstOrDefault());
    }

    public IActionResult TodasMemoria()
    {
        return View("TodasMemoria", context.MemoriasRam);
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(String NumSerieId, string Marca, string Tecnologia, string Capacidad, Estado estado)
    {
        try
        {

            List<MemoriaRam> NuevaMemoria = new List<MemoriaRam>();
            NuevaMemoria.Add(new MemoriaRam() { NumSerieId = NumSerieId.ToLower(), Marca = Marca.ToUpper(), Tecnologia = Tecnologia.ToUpper(), Capacidad = Capacidad.ToUpper(), estado = estado });

            context.MemoriasRam.AddRange(NuevaMemoria);
            context.SaveChanges();

            return View("Index", NuevaMemoria.FirstOrDefault());


        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel() { });
        }


    }


    public IActionResult Editar()
    {

        return View();
    }

    [HttpPost]
    public IActionResult Editar(String NumSerieId, string Marca, string Tecnologia, string Capacidad, Estado estado)
    {
        try
        {
            List<MemoriaRam> NuevaMemoria = new List<MemoriaRam>();
            NuevaMemoria.Add(new MemoriaRam() { NumSerieId = NumSerieId.ToLower(), Marca = Marca.ToUpper(), Tecnologia = Tecnologia.ToUpper(), Capacidad = Capacidad.ToUpper(), estado = estado });
            context.MemoriasRam.UpdateRange(NuevaMemoria);
            context.SaveChanges();
            return View("TodasMemorias", context.MemoriasRam);
        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel { });
        }

    }



    public IActionResult Borrar()
    {

        return View();
    }


    [HttpPost]
    public IActionResult Borrar(MemoriaRam memoria)
    {
        try
        {
            context.MemoriasRam.Remove(memoria);

            context.SaveChanges();

            return View("TodasMemorias", context.MemoriasRam);
        }
        catch (Exception)
        {

            return View(new ErrorViewModel() { });
        }


    }


    public IActionResult BuscarSerie()
    {
        return View("BuscarSerie");
    }

    [HttpPost]
    public IActionResult BuscarSerie(MemoriaRam memoriaRam)
    {

        try
        {
            List<MemoriaRam> buscadorMemoria = new List<MemoriaRam>();

            var BuscarMemoria = from memoria in context.MemoriasRam
                                where memoria == memoriaRam
                                select memoria;

            buscadorMemoria.AddRange(BuscarMemoria);

            return View("Index", buscadorMemoria.FirstOrDefault());
        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel { });
        }

    }



    public IActionResult BuscarCap()
    {
        return View();
    }



    [HttpPost]
    public IActionResult BuscarCap(string capacidad)
    {

        try
        {
            IEnumerable<MemoriaRam> buscarMemoria = from memory in context.MemoriasRam
                                                    where memory.Capacidad == capacidad
                                                    select memory;

            return View("TodasMemorias", buscarMemoria.ToList());
        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel { });
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
            IEnumerable<MemoriaRam> buscarMemoria = from memory in context.MemoriasRam
                                                    where memory.Marca.Substring(0, 3).ToUpper() == marca.Substring(0, 3).ToUpper()
                                                    select memory;

            return View("TodasMemorias", buscarMemoria.ToList());
        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel { });
        }

    }



}
