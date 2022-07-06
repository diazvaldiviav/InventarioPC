using Microsoft.AspNetCore.Mvc;
using ProyectoInventarioASP.Models;
using System.Collections.Generic;
namespace ProyectoInventarioASP.Models.Models.net.Controllers;

public class ImpresoraController : Controller
{
    ComputadoraContext context;

    public ImpresoraController(ComputadoraContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        return View(context.Impresoras);
    }

    public IActionResult TodasImpresoras()
    {

        return View("TodasImpresoras", context.Impresoras);

    }

    public IActionResult Crear()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Crear(string NumInvId, string NumSerie, string Marca, Estado estado)
    {
        try
        {
            List<Impresora> NuevaImp = new List<Impresora>();
            NuevaImp.Add(new Impresora(){NumInvId = NumInvId, NumSerie = NumSerie.ToLower(), Marca = Marca.ToUpper()});
            context.Impresoras.AddRange(NuevaImp);
            context.SaveChanges();
            return View("Index", NuevaImp.FirstOrDefault());

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
    public IActionResult Borrar(Impresora impresora)
    {
        try
        {
            context.Impresoras.Remove(impresora);

            context.SaveChanges();

            return View("TodasImpresoras", context.Impresoras);
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
    public IActionResult Editar(Impresora impresora)
    {

        try
        {
            context.Impresoras.UpdateRange(impresora);
            context.SaveChanges();
            return View("TodasImpresoras", context.Impresoras);
        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel() { });
        }

    }

    public IActionResult BuscarInv()
    {
        return View();
    }

    [HttpPost]
    public IActionResult BuscarInv(Impresora impresora)
    {
        try
        {
            List<Impresora> buscadorimpr = new List<Impresora>();

            var Buscarimpr = from imp in context.Impresoras
                             where imp == impresora
                             select imp;

            buscadorimpr.AddRange(Buscarimpr);

            return View("Index", buscadorimpr.FirstOrDefault());
        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel() { });
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
            IEnumerable<Impresora> Buscarimpr = from imp in context.Impresoras
                                                where imp.Marca.Substring(0, 2).ToUpper() == marca.Substring(0, 2).ToUpper()
                                                select imp;


            return View("TodasImpresoras", Buscarimpr.ToList());
        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel() { });
        }

    }

}
