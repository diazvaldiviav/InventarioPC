using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoInventarioASP.Models;

public class Computadora
{

    [Key]
    public int Id { get; set; }


    [Required]
    [Display(Name = "Inventario")]
    public string NumInv { get; set; }
    [Required]
    [Display(Name = "Departamento")]
    public string NombreDepartamento { get; set; }
    [Required]
    [Display(Name = "Sello")]
    public string Sello { get; set; }
    [Required]
    [Display(Name = "Area")]
    public string NombreArea { get; set; }
    [Required]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; }
    [Required]
    [Display(Name = "SO")]
    public string SO { get; set; }
    [Required]
    [Display(Name = "Inventario UPS")]
    public string UpsInv { get; set; }
    [Required]
    [Display(Name = "Estado")]
    public Estado estado { get; set; }
    [Required]
    [Display(Name = "Mac")]
    public string Mac { get; set; }
    [Required]
    [Display(Name = "IP")]
    public string NumIp { get; set; }

    [ForeignKey("ImpresoraId")]
    [Display(Name = "Impresora No")]
    public int ImpresoraId { get; set; }

    [ForeignKey("UsuarioId")]
    [Display(Name = "Usuario No")]
    public int UsuarioId { get; set; }

    [ForeignKey("MotherBoardId")]
    [Display(Name = "Board Serie")]
    public string MotherBoardId { get; set; }

    [ForeignKey("TecladoId")]
    [Display(Name = "Teclado No")]
    public int TecladoId { get; set; }

    [ForeignKey("UpsId")]
    [Display(Name = "UPS No")]
    public int UpsId { get; set; }

    [Required]
    [Display(Name = "Inventario Impresora")]
    public string ImprNumInv { get; set; }

    [Required]
    [Display(Name = "Teclado Inventario")]
    public string TeclNumInv { get; set; }

    [Required]
    [Display(Name = "Nombre del Usuario")]
    public string UserName { get; set; }

    [Required]
    [Display(Name = "Marca Board")]
    public string MotherBoardMarca { get; set; }

    [Required]
    [Display(Name = "Tecnologia de micro")]
    public string MicroTecn { get; set; }



    public virtual Teclado Teclado { get; set; }

    public virtual MotherBoard MotherBoard { get; set; }

    public virtual Impresora Impresora { get; set; }

    public virtual Usuario Usuario { get; set; }

    public virtual Ups Ups { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual List<Display> Display { get; set; }
    [JsonIgnore]
    [NotMapped]
    public virtual List<DiscoDuro> Discos { get; set; }
    [JsonIgnore]
    [NotMapped]
    public virtual List<MemoriaRam> Memorias { get; set; }

    [NotMapped]
    public virtual List<Scanner> Scanners { get; set; }

}

public enum Estado
{
    activo, inactivo
}
