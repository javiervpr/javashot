using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.Models
{
    [Table("HistorialPunto")]
    public class HistorialPunto
    {
        public Guid HistorialPuntoID { get; set; }
        public int Puntos { get; set; }
        public Partida Partida { get; set; }
        public DateTime FechaRegistro { get; set; }

        public HistorialPunto(int puntos, Partida partida)
        {
            Puntos = puntos;
            Partida = partida;
            FechaRegistro = DateTime.Now;
            HistorialPuntoID = Guid.NewGuid();
        }

        protected HistorialPunto() { }
    }
}
