using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models
{
    public class Teclado
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String NumSerie { get; set; }
        [Required]
        public String NumInv { get; set; }
        [Required]
        public String Marca { get; set; }
        [Required]
        public String TipoConexion { get; set; }
        [Required]
        public Estado estado {get; set;}

        [JsonIgnore]
        public virtual ICollection<Computadora> Computadora { get; set; }
    }
}