using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace ProyectoInventarioASP.Models;

public class Computadora
{
    public string NumInvId { get; set; }
    public string NombreDepartamento { get; set; }
    public string NombreArea { get; set; }
    public string Nombre { get; set; }
    public Estado estado { get; set; }
    public string Mac { get; set; }
    public string NumIp { get; set; }
    public string ImpresoraId { get; set; }
    public string NombreUsuarioId { get; set; }
    public string MotherBoardId { get; set; }
    public string TecladoId { get; set; }
    public virtual Teclado Teclado { get; set; }
    public virtual MotherBoard MotherBoard { get; set; }
    public virtual Impresora Impresora { get; set; }
    public virtual Usuario Usuario { get; set; }

    public virtual ICollection<Display> Display { get; set; }

}

public enum Estado
{
    activo, inactivo
}
