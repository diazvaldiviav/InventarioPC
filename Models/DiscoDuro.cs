using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace ProyectoInventarioASP.Models;

public class DiscoDuro
{
    [Key]
    [Display(Name = "Serial")]
    public string NumSerieId { get; set; }
    [Required(ErrorMessage = "Este Campo es requerido")]
    public string Marca { get; set; }
    [Required(ErrorMessage = "Este Campo es requerido")]
    [Display(Name = "Tipo de Cable")]
    public string TipoConexion { get; set; }
    [Required(ErrorMessage = "Este Campo es requerido")]
    public string Capacidad { get; set; }
    [ForeignKey("MotherBoardId")]
    [Display(Name = "Serial MotherBoard")]
    public string MotherBoardId { get; set; }

    public Estado estado { get; set; }

    public virtual MotherBoard motherBoard { get; set; }

    [NotMapped]
    public virtual Computadora computadora { get; set; }

    [NotMapped]
    public virtual Bajas baja { get; set; }
}




