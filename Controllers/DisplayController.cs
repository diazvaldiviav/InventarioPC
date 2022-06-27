using Microsoft.AspNetCore.Mvc;
using ProyectoInventario.Modelos;
using ProyectoInventario.Services;
using System.Collections.Generic;
namespace ProyectoInventario.Controllers;

[Route("api/[controller]")]
public class DisplayController : ControllerBase
{
   IDisplayService displayService;

   public DisplayController(IDisplayService displayService)
   {
     this.displayService = displayService;
   }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(displayService.Get());
    }


    [HttpPost]
    public IActionResult Post([FromBody] Display display)
    {
        displayService.Save(display);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(String id,[FromBody] Display display)
    {
        displayService.Update(id, display);
        return Ok();
    }

     [HttpPut("{id}")]
    public IActionResult Delete(String id)
    {
        displayService.Delete(id);
        return Ok();
    }

}
