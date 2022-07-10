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
        try
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
                //validacion del nombre
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
                    //validacion del nombre del departamento
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
                        //validacion del nombre area
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
                            //validacion del nombre del usuario
                            IEnumerable<Computadora> BuscarNomUsuario = from pc in context.Computadoras
                                                                        where pc.NombreUsuarioId.ToLower() == NumInvId.ToLower()
                                                                        select pc;

                            var validacionNomUsua = BuscarNomUsuario.ToArray();

                            if (validacionNomUsua.Length != 0)
                            {
                                return View(BuscarNomUsuario.FirstOrDefault());
                            }
                            //Aqui comienzo a filtrar a travez de las relaciones de otras tablas
                            //Comienzo validando la memoria Ram a travez del id y la tecnologia
                            else
                            {
                                //Comienzo por la capacidad
                                //Selecciono el id a travez de la Capacidad
                                var BuscarMem = from mem in context.MemoriasRam
                                                where mem.Capacidad == NumInvId
                                                select mem.NumSerieId;

                                //Paso el id o los ids que obtengo a un array
                                var RamString = BuscarMem.ToArray();

                                //valido que el array tenga algo
                                if (RamString.Length != 0)
                                {
                                    
                                    List<Computadora> NuevaLista = new List<Computadora>();
                                     //si el array tiene algun objeto lo recorro para iterar por cada objeto 
                                    for (int i = 0; i < RamString.Length; i++)
                                    {
                                        var JoinPcRam = from pc in context.Computadoras
                                                        join ram in context.MemoriasRam
                                                        on pc.MemoriaRamId equals ram.NumSerieId
                                                        where pc.MemoriaRamId == RamString[i]
                                                        select pc;
                                    //guardamos los objetos en una lista creada anteriormente
                                        NuevaLista.AddRange(JoinPcRam);
                                    }
                                    //aqui le muestro el resultado de la lista a la vista
                                    return View("TodasComputadoras", NuevaLista);


                                }
                                else
                                {
                                    var BuscarMemTec = from mem in context.MemoriasRam
                                                       where mem.Tecnologia.ToUpper() == NumInvId.ToUpper()
                                                       select mem.NumSerieId;

                                    var RamStringTec = BuscarMemTec.ToArray();

                                    if (RamStringTec.Length != 0)
                                    {
                                        List<Computadora> NuevaListaTec = new List<Computadora>();

                                        for (int i = 0; i < RamStringTec.Length; i++)
                                        {
                                            var JoinPcRamTec = from pc in context.Computadoras
                                                               join ram in context.MemoriasRam
                                                               on pc.MemoriaRamId equals ram.NumSerieId
                                                               where pc.MemoriaRamId == RamStringTec[i]
                                                               select pc;

                                            NuevaListaTec.AddRange(JoinPcRamTec);
                                        }

                                        return View("TodasComputadoras", NuevaListaTec);

                                    }
                                    //A partir de aqui comenzare a validar por disco duro
                                    //por capacidad y tecnologia al igual que la Ram 
                                    else
                                    {
                                        
                                        
                                    }
                                }
                            }
                        }

                    }
                }

            }

        }
        catch (Exception ex)
        {
            return View();
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







