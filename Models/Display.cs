using System.Text.Json;
using System.Text.Json.Serialization;


namespace ProyectoInventarioASP.Models
{

    public class Display
    {

        public String NumInvId { get; set; }

        public String NumSerie { get; set; }

        public String Marca { get; set; }

        public string ComputadoraId {get; set;}

        public Estado estado { get; set; }

        [JsonIgnore]
        public virtual Computadora Computadora { get; set; }

    }

}