using Microsoft.AspNetCore.Mvc;
using ProyectoInventarioASP.Models;
using System.Collections.Generic;
namespace ProyectoInventarioASP.Models.Models.net.Controllers;


public class ComputadoraController : Controller
{

    ComputadoraContext context;

    public ComputadoraController(ComputadoraContext context)
    {
        this.context = context;
    }


//Vista principal de la busqueda
    public IActionResult Index()
    {
        return View(context.Computadoras.FirstOrDefault());
    }

//Interaccion con el formulario de busqueda a travez del metodo post
    [HttpPost]
                             //pasamos el inv primero como parametro
    public IActionResult Index(string NumInvId)
    {
        //aqui buscamos el id a traves del contexto
        var validacion = context.Computadoras.Find(NumInvId);
//valido el dato que me pasaron como parametro es null si no es null pues lo que nos entraron fue el id
        if (validacion != null)
        {
            //si no es null hacemos la consulta a la base de datos buscando el id
            var buscadorPC = from pc in context.Computadoras
                             where pc.NumInvId == NumInvId
                             select pc;

          //aqui devolvemos nuestro resultado a la vista
            return View(buscadorPC.FirstOrDefault());
        }
        else
        {
            IEnumerable<Computadora> BuscarPc = from pc in context.Computadoras
                                                where pc.Nombre.ToUpper() == NumInvId.ToUpper()
                                                select pc;

            var validacionNombre = BuscarPc.ToArray();

            if (validacionNombre.Length != 0)
            {
                return View(BuscarPc.FirstOrDefault());
            }
            else
            {
                IEnumerable<Computadora> BuscarNomDep = from pc in context.Computadoras
                                                        where pc.NombreDepartamento.ToUpper() == NumInvId.ToUpper()
                                                        select pc;

                var validacionNomDep = BuscarNomDep.ToArray();

                if (validacionNomDep.Length != 0)
                {
                    return View("TodasComputadoras", BuscarNomDep.ToList());
                }
                else
                {
                    IEnumerable<Computadora> BuscarNomArea = from pc in context.Computadoras
                                                             where pc.NombreArea.ToUpper() == NumInvId.ToUpper()
                                                             select pc;

                    var validacionNomArea = BuscarNomArea.ToArray();

                    if (validacionNomArea.Length != 0)
                    {
                        return View("TodasComputadoras", BuscarNomArea.ToList());
                    }

                    else
                    {
                        IEnumerable<Computadora> BuscarNomUsuario = from pc in context.Computadoras
                                                                    where pc.NombreUsuarioId.ToLower() == NumInvId.ToLower()
                                                                    select pc;

                        var validacionNomUsua = BuscarNomUsuario.ToArray();

                        if (validacionNomUsua.Length != 0)
                        {
                            return View(BuscarNomUsuario.FirstOrDefault());
                        }
                    }

                }
            }

        }

        return View();
    }



    public IActionResult TodasComputadoras()
    {

        return View("TodasComputadoras", context.Computadoras);

    }




    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Create(string NumInvId,
                                  string NombreDepartamento,
                                  string NombreArea,
                                  string Nombre,
                                  Estado estado,
                                  string MemoriaRamId,
                                  string NumIp,
                                  string Mac,
                                  string NombreUsuarioId,
                                  string ImpresoraId,
                                  string DiscoDuroId,
                                  string MicroProcesadorId,
                                  string MotherBoardId,
                                  string MonitorId,
                                  string TecladoId)



    {
        try
        {

            List<Computadora> NuevaPC = new List<Computadora>();
            NuevaPC.Add(new Computadora()
            {
                NumInvId = NumInvId,
                NombreDepartamento = NombreDepartamento.ToUpper(),
                NombreArea = NombreArea.ToUpper(),
                Nombre = Nombre.ToUpper(),
                estado = estado,
                MemoriaRamId = MemoriaRamId.ToLower(),
                Mac = Mac.ToLower(),
                NumIp = NumIp,
                ImpresoraId = ImpresoraId.ToLower(),
                NombreUsuarioId = NombreUsuarioId.ToLower(),
                DiscoDuroId = DiscoDuroId.ToLower(),
                MicroProcesadorId = MicroProcesadorId.ToLower(),
                MotherBoardId = MotherBoardId.ToLower(),
                MonitorId = MonitorId,
                TecladoId = TecladoId
            });



            context.Computadoras.AddRange(NuevaPC);
            context.SaveChanges();
            return View("Index", NuevaPC.FirstOrDefault());

        }
        catch (Exception ex)
        {
            return View(new ErrorViewModel() { });
        }

    }

    public IActionResult Delete()
    {

        return View();
    }




    [HttpPost]
    public IActionResult Delete(Computadora computadora)
    {
        try
        {
            context.Computadoras.Remove(computadora);

            context.SaveChanges();

            return View("TodasComputadoras", context.Computadoras);

        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel { });
        }




    }



    public IActionResult Edit()
    {

        return View();
    }


    [HttpPost]
    public IActionResult Edit(string NumInvId,
                                  string NombreDepartamento,
                                  string NombreArea,
                                  string Nombre,
                                  Estado estado,
                                  string MemoriaRamId,
                                  string NumIp,
                                  string Mac,
                                  string NombreUsuarioId,
                                  string ImpresoraId,
                                  string DiscoDuroId,
                                  string MicroProcesadorId,
                                  string MotherBoardId,
                                  string MonitorId,
                                  string TecladoId)
    {
        try
        {
            List<Computadora> BuscarPc = new List<Computadora>();
            BuscarPc.Add(new Computadora()
            {
                NumInvId = NumInvId,
                NombreDepartamento = NombreDepartamento.ToUpper(),
                NombreArea = NombreArea.ToUpper(),
                Nombre = Nombre.ToUpper(),
                estado = estado,
                MemoriaRamId = MemoriaRamId.ToLower(),
                Mac = Mac.ToLower(),
                NumIp = NumIp,
                ImpresoraId = ImpresoraId.ToLower(),
                NombreUsuarioId = NombreUsuarioId.ToLower(),
                DiscoDuroId = DiscoDuroId.ToLower(),
                MicroProcesadorId = MicroProcesadorId.ToLower(),
                MotherBoardId = MotherBoardId.ToLower(),
                MonitorId = MonitorId,
                TecladoId = TecladoId
            });
            context.Computadoras.UpdateRange(BuscarPc);
            context.SaveChanges();
            return View("TodasComputadoras", context.Computadoras);
        }
        catch (Exception ex)
        {

            return View(ex.Message);
        }

    }


    public IActionResult BuscarInv()
    {
        return View();
    }


    [HttpPost]
    public IActionResult BuscarInv(Computadora PC)
    {
        try
        {
            List<Computadora> buscadorPC = new List<Computadora>();

            var BuscarPC = from pc in context.Computadoras
                           where pc == PC
                           select pc;

            buscadorPC.AddRange(BuscarPC);

            return View("Index", buscadorPC.FirstOrDefault());
        }
        catch (Exception ex)
        {

            return View(new ErrorViewModel { });
        }

    }

    public IActionResult BuscarUeb()
    {
        return View();
    }


    [HttpPost]
    public IActionResult BuscarUeb(string nombre)
    {
        IEnumerable<Computadora> buscarPc = from pc in context.Computadoras
                                            where pc.Nombre.Substring(0, 3).ToUpper() == nombre.Substring(0, 3).ToUpper()
                                            select pc;

        return View("TodasComputadoras", buscarPc.ToList());
    }


    public IActionResult BuscarDep()
    {
        return View();
    }


    [HttpPost]
    public IActionResult BuscarDep(string nombre)
    {
        IEnumerable<Computadora> buscarPc = from pc in context.Computadoras
                                            where pc.NombreDepartamento.Substring(0, 3).ToUpper() == nombre.Substring(0, 3).ToUpper()
                                            select pc;


        return View("TodasComputadoras", buscarPc.ToList());
    }


    public IActionResult BuscarUser()
    {
        return View();
    }


    [HttpPost]
    public IActionResult BuscarUser(string nombre)
    {
        IEnumerable<Computadora> buscarPc = from pc in context.Computadoras
                                            where pc.NombreUsuarioId.Substring(0, 3).ToUpper() == nombre.Substring(0, 3).ToUpper()
                                            select pc;


        return View("TodasComputadoras", buscarPc.ToList());
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
            IEnumerable<Computadora> buscarPcinact = from pc in context.Computadoras
                                                     where pc.estado == Estado.inactivo
                                                     select pc;

            return View("TodasComputadoras", buscarPcinact.ToList());

        }
        else
        {

            IEnumerable<Computadora> buscarPcinact = from pc in context.Computadoras
                                                     where pc.estado == Estado.activo
                                                     select pc;

            return View("TodasComputadoras", buscarPcinact.ToList());

        }

    }




}







