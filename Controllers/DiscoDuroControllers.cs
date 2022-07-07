using Microsoft.AspNetCore.Mvc;
using ProyectoInventarioASP.Models;
using System.Collections.Generic;
namespace ProyectoInventarioASP.Models.Models.net.Controllers;



public class DiscoDuroController : Controller
{
    ComputadoraContext context;

    public DiscoDuroController(ComputadoraContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        return View(context.DiscosDuro.FirstOrDefault());
    }


    public IActionResult TodosDiscosDuros()
    {
        return View("TodosDiscosDuros", context.DiscosDuro);
    }

    public IActionResult Crear()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Crear(String NumSerieId, string Marca, string TipoConexion, string Capacidad, Estado estado)
    {
        try
        {

            List<DiscoDuro> NuevoDisco = new List<DiscoDuro>();
            NuevoDisco.Add(new DiscoDuro() { NumSerieId = NumSerieId.ToLower(), Marca = Marca.ToUpper(), TipoConexion = TipoConexion.ToUpper(), Capacidad = Capacidad.ToUpper(), estado = estado });

            context.DiscosDuro.AddRange(NuevoDisco);
            context.SaveChanges();

            return View("Index", NuevoDisco.FirstOrDefault());


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
    public IActionResult Borrar(DiscoDuro discoDuro)
    {
        try
        {
            context.DiscosDuro.Remove(discoDuro);

            context.SaveChanges();

            return View("TodosDiscosDuros", context.DiscosDuro);
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
    public IActionResult Editar(String NumSerieId, string Marca, string TipoConexion, string Capacidad, Estado estado)
    {
        try
        {

            List<DiscoDuro> BuscarDisco = new List<DiscoDuro>(); 

            BuscarDisco.Add(new DiscoDuro() { NumSerieId = NumSerieId.ToLower(), Marca = Marca.ToUpper(), Capacidad = Capacidad, TipoConexion = TipoConexion.ToUpper(), estado = estado});

            context.DiscosDuro.UpdateRange(BuscarDisco);
            context.SaveChanges();
            return View("TodosDiscosDuros", context.DiscosDuro);
        }
        catch (Exception ex)
        {
            return View(ex.Message);
        }
    }

    public IActionResult BuscarSerie()
    {
        return View("BuscarSerie");
    }

    [HttpPost]
    public IActionResult BuscarSerie(DiscoDuro discoDuro)
    {

        try
        {
            List<DiscoDuro> buscadordiscoDuro = new List<DiscoDuro>();

            var BuscardiscoDuro = from disco in context.DiscosDuro
                                  where disco == discoDuro
                                  select disco;

            buscadordiscoDuro.AddRange(BuscardiscoDuro);

            return View("Index", buscadordiscoDuro.FirstOrDefault());
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
            IEnumerable<DiscoDuro> buscardisco = from disco in context.DiscosDuro
                                                 where disco.Capacidad == capacidad
                                                 select disco;

            return View("TodosDiscosDuros", buscardisco.ToList());
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
            IEnumerable<DiscoDuro> buscardisco = from disco in context.DiscosDuro
                                                 where disco.Marca.Substring(0, 3).ToUpper() == marca.Substring(0, 3).ToUpper()
                                                 select disco;

            return View("TodosDiscosDuros", buscardisco.ToList());
        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel { });
        }

    }



}
