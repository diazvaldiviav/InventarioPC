using Microsoft.AspNetCore.Mvc;
using ProyectoInventario.Modelos;
using ProyectoInventario.Services;
using System.Collections.Generic;
namespace ProyectoInventario.Controllers;

[Route("api/[controller]")]
public class MemoriaRamController: ControllerBase
{
   IMemoriaRamService memoriaRamService;

   public MemoriaRamController(IMemoriaRamService memoriaRamService)
   {
    this.memoriaRamService = memoriaRamService;
   }

   [HttpGet]

   public IActionResult Get()
   {
    return Ok(memoriaRamService.Get());
   }

   [HttpPost]

   public IActionResult Post([FromBody] MemoriaRam memoriaRam)
   {
    memoriaRamService.Save(memoriaRam);
    return Ok();
   }

   [HttpPut("{id}")]
    public IActionResult Put(String id, [FromBody] MemoriaRam memoriaRam)
    {
        memoriaRamService.Update(id, memoriaRam);
        return Ok();
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(String id)
    {
        memoriaRamService.Delete(id);
        return Ok();
    }





}
