using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoInventarioASP.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Nombre { get; set; }

        public string username {get; set;}

        public string password{get; set;}

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string permisos {get; set;}
    }

}