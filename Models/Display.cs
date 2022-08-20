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

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Display(Name = "Inventario")]
        public string NumInv { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Display(Name = "Serial")]
        public String NumSerie { get; set; }
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Este Campo es requerido")]
        public String Marca { get; set; }
        [Display(Name = "Computador No")]
        [ForeignKey("ComputadoraId")]
        public int ComputadoraId { get; set; }
        [Display(Name = "Inventario de Pc")]
        [Required]
        public string NumInvPc { get; set; }
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Este Campo es requerido")]
        public Estado estado { get; set; }
        [Display(Name = "Usuario No")]
        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }
        [Display(Name = "Nombre del Usuario")]
        [Required(ErrorMessage = "Este Campo es requerido")]
        public string UserName { get; set; }

        
        public virtual Computadora Computadora { get; set; }
        public virtual Usuario Usuario { get; set; }

    }

}