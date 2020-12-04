using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.Models
{
    [Table("Respuesta")]
    public class Respuesta
    {
        public Guid RespuestaID { get; set; }
        public string Contenido { get; set; }
        public bool Correcta { get; set; }

        public Respuesta(string contendio, bool correcta)
        {
            if (contendio.Count() > 500) throw new Exception("El contenido no puede tener más de 500 caracteres");
            RespuestaID = Guid.NewGuid();
            Contenido = contendio;
            Correcta = correcta;
        }
        public Respuesta() { }
        //protected Respuesta () { }
    }
}
