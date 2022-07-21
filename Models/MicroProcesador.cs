using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace ProyectoInventarioASP.Models
{
    public class MicroProcesador
    {
        [Key]
        public String NumSerieId { get; set; }
        [Required]
        public String Marca { get; set; }
        [Required]
        public String Tecnologia { get; set; }
        [Required]
        public Estado estado { get; set; }

        [JsonIgnore]
        public virtual ICollection<MotherBoard> MotherBoard { get; set; }
    }
}