using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.Models
{
    [Table("Pregunta")]
    public class Pregunta
    {
        public Guid PreguntaID { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string ExplicacionRespuesta { get; set; }

        public Pregunta(string titulo, string explicacionRespuesta)
        {
            PreguntaID = Guid.NewGuid();
            Titulo = titulo;
            FechaRegistro = DateTime.Now;
            ExplicacionRespuesta = explicacionRespuesta;
        }
    }
}
