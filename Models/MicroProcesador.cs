using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace ProyectoInventarioASP.Models
{
    public class MicroProcesador
    {
        public String NumSerieId { get; set; }
        public String Marca { get; set; }
        public String Tecnologia { get; set; }
        public Estado estado { get; set; }

        [JsonIgnore]
        public virtual ICollection<MotherBoard> MotherBoard { get; set; }
    }
}