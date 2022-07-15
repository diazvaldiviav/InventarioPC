using System.Text.Json.Serialization;
namespace ProyectoInventarioASP.Models;

    public class DiscoDuro
    {
        public string NumSerieId { get; set; }
        public string Marca { get; set; }
        public string TipoConexion { get; set; }
        public string Capacidad { get; set; }
        public string MotherBoardId {get; set;}
        public Estado estado { get; set; }
         
        public virtual MotherBoard motherBoard{ get; set; }
    }




