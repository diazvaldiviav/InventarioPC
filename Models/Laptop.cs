using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoInventarioASP.Models;

public class Laptop
{

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El Inventario es requerido")]
    [Display(Name = "Inventario")]
    public string NumInv { get; set; }
    [Required(ErrorMessage = "El Departamento es requerido")]
    [Display(Name = "Departamento")]
    public string NombreDepartamento { get; set; }

    [Required(ErrorMessage = "El Area es requerido")]
    [Display(Name = "Area")]
    public string NombreArea { get; set; }
    [Required(ErrorMessage = "El nombre es requerido")]
    [Display(Name = "Nombre")]
    [MaxLength(14, ErrorMessage = "El nombre debe tener como maximo 14 caracteres")]
    [MinLength(13, ErrorMessage = "El nombre debe tener como minimo 13 caracteres")]
    public string Nombre { get; set; }
    [Required(ErrorMessage = "El sist operativo es requerido")]
    [Display(Name = "SO")]
    public string SO { get; set; }

    [Required(ErrorMessage = "El estado operativo es requerido")]
    [Display(Name = "Estado")]
    public Estado estado { get; set; }

    [Required(ErrorMessage = "La mac es requerido")]
    [Display(Name = "Mac")]
    public string Mac { get; set; }
    [Required(ErrorMessage = "El Ip es requerido")]
    [Display(Name = "IP")]
    public string NumIp { get; set; }

    [ForeignKey("UsuarioId")]
    [Display(Name = "Usuario No")]
    public int UsuarioId { get; set; }

    [Required(ErrorMessage = "El Nombre de usuario es requerido")]
    [Display(Name = "Nombre del Usuario")]
    public string UserName { get; set; }

  
    public virtual Usuario Usuario { get; set; }

}



