using ProyectoInventario.Modelos;
using System.Collections.Generic;
namespace ProyectoInventario.Services;

public class MicroService : IMicroService

{
    ComputadoraContext context;
    public MicroService(ComputadoraContext context)
    {
        this.context = context;
    }

    public IEnumerable<MicroProcesador> Get()
    {
        return context.MicroProcesadores;
    }

    public async Task Save(MicroProcesador micro)
    {
        context.Add(micro);

        await context.SaveChangesAsync();
    }

    public async Task Update(String id, MicroProcesador micro)
    {
        var MicroActual = context.MicroProcesadores.Find(id);

        if (MicroActual != null)
        {
            MicroActual.Marca = micro.Marca;
            micro.Tecnologia = micro.Tecnologia;

            await context.SaveChangesAsync();

        }
    }

    public async Task Delete(String id)
    {
        var MicroActual = context.MicroProcesadores.Find(id);

        if (MicroActual != null)
        {
            context.Remove(MicroActual);

            await context.SaveChangesAsync();

        }
    }
}

public interface IMicroService
{
   IEnumerable<MicroProcesador> Get();
   Task Save(MicroProcesador micro);
   Task Update(String id, MicroProcesador micro);

   Task Delete(String id);
}
