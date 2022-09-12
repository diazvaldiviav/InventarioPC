using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [Required]
        public string permisos { get; set; }
    }

}