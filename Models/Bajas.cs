using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models;

public class Bajas
{
    [Key]
    public int id { get; set; }
    [Required(ErrorMessage = "Este Campo es requerido")]
    [Display(Name = "Inventario")]
    public string NumInv { get; set; }

    [Required(ErrorMessage = "Este Campo es requerido")]
    [Display(Name = "Serial")]
    public String NumSerie { get; set; }
    [Display(Name = "Marca")]
    [Required(ErrorMessage = "Este Campo es requerido")]
    public String Marca { get; set; }
    [Required(ErrorMessage = "Este Campo es requerido")]
    [Display(Name = "Fecha de baja")]
    public DateTime fechaBaja { get; set; }

    [Required(ErrorMessage = "Este Campo es requerido")]
    [Display(Name = "Equipo")]
    public string Equipo { get; set; }

    [Required(ErrorMessage = "Este Campo es requerido")]
    [Display(Name = "MotherBoard Serial")]
    public string SerieBoard { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual List<DiscoDuro> Discos { get; set; }
    [JsonIgnore]
    [NotMapped]
    public virtual List<MemoriaRam> Memorias { get; set; }

    [NotMapped]
    public virtual MotherBoard MotherBoard { get; set; }

    [NotMapped]
    public virtual MicroProcesador Micro { get; set; }


}