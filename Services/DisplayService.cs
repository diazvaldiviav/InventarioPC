using ProyectoInventario.Modelos;
using System.Collections.Generic;
namespace ProyectoInventario.Services;

public class DisplayService : IDisplayService
{
    ComputadoraContext context;

    public DisplayService(ComputadoraContext context)
    {
        this.context = context;
    }

    public IEnumerable<Display> Get()
    {
        return context.Displays;
    }

    public async Task Save(Display display)
    {
        context.Add(display);
        await context.SaveChangesAsync();
    }

    public async Task Update(String id, Display display)
    {
        var DisplayActual = context.Displays.Find(id);

        if (DisplayActual != null)
        {
            DisplayActual.NumSerie = display.NumSerie;
            display.Marca = display.Marca;

            await context.SaveChangesAsync();

        }
    }

    public async Task Delete(String id)
    {
        var DisplayActual = context.Displays.Find(id);

        if (DisplayActual != null)
        {
            context.Remove(DisplayActual);
            await context.SaveChangesAsync();
        }
    }

}

public interface IDisplayService
{
    IEnumerable<Display> Get();

    Task Save(Display display);

    Task Update(String id, Display display);
    Task Delete(String id);
}