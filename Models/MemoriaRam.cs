using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models
{
    public class MemoriaRam
    {
        [Key]
        [Display(Name="Serial")]
        public string KayNumSerieId { get; set; }

        [Required(ErrorMessage ="Este Campo es requerido")]
        public string Marca { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        public string Capacidad { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        public string Tecnologia { get; set; }
        [ForeignKey("MotherBoardId")]
        [Display(Name="Serial MotherBoard")]
        public string MotherBoardId { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        public Estado estado { get; set; }

        
        public virtual MotherBoard MotherBoard { get; set; }

        [NotMapped]
        public virtual Computadora computadora { get; set; }

        [NotMapped]
        public virtual Bajas baja { get; set; }




    }
}