using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ProyectoInventarioASP.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Nombre Completo")]
        public string NombreCompleto { get; set; }
        [Required]
        [Display(Name="Usuario")]
        public string NombreUsuario { get; set; }
        [Required]
        [Display(Name="Departamento")]
        public string NombreDepartamento { get; set; }
        [Required]
        [Display(Name="Area")]
        public string NombreArea { get; set; }

        [JsonIgnore]
        public virtual ICollection<Computadora> Computadora { get; set; }

        [JsonIgnore]
        public virtual ICollection<Teclado> Teclado { get; set; }
        [JsonIgnore]
        public virtual ICollection<Display> Monitores { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ups> Ups { get; set; }
        [JsonIgnore]
        public virtual ICollection<Impresora> Impresora { get; set; }


    }
}