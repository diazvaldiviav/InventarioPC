using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ProyectoInventarioASP.Models
{
    public class Impresora
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Serial")]
        public string NumSerie { get; set; }

        [Required]
        [Display(Name = "Inventario")]
        public string NumInv { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public Estado estado { get; set; }

        [JsonIgnore]
        public virtual ICollection<Computadora> Computadora { get; set; }
    }
}