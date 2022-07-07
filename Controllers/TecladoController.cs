using Microsoft.AspNetCore.Mvc;
using ProyectoInventarioASP.Models;
using System.Collections.Generic;
namespace ProyectoInventarioASP.Models.Models.net.Controllers;

public class TecladoController : Controller
{
    ComputadoraContext context;

    public TecladoController(ComputadoraContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        return View(context.Teclados);
    }

    public IActionResult TodosTeclados()
    {
        return View("TodosTeclados", context.Teclados);
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(String NumInvId, String NumSerie, String Marca, string TipoConexion, Estado estado)
    {
        try
        {
            List<Teclado> NuevoTeclado = new List<Teclado>();
            NuevoTeclado.Add(new Teclado() { NumInvId = NumInvId, NumSerie = NumSerie.ToLower(), Marca = Marca.ToUpper(), TipoConexion = TipoConexion, estado = estado });
            context.Teclados.AddRange(NuevoTeclado);
            context.SaveChanges();
            return View("Index", NuevoTeclado.FirstOrDefault());
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
    public IActionResult Borrar(Teclado teclado)
    {
        try
        {
            context.Teclados.Remove(teclado);

            context.SaveChanges();

            return View("TodosTeclados", context.Teclados);
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
    public IActionResult Editar(Teclado teclado)
    {
        try
        {
            context.Teclados.UpdateRange(teclado);
            context.SaveChanges();
            return View("TodosTeclados", context.Teclados);
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
            IEnumerable<Teclado> BuscarTeclado = from tecl in context.Teclados
                                                 where tecl.NumSerie == numSerie
                                                 select tecl;


            return View("Index", BuscarTeclado.FirstOrDefault());
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
            IEnumerable<Teclado> BuscarTeclado = from tecl in context.Teclados
                                                 where tecl.Marca.Substring(0, 3).ToUpper() == marca.Substring(0, 3).ToUpper()
                                                 select tecl;

            return View("TodoosTeclados", BuscarTeclado.ToList());
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
    public IActionResult BuscarInv(Teclado teclado)
    {
        try
        {
            List<Teclado> buscadorTeclado = new List<Teclado>();

            var Buscartecl = from tecl in context.Teclados
                             where tecl == teclado
                             select tecl;

            buscadorTeclado.AddRange(Buscartecl);

            return View("Index", buscadorTeclado.FirstOrDefault());
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
            IEnumerable<Teclado> buscarPcinact = from tecl in context.Teclados
                                                 where tecl.estado == Estado.inactivo
                                                 select tecl;

            return View("TodosTeclados", buscarPcinact.ToList());

        }
        else
        {

            IEnumerable<Teclado> buscarPcinact = from tecl in context.Teclados
                                                 where tecl.estado == Estado.activo
                                                 select tecl;

            return View("TodosTeclados", buscarPcinact.ToList());

        }

    }




}
