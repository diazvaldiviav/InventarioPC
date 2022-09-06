using System.ComponentModel.DataAnnotations;

namespace ProyectoInventarioASP.Models
{
    public class Equipos
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El tipo de equipo es requerido")]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
    }
}