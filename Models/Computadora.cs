using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoInventarioASP.Models;

public class Computadora
{

    [Key]
    public int Id { get; set; }


    [Required(ErrorMessage = "El inventario es requerido")]
    [Display(Name = "Inventario")]
    public string NumInv { get; set; }
    [Required(ErrorMessage = "El departamento es requerido")]
    [Display(Name = "Departamento")]
    [StringLength(50, ErrorMessage = "El departamento es debe tener 50 caracteres como maximo")]
    public string NombreDepartamento { get; set; }
    [Required(ErrorMessage = "El Sello es requerido")]
    [Display(Name = "Sello")]
    public string Sello { get; set; }
    [Required(ErrorMessage = "El area es requerida")]
    [Display(Name = "Area")]
    [StringLength(50)]
    public string NombreArea { get; set; }


    [Required(ErrorMessage = "El nombre es requerido")]
    [Display(Name = "Nombre")]
    [MaxLength(14, ErrorMessage = "El nombre debe tener como maximo 14 caracteres")]
    [MinLength(13, ErrorMessage = "El nombre debe tener como minimo 13 caracteres")]
    public string Nombre { get; set; }
    [Required(ErrorMessage = "El Sistema operativo es requerido")]
    [Display(Name = "SO")]
    public string SO { get; set; }
    [Required(ErrorMessage = "El inventario de la ups es requerido")]
    [Display(Name = "Inventario UPS")]
    public string UpsInv { get; set; }
    [Required(ErrorMessage = "El estado es requerido")]
    [Display(Name = "Estado")]
    public Estado estado { get; set; }
    [Required(ErrorMessage = "La mac es requerida")]
    [Display(Name = "Mac")]
    public string Mac { get; set; }
    [Required(ErrorMessage = "El ip es requerido")]
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

    [Required(ErrorMessage = "El inventario de la impresora es requerido")]
    [Display(Name = "Inventario Impresora")]
    public string ImprNumInv { get; set; }

    [Required(ErrorMessage = "El inventario del teclado es requerido")]
    [Display(Name = "Teclado Inventario")]
    public string TeclNumInv { get; set; }

    [Required(ErrorMessage = "El nombre del usuario es requerido")]
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
