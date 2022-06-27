using ProyectoInventario.Modelos;
using System.Collections.Generic;
namespace ProyectoInventario.Services
{
    public class DiscoDuroService : IDiscoDuroService
    {
        ComputadoraContext context;

        public DiscoDuroService(ComputadoraContext context)
        {
            this.context = context;
        }


        public IEnumerable<DiscoDuro> Get()
        {
            return context.DiscosDuro;
        }

        public async Task Save(DiscoDuro discoDuro)
        {
            context.Add(discoDuro);

            await context.SaveChangesAsync();
        }

        public async Task Update(String id, DiscoDuro discoDuro)
        {
            var discoDuroActual = context.DiscosDuro.Find(id);

            if (discoDuroActual != null)
            {
                discoDuroActual.Marca = discoDuro.Marca;
                discoDuro.Capacidad = discoDuro.Capacidad;

                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(String id)
        {
            var discoDuroActual = context.DiscosDuro.Find(id);

            if (discoDuroActual != null)
            {
                context.Remove(discoDuroActual);

                await context.SaveChangesAsync();
            }
        }
    }
}

public interface IDiscoDuroService
{
    IEnumerable<DiscoDuro> Get();
    Task Save(DiscoDuro discoDuro);

    Task Update(String id, DiscoDuro discoDuro);
    Task Delete(String id);
}