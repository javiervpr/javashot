using JavaShotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.DTOs
{
    public class PreguntaDTO
    {
        public Guid PreguntaID { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public List<Respuesta> Respuestas { get; set; }
        public string PartidaPreguntaID { get; set; }
        public string ExplicacionRespuesta { get; set; }
        public bool? Contestada { get; set; }
        public bool? ContestadaCorrectamente { get; set; }

        public PreguntaDTO(string preguntaID, string titulo, DateTime fechaRegistro, List<Respuesta> respuestas, string partidaPreguntaID, string explicacionRespuesta)
        {
            PreguntaID = Guid.Parse(preguntaID);
            Titulo = titulo;
            FechaRegistro = fechaRegistro;
            Respuestas = respuestas;
            PartidaPreguntaID = partidaPreguntaID;
            ExplicacionRespuesta = explicacionRespuesta;
        }

        public PreguntaDTO(string preguntaID, string titulo, DateTime fechaRegistro, List<Respuesta> respuestas, string partidaPreguntaID, string explicacionRespuesta, bool? contestada, bool? contestadaCorrectamente)
        {
            PreguntaID = Guid.Parse(preguntaID);
            Titulo = titulo;
            FechaRegistro = fechaRegistro;
            Respuestas = respuestas;
            Contestada = contestada;
            ContestadaCorrectamente = contestadaCorrectamente;
            PartidaPreguntaID = partidaPreguntaID;
            ExplicacionRespuesta = explicacionRespuesta;
        }

        public PreguntaDTO() {}


    }
}
