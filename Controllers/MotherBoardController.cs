using Microsoft.AspNetCore.Mvc;
using ProyectoInventario.Modelos;
using ProyectoInventario.Services;
using System.Collections.Generic;
namespace ProyectoInventario.Controllers;

[Route("api/[controller]")]
public class MotherBoardController : ControllerBase
{
   IMotherBoardService motherBoardService;

   public MotherBoardController(IMotherBoardService motherBoardService)
   {
     this.motherBoardService = motherBoardService;
   }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(motherBoardService.Get());
    }


    [HttpPost]
    public IActionResult Post([FromBody] MotherBoard board)
    {
        motherBoardService.Save(board);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(String id,[FromBody] MotherBoard board)
    {
        motherBoardService.Update(id, board);
        return Ok();
    }

     [HttpPut("{id}")]
    public IActionResult Delete(String id)
    {
        motherBoardService.Delete(id);
        return Ok();
    }

}
