using Microsoft.AspNetCore.Mvc;
using ProyectoInventario.Modelos;
using ProyectoInventario.Services;
using System.Collections.Generic;
namespace ProyectoInventario.Controllers;

[Route("api/[controller]")]
public class ImpresoraController : ControllerBase
{
    IImpresoraService impresoraService;

    public ImpresoraController(IImpresoraService impresoraService)
    {
        this.impresoraService = impresoraService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(impresoraService.Get());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Impresora impresora)
    {
        impresoraService.Save(impresora);
        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult Put(String id, [FromBody] Impresora impresora)
    {
        impresoraService.Update(id, impresora);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(String id)
    {
        impresoraService.Delete(id);
        return Ok();
    }

}
