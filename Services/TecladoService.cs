using ProyectoInventario.Modelos;
using System.Collections.Generic;
namespace ProyectoInventario.Services;

public class TecladoService : ITecladoService
{
    ComputadoraContext context;

    public TecladoService(ComputadoraContext context)
    {
        this.context = context;
    }

    public IEnumerable<Teclado> Get()
    {
        return context.Teclados;
    }

    public async Task Save(Teclado teclado)
    {
        context.Add(teclado);

        await context.SaveChangesAsync();
    }

    public async Task Update(String id, Teclado teclado)
    {
        var tecladoActual = context.Teclados.Find(id);

        if (tecladoActual != null)
        {
            tecladoActual.Marca = teclado.Marca;
            teclado.NumSerie = teclado.NumSerie;
            teclado.TipoConexion = teclado.TipoConexion;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(String id)
    {
        var tecladoActual = context.Teclados.Find(id);

        if (tecladoActual != null)
        {
            context.Remove(tecladoActual);

            await context.SaveChangesAsync();
        }

    }


}

public interface ITecladoService
{
    IEnumerable<Teclado> Get();
    Task Save(Teclado teclado);
    Task Update(String id, Teclado teclado);
    Task Delete(String id);
}
