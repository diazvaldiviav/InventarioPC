using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models;

public class MotherBoard
{

    public String NumSerieId { get; set; }

    public String Marca { get; set; }

    public Estado estado { get; set; }
    [JsonIgnore]
    public virtual ICollection<Computadora> Computadoras { get; set; }

}