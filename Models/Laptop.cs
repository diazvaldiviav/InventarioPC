using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoInventarioASP.Models;

public class Laptop
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
    [Display(Name = "Estado")]
    public Estado estado { get; set; }
    [Required]
    [Display(Name = "Mac")]
    public string Mac { get; set; }
    [Required]
    [Display(Name = "IP")]
    public string NumIp { get; set; }

    [ForeignKey("UsuarioId")]
    [Display(Name = "Usuario No")]
    public int UsuarioId { get; set; }

    [Required]
    [Display(Name = "Nombre del Usuario")]
    public string UserName { get; set; }

  
    public virtual Usuario Usuario { get; set; }

}



