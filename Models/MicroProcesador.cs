using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace ProyectoInventarioASP.Models
{
    public class MicroProcesador
    {
        [Key]
        public String NumSerieId { get; set; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        public string NumSerieMicro { get; set; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        public String Marca { get; set; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        public String Tecnologia { get; set; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        public Estado estado { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        public String invPc { get; set; }


        public virtual ICollection<MotherBoard> MotherBoard { get; set; }

        [NotMapped]
        public virtual Computadora computadora { get; set; }

        [NotMapped]
        public virtual Bajas baja { get; set; }

        [NotMapped]
        public virtual MotherBoard board { get; set; }
    }
}