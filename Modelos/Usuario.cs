using System.Text.Json.Serialization;
namespace ProyectoInventario.Modelos
{
    public class Usuario
    {
        public string NombreUsuarioId { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreDepartamento { get; set; }
        public string NombreArea { get; set; }

        [JsonIgnore]
        public virtual ICollection<Computadora> Computadora { get; set; }


    }
}