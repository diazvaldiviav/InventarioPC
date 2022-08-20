using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ProyectoInventarioASP.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        [Display(Name = "Departamento")]
        public string NombreDepartamento { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        [Display(Name = "Area")]
        public string NombreArea { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        public string Cargo { get; set; }

        [JsonIgnore]
        public virtual List<Computadora> Computadora { get; set; }

        [JsonIgnore]
        public virtual List<Teclado> Teclado { get; set; }
        [JsonIgnore]
        public virtual List<Display> Monitores { get; set; }
        [JsonIgnore]
        public virtual List<Ups> Ups { get; set; }
        [JsonIgnore]
        public virtual List<Impresora> Impresora { get; set; }
        [JsonIgnore]
        public virtual List<Celular> Celular { get; set; }
        [JsonIgnore]
        public virtual List<Telefono> Telefono { get; set; }

        [JsonIgnore]
        public virtual List<Laptop> Laptop { get; set; }

        [JsonIgnore]
        public virtual List<Scanner> Scanner { get; set; }


    }
}