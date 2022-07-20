using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models
{
    public class MemoriaRam
    {
        [Key]
        public string KayNumSerieId { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Capacidad { get; set; }
        [Required]
        public string Tecnologia { get; set; }
        [ForeignKey("MotherBoardId")]
        public string MotherBoardId { get; set; }
        [Required]
        public Estado estado { get; set; }

        public virtual MotherBoard MotherBoard { get; set; }



    }
}