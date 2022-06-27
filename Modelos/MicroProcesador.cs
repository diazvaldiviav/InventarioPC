using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace ProyectoInventario.Modelos
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