using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models
{
    public class MemoriaRam
    {
        public string NumSerieId { get; set; }
        public string Marca { get; set; }
        public string Capacidad { get; set; }
        public string Tecnologia { get; set; }

        public Estado estado { get; set; }
        [JsonIgnore]
        public virtual ICollection<Computadora> Computadora { get; set; }

    }
}