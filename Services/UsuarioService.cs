using ProyectoInventario.Modelos;
using System.Collections.Generic;
namespace ProyectoInventario.Services;

public class UsuarioService : IUsuarioService
{
    ComputadoraContext context;

    public UsuarioService(ComputadoraContext context)
    {
        this.context = context;
    }

    public IEnumerable<Usuario> Get()
    {
        return context.Usuarios;
    }

    public async Task Save(Usuario usuario)
    {
        context.Add(usuario);

        await context.SaveChangesAsync();
    }

    public async Task Update(String id, Usuario usuario)
    {
        var usuarioActual = context.Usuarios.Find(id);

        if (usuarioActual != null)
        {
            usuarioActual.NombreCompleto = usuario.NombreCompleto;
            usuario.NombreDepartamento = usuario.NombreDepartamento;
            usuario.NombreArea = usuario.NombreArea;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(String id)
    {
        var usuarioActual = context.Usuarios.Find(id);

        if (usuarioActual != null)
        {
            context.Remove(usuarioActual);

            await context.SaveChangesAsync();
        }

    }


}

public interface IUsuarioService
{
    IEnumerable<Usuario> Get();
    Task Save(Usuario usuario);
    Task Update(String id, Usuario usuario);
    Task Delete(String id);
}
