using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProyectoInventario.Modelos;

public class MotherBoard
{
    
    public String NumSerieId {get;set;}

    public String Marca {get;set;}

    [JsonIgnore]
    public virtual ICollection<Computadora> Computadoras {get;set;}

}