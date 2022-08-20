using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace ProyectoInventarioASP.Models
{
    public class MicroProcesador
    {
        [Key]
        public String NumSerieId { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        public String Marca { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        public String Tecnologia { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        public Estado estado { get; set; }

        [JsonIgnore]
        public virtual ICollection<MotherBoard> MotherBoard { get; set; }
    }
}