using Microsoft.AspNetCore.Mvc;
using ProyectoInventarioASP.Models;
using System.Collections.Generic;
namespace ProyectoInventarioASP.Models.Models.net.Controllers;

public class UsuarioController : Controller
{
    ComputadoraContext context;

    public UsuarioController(ComputadoraContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        return View(context.Usuarios);
    }

    public IActionResult TodosUsuarios()
    {
        return View("TodosUsuarios", context.Usuarios);
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(String NombreUsuarioId, String NombreCompleto, String NombreDepartamento, string NombreArea)
    {
        try
        {
            List<Usuario> NuevoUsuario = new List<Usuario>();
            NuevoUsuario.Add(new Usuario() { NombreUsuarioId = NombreUsuarioId.ToLower(), NombreCompleto = NombreCompleto.ToUpper(), NombreDepartamento = NombreDepartamento.ToUpper(), NombreArea = NombreArea.ToUpper() });
            context.Usuarios.AddRange(NuevoUsuario);
            context.SaveChanges();
            return View("Index", NuevoUsuario.FirstOrDefault());
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
    public IActionResult Borrar(Usuario usuario)
    {
        try
        {
            context.Usuarios.Remove(usuario);

            context.SaveChanges();

            return View("TodosUsuarios", context.Usuarios);
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
    public IActionResult Editar(String NombreUsuarioId, String NombreCompleto, String NombreDepartamento, string NombreArea)
    {
        try
        {
            List<Usuario> NuevoUsuario = new List<Usuario>();
            NuevoUsuario.Add(new Usuario() { NombreUsuarioId = NombreUsuarioId.ToLower(), NombreCompleto = NombreCompleto.ToUpper(), NombreDepartamento = NombreDepartamento.ToUpper(), NombreArea = NombreArea.ToUpper() });
            context.Usuarios.UpdateRange(NuevoUsuario);
            context.SaveChanges();
            return View("Index", context.Usuarios.FirstOrDefault());
        }
        catch (Exception ex)
        {

            return View(ex.Message);
        }

    }

    public IActionResult BuscarNom()
    {
        return View("BuscarNom");
    }

    [HttpPost]
    public IActionResult BuscarNom(string NombreUsuarioId)
    {

        try
        {
            IEnumerable<Usuario> Buscarnombre = from nom in context.Usuarios
                                                 where nom.NombreUsuarioId == NombreUsuarioId
                                                 select nom;


            return View("Index", Buscarnombre.FirstOrDefault());
        }
        catch (Exception ex)
        {

            return View(ex.Message);
        }

    }


    public IActionResult BuscarDep()
    {
        return View("BuscarDep");
    }

    [HttpPost]
    public IActionResult BuscarDep(string NombreDepartamento)
    {

        try
        {
            IEnumerable<Usuario> Buscarnombre = from nom in context.Usuarios
                                                 where nom.NombreDepartamento == NombreDepartamento
                                                 select nom;


            return View("TodosUsuarios", Buscarnombre.ToList());
        }
        catch (Exception ex)
        {

            return View(ex.Message);
        }

    }


     public IActionResult BuscarArea()
    {
        return View("BuscarArea");
    }

    [HttpPost]
    public IActionResult BuscarArea(string NombreArea)
    {

        try
        {
            IEnumerable<Usuario> Buscarnombre = from nom in context.Usuarios
                                                 where nom.NombreArea == NombreArea
                                                 select nom;


            return View("TodosUsuarios", Buscarnombre.ToList());
        }
        catch (Exception ex)
        {

            return View(ex.Message);
        }

    }


}
