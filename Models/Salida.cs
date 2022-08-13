using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models
{
    public class Salida
    {
        [Key]
        public string Id { get; set; }


        [Display(Name = "Quien Se lo lleva")]
        [Required(ErrorMessage = "Este Campo es requerido")]
        public string salida { get; set; }

        [Display(Name = "Fecha Entrega")]
        [Required(ErrorMessage = "Este Campo es requerido")]
        public DateTime FechaSalida { get; set; }
        public int EntradaId { get; set; }

        [MaxLength(150, ErrorMessage = "Las observaciones deben tener como maximo 150caracteres")]
        [Required(ErrorMessage = "Este Campo es requerido")]
        public string observaciones { get; set; }

        public Entrada entradas { get; set; }




    }
}