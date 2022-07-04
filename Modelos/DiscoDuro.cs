using System.Text.Json.Serialization;
namespace ProyectoInventario.Modelos
{
    public class DiscoDuro
    {
        public String NumSerieId { get; set; }
        public String Marca { get; set; }
        public String TipoConexion { get; set; }
        public String Capacidad {get; set;}

        [JsonIgnore]
        public virtual ICollection<Computadora> Computadora {get;set;}
    }
}