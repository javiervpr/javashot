using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.Models
{
    [Table("PartidaPregunta")]
    public class PartidaPregunta
    {
        public Guid PartidaPreguntaID { get; set; }
        public bool? ContestadaCorrectamente { get; set; }
        public bool Contestada { get; set; }
        public DateTime? FechaRespuesta { get; set; }
        public Guid PartidaID { get; set; }
        public Guid PreguntaID { get; set; }
        public Partida Partida { get; set; }
        public Pregunta Pregunta { get; set; }

        public PartidaPregunta(Partida partida, Pregunta pregunta)
        {
            Partida = partida;
            Pregunta = pregunta;
            PartidaPreguntaID = Guid.NewGuid();
            Contestada = false;
            FechaRespuesta = DateTime.Now;
        }
        protected PartidaPregunta() { }
    }
}
