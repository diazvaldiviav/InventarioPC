using ProyectoInventario.Modelos;
using System.Collections.Generic;
namespace ProyectoInventario.Services;

public class ComputadoraService : IComputadoraService
{
    ComputadoraContext context;

    public ComputadoraService(ComputadoraContext context)
    {
        this.context = context;
    }

    public IEnumerable<Computadora> Get()
    {
        return context.Computadoras;
    }

    public async Task Save(Computadora pc)
    {
        context.Add(pc);

        await context.SaveChangesAsync();
    }

    public async Task Update(String id, Computadora pc)
    {
        var pcActual = context.Computadoras.Find(id);

        if (pcActual != null)
        {
            pcActual.NombreArea = pc.NombreArea;
            pc.NombreDepartamento = pc.NombreDepartamento;
            pc.DiscoDuroId = pc.DiscoDuroId;
            pc.MemoriaRamId = pc.MemoriaRamId;
            pc.MicroProcesadorId = pc.MicroProcesadorId;
            pc.TecladoId = pc.TecladoId;
            pc.MonitorId = pc.MonitorId;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(String id)
    {
        var pcActual = context.Computadoras.Find(id);

        if (pcActual != null)
        {
            context.Remove(pcActual);

            await context.SaveChangesAsync();
        }
    }
}

public interface IComputadoraService
{
    IEnumerable<Computadora> Get();
    Task Save(Computadora pc);
    Task Update(String id, Computadora pc);
    Task Delete(String id);
}
