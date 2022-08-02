using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ProyectoInventarioASP.Models
{

    public class Display
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Inventario")]
        public string NumInv { get; set; }

        [Required]
        [Display(Name = "Serial")]
        public String NumSerie { get; set; }
        [Display(Name = "Marca")]
        [Required]
        public String Marca { get; set; }
        [Display(Name = "Computador No")]
        [ForeignKey("ComputadoraId")]
        public int ComputadoraId { get; set; }
        [Display(Name = "Inventario de Pc")]
        [Required]
        public string NumInvPc { get; set; }
        [Display(Name = "Estado")]
        [Required]
        public Estado estado { get; set; }
        [Display(Name = "Usuario No")]
        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }
        [Display(Name = "Nombre del Usuario")]
        [Required]
        public string UserName { get; set; }

        [JsonIgnore]
        public virtual Computadora Computadora { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }

    }

}