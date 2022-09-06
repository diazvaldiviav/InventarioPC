using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models
{
    public class Entrada
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="Este Campo es requerido")]
        [Display(Name = "Equipo")]
        public string Equipo { get; set; }
        [Required (ErrorMessage ="Este Campo es requerido")]
        [Display(Name = "Inventario")]
        public String NumInvEquipo { get; set; }

        [Required (ErrorMessage ="Este Campo es requerido")]
        [Display(Name = "Quien Entrega")]
        public string Entrega { get; set; }
        [Required (ErrorMessage ="Este Campo es requerido")]
        public string Lugar { get; set; }
        [Display(Name = "Fecha Entrega")]
        [Required (ErrorMessage ="Este Campo es requerido")]
        public DateTime FechaEntrega { get; set; }

        [MaxLength(150, ErrorMessage = "Las observaciones deben tener como maximo 150caracteres")]
        [Required (ErrorMessage ="Este Campo es requerido")]
        public string observaciones { get; set; }

        public Salida salidas { get; set; }
        
        [NotMapped]
        public List<Equipos> equipos{get; set;}



    }
}