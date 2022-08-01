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
     [Display(Name = "Impresora No")]
    [ForeignKey("ImpresoraId")]
    public int ImpresoraId { get; set; }
    [Display(Name = "Usuario No")]
    [ForeignKey("UsuarioId")]
    public int UsuarioId { get; set; }
    [Display(Name = "Board Serie")]
    [ForeignKey("MotherBoardId")]
    public string MotherBoardId { get; set; }
    [Display(Name = "Teclado No")]
    [ForeignKey("TecladoId")]
    public int TecladoId { get; set; }
    [Display(Name = "UPS No")]
    [ForeignKey("UpsId")]
    public int UpsId { get; set; }
    [Display(Name = "Inventario Impresora")]
    [Required]
    public string ImprNumInv {get; set;}
    [Display(Name = "Teclado Inventario")]
    [Required]
    public string TeclNumInv {get; set;}
    [Display(Name = "Nombre del Usuario")]
    [Required]
    public string UserName {get; set;}
    [Display(Name = "Marca Board")]
    [Required]
    public string MotherBoardMarca {get; set;}
    [Display(Name = "Capacidad de disco")]
    [Required]
    public string DiscoDuroCap {get; set;}
    [Display(Name = "Cable del disco")]
    [Required]
    public string DiscoDuroTipoCon {get; set;}
    [Display(Name = "Capacidad de memoria")]
    [Required]
    public string MemoriaRamCap {get; set;}
    [Display(Name = "Tecnologia de memoria")]
    [Required]
    public string MemoriaRamTec {get; set;}
    [Display(Name = "Tecnologia de micro")]
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
