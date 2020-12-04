using JavaShotAPI.DALContext;
using JavaShotAPI.DTOs;
using JavaShotAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.Repositories
{
    public class PreguntaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PreguntaRepository> _logger;


        public PreguntaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserta en la DB la pregunta con sus respuestas opcionales en las tablas correspondientes
        /// </summary>
        /// <param name="preguntaDTO"></param>
        /// <returns>Retorna etrue si el proceso fue correcto y false si sale error</returns>
        public async Task<bool> Insert(PreguntaDTO preguntaDTO)
        {
            try
            {
                //_logger.LogInformation("PreguntaRepository Insert", preguntaDTO);
                Pregunta pregunta = new Pregunta(preguntaDTO.Titulo, preguntaDTO.ExplicacionRespuesta);
                await _context.AddAsync(pregunta);
                foreach (Respuesta respuestaDTO in preguntaDTO.Respuestas)
                {
                    Respuesta respuesta = new Respuesta(respuestaDTO.Contenido, respuestaDTO.Correcta);
                    await _context.AddAsync(respuesta);
                    PreguntaRespuesta preguntaRespuesta = new PreguntaRespuesta(pregunta, respuesta);
                    await _context.AddAsync(preguntaRespuesta);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //_logger.LogError(string.Format("ERROR {0}", e.Message), e);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Inserta en la DB la pregunta con sus respuestas opcionales en las tablas correspondientes
        /// </summary>
        /// <param name="preguntaDTO"></param>
        /// <returns>Retorna etrue si el proceso fue correcto y false si sale error</returns>
        public async Task<bool> InsertList(List<PreguntaDTO> list)
        {
            try
            {
                //_logger.LogInformation("PreguntaRepository Insert", preguntaDTO);
                foreach (PreguntaDTO preguntaDTO in list)
                {
                    Pregunta pregunta = new Pregunta(preguntaDTO.Titulo, preguntaDTO.ExplicacionRespuesta);
                    await _context.AddAsync(pregunta);
                    foreach (Respuesta respuestaDTO in preguntaDTO.Respuestas)
                    {
                        Respuesta respuesta = new Respuesta(respuestaDTO.Contenido, respuestaDTO.Correcta);
                        await _context.AddAsync(respuesta);
                        PreguntaRespuesta preguntaRespuesta = new PreguntaRespuesta(pregunta, respuesta);
                        await _context.AddAsync(preguntaRespuesta);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //_logger.LogError(string.Format("ERROR {0}", e.Message), e);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Utilizado para saber si respondio correctamente una pregunta
        /// </summary>
        /// <returns>Retorna true si la respuesta es correcta, false si es incorrecta y null si hay error </returns>
        public async Task<bool?> ResponderPregunta(string partidaPreguntaID, string respuestaID)
        {
            try
            {
                PartidaPregunta partidaPregunta = await _context.PartidaPreguntas
                    .Where(p => p.PartidaPreguntaID.Equals(Guid.Parse(partidaPreguntaID))).FirstOrDefaultAsync();
                if (partidaPregunta == null)
                    return null;
                PreguntaRespuesta preguntaRespuesta = await _context.PreguntaRespuestas.Include(p => p.Pregunta).Include(o => o.Respuesta)
                    .Where(p => p.Pregunta.PreguntaID.Equals(partidaPregunta.PreguntaID)
                            && p.Respuesta.RespuestaID.Equals(Guid.Parse(respuestaID))
                    ).FirstOrDefaultAsync();
                if (preguntaRespuesta == null)
                    return null;

                if (preguntaRespuesta.Respuesta.Correcta) // Registramos el punto
                {
                    HistorialPuntoRepository historialPuntoRepository = new HistorialPuntoRepository(_context);
                    bool resultadoRegistroPunto = await historialPuntoRepository.RegistrarPuntos(partidaPregunta.PartidaID.ToString());
                    if (!resultadoRegistroPunto)
                        return null;
                }
                partidaPregunta.ContestadaCorrectamente = preguntaRespuesta.Respuesta.Correcta;
                partidaPregunta.Contestada = true;
                await _context.SaveChangesAsync();
                    return preguntaRespuesta.Respuesta.Correcta;
                //return consultaPreguntaRespuesta.PreguntaRespuesta.Respuesta.Correcta;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Devuelve la lista de todas las preguntas con sus respuestas
        /// </summary>
        /// <returns>Lista de PreguntaDTO</returns>
        public async Task<List<PreguntaDTO>> GetAllPreguntas()
        {
            try
            {
                List<PreguntaDTO> preguntaDTOs = new List<PreguntaDTO>();
                List<Pregunta> preguntas = await _context.Preguntas.ToListAsync();
                foreach (Pregunta pregunta in preguntas)
                {
                    List<Respuesta> respuestas = await _context.PreguntaRespuestas.Include(p => p.Respuesta)
                    .Where(p => p.Pregunta.PreguntaID.Equals(pregunta.PreguntaID))
                    .Select(s => new Respuesta()
                    {
                        RespuestaID = s.Respuesta.RespuestaID,
                        Contenido = s.Respuesta.Contenido,
                        Correcta = s.Respuesta.Correcta
                    })
                    .ToListAsync();
                    preguntaDTOs.Add(new PreguntaDTO(pregunta.PreguntaID.ToString(), pregunta.Titulo, pregunta.FechaRegistro, respuestas, null, pregunta.ExplicacionRespuesta));
                }
                return preguntaDTOs;
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format("ERROR {0}", e.Message), e);
                return null;
            }
        }

        /// <summary>
        /// Devuelve la lista de todas las preguntas
        /// </summary>
        /// <returns>Lista de PreguntaDTO</returns>
        public async Task<PreguntaDTO> GetPregunta(string preguntaID, string partidaPreguntaID = null , bool? contestada = false, bool? contestadaCorrectamente = false)
        {
            try
            {
                Guid preguntaGuid = Guid.Parse(preguntaID);
                Pregunta pregunta = await _context.Preguntas.Where(p => p.PreguntaID.Equals(preguntaGuid)).FirstOrDefaultAsync();
                List<Respuesta> respuestas = await _context.PreguntaRespuestas.Include(p => p.Respuesta)
                    .Where(p => p.Pregunta.PreguntaID.Equals(preguntaGuid))
                    .Select(s => new Respuesta()
                    {
                        RespuestaID = s.Respuesta.RespuestaID,
                        Contenido = s.Respuesta.Contenido,
                        Correcta = false//s.Respuesta.Correcta
                    })
                    .ToListAsync();
                PreguntaDTO preguntaDTO = new PreguntaDTO(pregunta.PreguntaID.ToString(), pregunta.Titulo, pregunta.FechaRegistro, respuestas, partidaPreguntaID,pregunta.ExplicacionRespuesta, contestada, contestadaCorrectamente);
                return preguntaDTO;
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format("ERROR {0}", e.Message), e);
                return null;
            }
        }

    }
}
