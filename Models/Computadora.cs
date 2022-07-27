using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoInventarioASP.Models;

public class Computadora
{

    [Key]
    public string Id { get; set; }

    [Required]
    public string NumInv { get; set; }
    [Required]
    public string NombreDepartamento { get; set; }
    [Required]
    public string NombreArea { get; set; }
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string SO { get; set; }
    [Required]
    public string UpsInv { get; set; }
    [Required]
    public Estado estado { get; set; }
    [Required]
    public string Mac { get; set; }
    [Required]
     public string NumIp { get; set; }
    [ForeignKey("ImpresoraId")]
    public string ImpresoraId { get; set; }
    [ForeignKey("NombreUsuarioId")]
    public string NombreUsuarioId { get; set; }
    [ForeignKey("MotherBoardId")]
    public string MotherBoardId { get; set; }
    [ForeignKey("TecladoId")]
    public string TecladoId { get; set; }
    [ForeignKey("UpsId")]
    public string UpsId { get; set; }
    [Required]
    public string ImprNumInv {get; set;}
    [Required]
    public string TeclNumInv {get; set;}
    [Required]
    public string UserName {get; set;}
    [Required]
    public string MotherBoardMarca {get; set;}
    [Required]
    public string DiscoDuroCap {get; set;}
    [Required]
    public string DiscoDuroTipoCon {get; set;}
    [Required]
    public string MemoriaRamCap {get; set;}
    [Required]
    public string MemoriaRamTec {get; set;}
    [Required]
    public string MicroTecn {get; set;}

   
    

    

    public virtual Teclado Teclado { get; set; }
    public virtual MotherBoard MotherBoard { get; set; }
    public virtual Impresora Impresora { get; set; }
    public virtual Usuario Usuario { get; set; }
    public virtual Ups Ups { get; set; }

    public virtual ICollection<Display> Display { get; set; }

}

public enum Estado
{
    activo, inactivo
}
