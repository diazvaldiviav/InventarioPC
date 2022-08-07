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
        public string salida { get; set; }

        [Display(Name = "Fecha Entrega")]

        public DateTime FechaSalida { get; set; }
        public int EntradaId { get; set; }

        public string observaciones { get; set; }

        public Entrada entradas { get; set; }




    }
}