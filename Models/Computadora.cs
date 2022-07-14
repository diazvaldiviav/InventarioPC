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
    public string MemoriaRamId { get; set; }
    public string Mac { get; set; }

    public string NumIp { get; set; }

    public string ImpresoraId { get; set; }
    public string NombreUsuarioId { get; set; }
    public string DiscoDuroId { get; set; }
    public string MicroProcesadorId { get; set; }
    public string MotherBoardId { get; set; }
    public string MonitorId { get; set; }
    public string TecladoId { get; set; }

    public virtual DiscoDuro DiscoDuro { get; set; }
    public virtual MicroProcesador MicroProcesador { get; set; }
    public virtual Display Display { get; set; }
    public virtual Teclado Teclado { get; set; }
    public virtual MotherBoard MotherBoard { get; set; }
    public virtual Impresora Impresora { get; set; }
    public virtual Usuario Usuario { get; set; } 


    [JsonIgnore]
    public virtual ICollection<MemoriaRam> Memorias { get; set; }


    }

    public enum Estado
{
    activo, inactivo
}
