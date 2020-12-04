using JavaShotAPI.DTOs;
using JavaShotAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JavaShotAPIIntegrationTest.TestClasses
{
    public class HistorialPuntoDTOTest
    {
        [Fact]
        public async Task CrearHistorialPuntoDTO()
        {
            HistorialPuntoDTO historialPuntoDTO = new HistorialPuntoDTO();
            Assert.NotNull(historialPuntoDTO);
            Partida p = new Partida(null);
            HistorialPuntoDTO historialPuntoDTO1 = new HistorialPuntoDTO(Guid.NewGuid(), 1, p, DateTime.Now);
            Assert.NotNull(historialPuntoDTO1);
            HistorialPuntoDTO historialPuntoDTO2 = new HistorialPuntoDTO(new HistorialPunto(1, p));
            Assert.NotNull(historialPuntoDTO2);

            Guid guiTemp = Guid.NewGuid();
            historialPuntoDTO1.HistorialPuntoID = guiTemp;
            Assert.Equal(guiTemp, historialPuntoDTO1.HistorialPuntoID);
            Assert.Equal(1, historialPuntoDTO1.Puntos);
            Assert.Null(historialPuntoDTO1.Partida.Usuario);
            DateTime nuevaFecha = DateTime.Now;
            historialPuntoDTO1.FechaRegistro = nuevaFecha;
            Assert.Equal(nuevaFecha, historialPuntoDTO1.FechaRegistro);
        }
    }
}
