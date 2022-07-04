using Microsoft.AspNetCore.Mvc;
using ProyectoInventario.Modelos;
using ProyectoInventario.Services;
using System.Collections.Generic;
namespace ProyectoInventario.Controllers;

[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    IUsuarioService usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        this.usuarioService = usuarioService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(usuarioService.Get());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Usuario usuario)
    {
        usuarioService.Save(usuario);
        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult Put(String id, [FromBody] Usuario usuario)
    {
        usuarioService.Update(id, usuario);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(String id)
    {
        usuarioService.Delete(id);
        return Ok();
    }

}
