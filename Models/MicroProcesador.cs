using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace ProyectoInventarioASP.Models
{
    public class MicroProcesador
    {
        public String NumSerieId { get; set; }
        public String Marca { get; set; }
        public String Tecnologia { get; set; }

        [JsonIgnore]
        public virtual ICollection<Computadora> Computadora { get; set; }
    }
}