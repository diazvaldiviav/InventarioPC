using ProyectoInventario.Modelos;
using System.Collections.Generic;
namespace ProyectoInventario.Services;

public class ImpresoraService : IImpresoraService
{
    ComputadoraContext context;

    public ImpresoraService(ComputadoraContext context)
    {
        this.context = context;
    }

    public IEnumerable<Impresora> Get()
    {
        return context.Impresoras;
    }

    public async Task Save(Impresora impresora)
    {
        context.Add(impresora);

        await context.SaveChangesAsync();
    }

    public async Task Update(String id, Impresora impresora)
    {
        var impresoraActual = context.Impresoras.Find(id);

        if (impresoraActual != null)
        {
            impresoraActual.Marca = impresora.Marca;
            impresora.NumSerie = impresora.NumSerie;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(String id)
    {
        var impresoraActual = context.Impresoras.Find(id);

        if (impresoraActual != null)
        {
            context.Remove(impresoraActual);

            await context.SaveChangesAsync();
        }

    }


}

public interface IImpresoraService
{
    IEnumerable<Impresora> Get();
    Task Save(Impresora impresora);
    Task Update(String id, Impresora impresora);
    Task Delete(String id);
}
