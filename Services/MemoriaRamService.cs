using ProyectoInventario.Modelos;
using System.Collections.Generic;
namespace ProyectoInventario.Services;

public class MemoriaRamService : IMemoriaRamService
{
    ComputadoraContext context;

    public MemoriaRamService(ComputadoraContext context)
    {
        this.context = context;
    }

    public IEnumerable<MemoriaRam> Get()
    {
        return context.MemoriasRam;
    }

    public async Task Save(MemoriaRam memoriaRam)
    {
        context.Add(memoriaRam);

        await context.SaveChangesAsync();
    }

    public async Task Update(String id, MemoriaRam memoriaRam)
    {
      var memoriaRamActual = context.MemoriasRam.Find(id);

      if (memoriaRamActual != null)
      {
        memoriaRamActual.Marca = memoriaRam.Marca;
        memoriaRam.Tecnologia = memoriaRam.Tecnologia;
        memoriaRam.Capacidad = memoriaRam.Capacidad;

        await context.SaveChangesAsync();
      }
    }

    public async Task Delete(String id)
    {
      var memoriaRamActual = context.MemoriasRam.Find(id);

      if (memoriaRamActual != null)
      {
        context.Remove(memoriaRamActual);

        await context.SaveChangesAsync();
      }
    }

}

public interface IMemoriaRamService
{
   IEnumerable<MemoriaRam> Get();
   Task Save(MemoriaRam memoriaRam);
   Task Update(String id, MemoriaRam memoriaRam);
   Task Delete(String id);
}