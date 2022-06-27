using System.Text.Json.Serialization;

namespace ProyectoInventario.Modelos
{
    public class Teclado
    {
        public String NumInvId { get; set; }
        public String NumSerie { get; set; }
        public String Marca { get; set; }
        public String TipoConexion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Computadora> Computadora { get; set; }
    }
}