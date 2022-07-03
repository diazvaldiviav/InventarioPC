using System.Text.Json.Serialization;
namespace ProyectoInventarioASP.Models
{
    public class Impresora
    {
        public string NumInvId { get; set; }
        public string NumSerie { get; set; }
        public string Marca { get; set; }

        public Estado estado { get; set; }

        [JsonIgnore]
        public virtual ICollection <Computadora> Computadora { get; set; }
    }
}