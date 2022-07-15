using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models;

public class MotherBoard
{

    public string NumSerieId { get; set; }
    public string Marca { get; set; }
    public string MicroProcesadorId { get; set; }

    public Estado estado { get; set; }

    public virtual MicroProcesador Micro { get; set; }

    [JsonIgnore]
    public virtual ICollection<MemoriaRam> Memorias { get; set; }
    [JsonIgnore]
    public virtual ICollection<DiscoDuro> Discos { get; set; }
    [JsonIgnore]
    public virtual ICollection<Computadora> Computadora { get; set; }

}