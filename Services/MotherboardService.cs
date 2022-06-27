using ProyectoInventario.Modelos;
using System.Collections.Generic;
namespace ProyectoInventario.Services;

public class MotherboardService : IMotherBoardService
{
    ComputadoraContext context;

    public MotherboardService(ComputadoraContext context)
    {
        this.context = context; 
    }

    public IEnumerable<MotherBoard> Get()
    {
        return context.MotherBoards;
    }

    public async Task Save(MotherBoard motherBoard)
    {
        context.Add(motherBoard);
        await context.SaveChangesAsync();
    }

    public async Task Update(String id, MotherBoard motherBoard)
    {
        var motherBoardActual = context.MotherBoards.Find(id);

        if (motherBoardActual != null)
        {
            motherBoardActual.Marca = motherBoard.Marca;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(String id)
    {
        var motherBoardActual = context.MotherBoards.Find(id);
        if (motherBoardActual != null)
        {
            context.Remove(motherBoardActual);

            await context.SaveChangesAsync();
        }
    }

}

public interface IMotherBoardService

{
    IEnumerable<MotherBoard> Get();
    Task Save(MotherBoard motherBoard);
    Task Update(String id, MotherBoard motherBoard);
    Task Delete(String id);
}

