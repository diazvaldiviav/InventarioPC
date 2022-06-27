using Microsoft.AspNetCore.Mvc;
using ProyectoInventario.Modelos;
using ProyectoInventario.Services;
using System.Collections.Generic;
namespace ProyectoInventario.Controllers;


[Route("api/[controller]")]
public class DiscoDuroController: ControllerBase
{
  IDiscoDuroService discoDuroService;

  public DiscoDuroController(IDiscoDuroService discoDuroService)
  {
    this.discoDuroService = discoDuroService;
  }


  [HttpGet]

  public IActionResult Get()
  {
    discoDuroService.Get();
    return Ok();
  }

  [HttpPost]
  public IActionResult Post([FromBody] DiscoDuro discoDuro)
  {
    discoDuroService.Save(discoDuro);
    return Ok();
  }

  [HttpPut("{id}")]
  public IActionResult Put(String id, [FromBody] DiscoDuro discoDuro)
  {
     discoDuroService.Update(id, discoDuro);
     return Ok();
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(String id)
  {
    discoDuroService.Delete(id);
    return Ok();
  }
}
