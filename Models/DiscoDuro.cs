using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace ProyectoInventarioASP.Models;

    public class DiscoDuro
    {
        [Key]
        public string NumSerieId { get; set; }
        [Required]
        public string Marca { get; set; }
         [Required]
        public string TipoConexion { get; set; }
         [Required]
        public string Capacidad { get; set; }
        [ForeignKey("MotherBoardId")]
        public string MotherBoardId {get; set;}

        public Estado estado { get; set; }
         
        public virtual MotherBoard motherBoard{ get; set; }
    }




