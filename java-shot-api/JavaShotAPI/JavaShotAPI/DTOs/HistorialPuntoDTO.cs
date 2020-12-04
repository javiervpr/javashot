using JavaShotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.DTOs
{
    public class HistorialPuntoDTO
    {
        public Guid HistorialPuntoID { get; set; }
        public int Puntos { get; set; }
        public Partida Partida { get; set; }
        public DateTime FechaRegistro { get; set; }

        public HistorialPuntoDTO() {}

        public HistorialPuntoDTO(Guid historialPuntoID, int puntos, Partida partida, DateTime fechaRegistro)
        {
            HistorialPuntoID = historialPuntoID;
            Puntos = puntos;
            Partida = partida;
            FechaRegistro = fechaRegistro;
        }

        public HistorialPuntoDTO(HistorialPunto historialPunto) {
            HistorialPuntoID = historialPunto.HistorialPuntoID;
            Puntos = historialPunto.Puntos;
            Partida = historialPunto.Partida;
            FechaRegistro = historialPunto.FechaRegistro;
        }
    }
}
