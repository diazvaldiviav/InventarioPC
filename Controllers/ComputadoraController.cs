using Microsoft.AspNetCore.Mvc;
using ProyectoInventario.Modelos;
using ProyectoInventario.Services;
using System.Collections.Generic;
namespace ProyectoInventario.Controllers;


[Route("api/[controller]")]
public class ComputadoraController : ControllerBase
{
    IComputadoraService computadoraService;
    ComputadoraContext context;
    public ComputadoraController(IComputadoraService computadoraService, ComputadoraContext db)
    {
        this.computadoraService = computadoraService;
        context = db;
    }


    [HttpGet]

    public IActionResult Get()
    {
        return Ok(computadoraService.Get());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Computadora PC)
    {
        computadoraService.Save(PC);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(String id, [FromBody] Computadora PC)
    {
        computadoraService.Update(id, PC);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(String id)
    {
        computadoraService.Delete(id);
        return Ok();
    }

    [HttpGet]
    [Route("createdb")]
    public IActionResult CreateDatabase()
    {
        context.Database.EnsureCreated();

        return Ok();
    }
}
