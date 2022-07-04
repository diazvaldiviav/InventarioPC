using System.Text.Json.Serialization;
namespace ProyectoInventario.Modelos
{
    public class Impresora
    {
        public string NumInvId { get; set; }
        public string NumSerie { get; set; }
        public string Marca { get; set; }

        [JsonIgnore]
        public virtual ICollection <Computadora> Computadora { get; set; }
    }
}