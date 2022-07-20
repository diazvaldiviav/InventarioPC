using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ProyectoInventarioASP.Models
{

    public class Display
    {
        [Key]
        public String Id { get; set; }

        [Required]
        public string NumInv { get; set; }

        [Required]
        public String NumSerie { get; set; }

        [Required]
        public String Marca { get; set; }

        [ForeignKey("ComputadoraId")]
        public string ComputadoraId { get; set; }

        public string NumInvPc { get; set; }

        [Required]
        public Estado estado { get; set; }

        [JsonIgnore]
        public virtual Computadora Computadora { get; set; }

    }

}