using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.DTOs
{
    public class RespuestaAPI<T>
    {
        public int Codigo { get; set; }
        public DateTime FechaHora { get; set; }
        public string Mensaje { get; set; }
        public T Data { get; set; }

        public RespuestaAPI()
        {
        }

        public RespuestaAPI(int codigo, string mensaje, T data)
        {
            FechaHora = DateTime.Now;
            Codigo = codigo;
            Data = data;
            Mensaje = mensaje;
        }
    }
}
