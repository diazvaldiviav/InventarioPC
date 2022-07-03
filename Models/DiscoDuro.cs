using System.Text.Json.Serialization;
namespace ProyectoInventarioASP.Models;

    public class DiscoDuro
    {
        public String NumSerieId { get; set; }
        public String Marca { get; set; }
        public String TipoConexion { get; set; }
        public String Capacidad { get; set; }
        public Estado estado { get; set; }


        [JsonIgnore]
        public virtual ICollection<Computadora> Computadora { get; set; }
    }




