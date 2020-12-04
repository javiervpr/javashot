using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.Models
{
    [Table("PreguntaRespuesta")]
    public class PreguntaRespuesta
    {
        public Guid PreguntaRespuestaID { get; set; }
        public Pregunta Pregunta { get; set; }
        public Respuesta Respuesta { get; set; }

        public PreguntaRespuesta(Pregunta pregunta, Respuesta respuesta)
        {
            PreguntaRespuestaID = Guid.NewGuid();
            Pregunta = pregunta;
            Respuesta = respuesta;
        }

        protected PreguntaRespuesta() { }
    }
}
