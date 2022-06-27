using System.Text.Json.Serialization;
namespace ProyectoInventario.Modelos
{
    public class Computadora
    {
        public String NumInvId { get; set; }
        public String NombreDepartamento { get; set; }
        public String NombreArea { get; set; }
        public String MemoriaRamId { get; set; }
        public String DiscoDuroId { get; set; }
        public String MicroProcesadorId { get; set; }
        public String MotherBoardId { get; set; }
        public String MonitorId { get; set; }
        public String TecladoId { get; set; }

        public virtual DiscoDuro DiscoDuro { get; set; }
        public virtual MemoriaRam MemoriaRam { get; set; }
        public virtual MicroProcesador MicroProcesador { get; set; }
        public virtual Display Display { get; set; }
        public virtual Teclado Teclado { get; set; }
        public virtual MotherBoard MotherBoard { get; set; }


    }
}