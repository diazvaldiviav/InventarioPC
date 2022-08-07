using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models
{
    public class Entrada
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Equipo")]
        public string Equipo { get; set; }
        [Required]
        [Display(Name = "Inventario")]
        public String NumInvEquipo { get; set; }

        [Required]
        [Display(Name = "Quien Entrega")]
        public string Entrega { get; set; }
        [Required]
        public string Lugar { get; set; }
        [Display(Name = "Fecha Entrega")]
        [Required]
        public DateTime FechaEntrega { get; set; }

        public string observaciones { get; set; }

        public Salida salidas { get; set; }



    }
}