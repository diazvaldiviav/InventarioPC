using Microsoft.AspNetCore.Mvc;
using ProyectoInventario.Modelos;
using ProyectoInventario.Services;
using System.Collections.Generic;
namespace ProyectoInventario.Controllers;

[Route("api/[controller]")]
public class MicroController : ControllerBase
{
    IMicroService microService;

    public MicroController(IMicroService microService)
    {
        this.microService = microService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(microService.Get());
    }

    [HttpPost]
    public IActionResult Post([FromBody] MicroProcesador micro)
    {
        microService.Save(micro);
        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult Put(String id, [FromBody] MicroProcesador micro)
    {
        microService.Update(id, micro);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(String id)
    {
        microService.Delete(id);
        return Ok();
    }    

}
