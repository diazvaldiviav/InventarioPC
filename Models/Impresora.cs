using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Display(Name = "Usuario No")]
        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }
        [Display(Name = "Nombre del Usuario")]
        [Required]
        public string UserName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Computadora> Computadora { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}