using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ProyectoInventarioASP.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NombreCompleto { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string NombreDepartamento { get; set; }
        [Required]
        public string NombreArea { get; set; }

        [JsonIgnore]
        public virtual ICollection<Computadora> Computadora { get; set; }


    }
}