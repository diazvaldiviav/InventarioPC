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
        return View(context.Displays.FirstOrDefault());
    }

    public IActionResult TodosMonitores()
    {
        return View("TodosMonitores", context.Displays);
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(String NumInvId, String NumSerie, String Marca, Estado estado)
    {
        try
        {
            List<Display> NuevoMonitor = new List<Display>();
            NuevoMonitor.Add(new Display() { NumInvId = NumInvId, NumSerie = NumSerie.ToLower(), Marca = Marca.ToUpper(), estado = estado });
            context.Displays.AddRange(NuevoMonitor);
            context.SaveChanges();
            return View("Index", NuevoMonitor.FirstOrDefault());
        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel() { });
        }

    }

    public IActionResult Borrar()
    {

        return View();
    }

    [HttpPost]
    public IActionResult Borrar(Display display)
    {
        try
        {
            context.Displays.Remove(display);

            context.SaveChanges();

            return View("TodosMonitores", context.Displays);
        }
        catch (Exception)
        {

            return View(new ErrorViewModel() { });
        }

    }

    public IActionResult Editar()
    {

        return View();
    }

    [HttpPost]
    public IActionResult Editar(Display display)
    {
        try
        {
            context.Displays.UpdateRange(display);
            context.SaveChanges();
            return View("TodosMonitores", context.Displays);
        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel { });
        }

    }

    public IActionResult BuscarSerie()
    {
        return View("BuscarSerie");
    }

    [HttpPost]
    public IActionResult BuscarSerie(string numSerie)
    {

        try
        {
            IEnumerable<Display> BuscarMonitor = from mon in context.Displays
                                                 where mon.NumSerie == numSerie
                                                 select mon;


            return View("Index", BuscarMonitor.FirstOrDefault());
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
            IEnumerable<Display> buscarMonitor = from mon in context.Displays
                                                 where mon.Marca.Substring(0, 3).ToUpper() == marca.Substring(0, 3).ToUpper()
                                                 select mon;

            return View("TodosMonitores", buscarMonitor.ToList());
        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel { });
        }

    }


    public IActionResult BuscarInv()
    {
        return View();
    }

    [HttpPost]
    public IActionResult BuscarInv(Display display)
    {
        try
        {
            List<Display> buscadorMonitor = new List<Display>();

            var BuscarMon = from mon in context.Displays
                            where mon == display
                            select mon;

            buscadorMonitor.AddRange(BuscarMon);

            return View("Index", buscadorMonitor.FirstOrDefault());
        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel { });
        }

    }

    public IActionResult BuscarEst()
    {
        return View();
    }


    [HttpPost]
    public IActionResult BuscarEst(Estado estado)
    {
        var estadito = Convert.ToInt16(estado);
        if (estadito == 1)
        {
            IEnumerable<Display> buscarPcinact = from mon in context.Displays
                                                 where mon.estado == Estado.inactivo
                                                 select mon;

            return View("TodosMonitores", buscarPcinact.ToList());

        }
        else
        {

            IEnumerable<Display> buscarPcinact = from mon in context.Displays
                                                 where mon.estado == Estado.activo
                                                 select mon;

            return View("TodosMonitores", buscarPcinact.ToList());

        }

    }


}
