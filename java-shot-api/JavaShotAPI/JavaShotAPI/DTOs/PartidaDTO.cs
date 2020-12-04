using JavaShotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.DTOs
{
    public class PartidaDTO
    {
        public Guid PartidaID { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaRegistro { get; set; }
        public List<PreguntaDTO> Preguntas { get; set; }

        public int CantidadPreguntas {get;set;}

        public PartidaDTO(Guid partidaID, Usuario usuario, DateTime fechaRegistro, List<PreguntaDTO> preguntas, int cantidadPreguntas)
        {
            PartidaID = partidaID;
            Usuario = usuario;
            FechaRegistro = fechaRegistro;
            Preguntas = preguntas;
            CantidadPreguntas = cantidadPreguntas;
        }

        public PartidaDTO() { }
    }
}
