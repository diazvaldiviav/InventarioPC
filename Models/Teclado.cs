using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models
{
    public class Teclado
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        [Display(Name="Serial")]
        public String NumSerie { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        [Display(Name="Inventario")]
        public String NumInv { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        public String Marca { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        [Display(Name="Conexion")]
        public String TipoConexion { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        public Estado estado {get; set;}
        [Display(Name = "Usuario No")]
        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }
        [Display(Name = "Nombre del Usuario")]
        [Required(ErrorMessage ="Este Campo es requerido")]
        public string UserName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Computadora> Computadora { get; set; }

      
        public virtual Usuario Usuario { get; set; }
    }
}