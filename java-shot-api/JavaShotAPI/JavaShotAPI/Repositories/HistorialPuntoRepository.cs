using JavaShotAPI.DALContext;
using JavaShotAPI.DTOs;
using JavaShotAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.Repositories
{
    public class HistorialPuntoRepository
    {
        private readonly ApplicationDbContext _context;

        public HistorialPuntoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra puntos al historial que depende de una partida
        /// </summary>
        /// <param name="partidaID"></param>
        /// <param name="puntos"></param>
        /// <returns>
        /// True -> si es exitoso
        /// False -> si falla
        /// </returns>
        public async Task<bool> RegistrarPuntos(string partidaID, int puntos = 1)
        {
            try
            {
                Partida partida = await _context.Partidas.Where(p => p.PartidaID.Equals(Guid.Parse(partidaID))).FirstOrDefaultAsync();
                if (partida == null)
                    return false;
                HistorialPunto historialPunto = new HistorialPunto(puntos, partida);
                await _context.AddAsync(historialPunto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<PuntoPersonaDTO> GetPuntos(string usuarioID)
        {
            var historialPuntos = await _context.HistorialPuntos
                   .Include(u => u.Partida)
                   .GroupBy(item =>
                       item.Partida.Usuario.UsuarioID
                   )
                   .Select(group => new
                   {
                       UsuarioID = group.Key,
                       Puntos = group.Sum(item => item.Puntos)
                   }).Where(u => u.UsuarioID.Equals(Guid.Parse(usuarioID)))
                   .ToListAsync();

            PuntoPersonaDTO puntoPersonaDTO = historialPuntos.Join(_context.Usuarios,
                historialPuntoID => historialPuntoID.UsuarioID,
                historialPuntosJoin => historialPuntosJoin.UsuarioID,
                (historialPuntoID, historialPuntosJoin) => new { first = historialPuntoID, historialPuntosJoin }
                ).Select(s => new PuntoPersonaDTO
                {
                    Puntos = s.first.Puntos,
                    NombreUsuario = s.historialPuntosJoin.Nombres,
                    ApellidoUsuario = s.historialPuntosJoin.Apellidos,
                    UsuarioID = s.historialPuntosJoin.UsuarioID.ToString()

                }).FirstOrDefault();
            ;
            return puntoPersonaDTO;
        }

        public async Task<List<PuntoPersonaDTO>> GetPuntosDeTodos()
        {
           var historialPuntos = await _context.HistorialPuntos
                    .Include(u => u.Partida)
                    .GroupBy(item => 
                        item.Partida.Usuario.UsuarioID
                    )
                    .Select(group => new 
                    {
                        UsuarioID = group.Key,
                        Puntos = group.Sum(item => item.Puntos)
                    }).ToListAsync();

            List<PuntoPersonaDTO> lista = historialPuntos.Join(_context.Usuarios,
                historialPuntoID => historialPuntoID.UsuarioID,
                historialPuntosJoin => historialPuntosJoin.UsuarioID,
                (historialPuntoID, historialPuntosJoin) => new { first = historialPuntoID, historialPuntosJoin }
                ).Select(s => new PuntoPersonaDTO
                {
                    Puntos = s.first.Puntos,
                    NombreUsuario = s.historialPuntosJoin.Nombres,
                    ApellidoUsuario = s.historialPuntosJoin.Apellidos,
                    UsuarioID = s.historialPuntosJoin.UsuarioID.ToString()

                }).OrderByDescending(x => x.Puntos).ToList();
                ;
            return lista;
        }
    }
}
