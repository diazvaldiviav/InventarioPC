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

    public IActionResult TodosMicros()
    {
        return View("TodosMicros", context.MicroProcesadores);
    }


     public IActionResult Crear()
    {
      return View();
    }


    [HttpPost]
    public IActionResult Crear(String NumSerieId, string Marca, string Tecnologia, Estado estado)
    {
        try
        {

            List<MicroProcesador> NuevoMicro = new List<MicroProcesador>();
            NuevoMicro.Add(new MicroProcesador() { NumSerieId = NumSerieId.ToLower(), Marca = Marca.ToUpper(), Tecnologia = Tecnologia.ToUpper(), estado = estado });

            context.MicroProcesadores.AddRange(NuevoMicro);
            context.SaveChanges();

            return View("Index", NuevaMemoria.FirstOrDefault());


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
    public IActionResult Editar(String NumSerieId, string Marca, string Tecnologia, Estado estado)
    {
        try
        {

            List<MicroProcesador> NuevoMicro = new List<MicroProcesador>();
            NuevoMicro.Add(new MicroProcesador() { NumSerieId = NumSerieId.ToLower(), Marca = Marca.ToUpper(), Tecnologia = Tecnologia.ToUpper(), estado = estado });

            context.MicroProcesadores.UpdateRange(NuevoMicro);
            context.SaveChanges();

            return View("TodosMicros", NuevaMemoria.FirstOrDefault());


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
    public IActionResult Borrar(MicroProcesador micro)
    {
        try
        {
            context.MicroProcesadores.Remove(micro);

            context.SaveChanges();

            return View("TodosMicros", context.MicroProcesadores);
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
    public IActionResult BuscarSerie(MicroProcesador micro)
    {

        try
        {
            List<MicroProcesador> buscadorMicro = new List<MicroProcesador>();

            var buscarMicro = from micro in context.MicroProcesadores
                              where micro == micro
                              select micro;

            buscadorMicro.AddRange(BuscarMicro);

            return View("Index", buscadorMicro.FirstOrDefault());
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
            IEnumerable<MemoriaRam> buscarMicro = from micro in context.MicroProcesadores
                                                  where micro.Marca.Substring(0, 3).ToUpper() == marca.Substring(0, 3).ToUpper()
                                                  select micro;

            return View("TodosMicros", buscarMicro.ToList());
        }
        catch (Exception ex)
        {

            return View(ex.Message);
        }

    }




}
