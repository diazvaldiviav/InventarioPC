using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models
{
    public class Ups
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