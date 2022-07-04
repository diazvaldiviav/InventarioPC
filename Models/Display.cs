using System.Text.Json;
using System.Text.Json.Serialization;


namespace ProyectoInventarioASP.Models
{

    public class Display
    {

        public String NumInvId { get; set; }

        public String NumSerie { get; set; }

        public String Marca { get; set; }

        public Estado estado { get; set; }

        [JsonIgnore]
        public virtual ICollection<Computadora> Computadora { get; set; }

    }

}