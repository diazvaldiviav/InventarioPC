using Microsoft.AspNetCore.Mvc;
using ProyectoInventario.Modelos;
using ProyectoInventario.Services;
using System.Collections.Generic;
namespace ProyectoInventario.Controllers;

[Route("api/[controller]")]
public class TecladoController : ControllerBase
{
    ITecladoService tecladoService;

    public TecladoController(ITecladoService tecladoService)
    {
        this.tecladoService = tecladoService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(tecladoService.Get());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Teclado teclado)
    {
        tecladoService.Save(teclado);
        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult Put(String id, [FromBody] Teclado teclado)
    {
        tecladoService.Update(id, teclado);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(String id)
    {
        tecladoService.Delete(id);
        return Ok();
    }

}
