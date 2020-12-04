using JavaShotAPI.DALContext;
using JavaShotAPI.DTOs;
using JavaShotAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JavaShotAPI.Repositories
{
    public class PartidaRepository
    {
        private readonly ApplicationDbContext _context;

        public PartidaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// CrearPartida inserta los registros correspondientes en la tabla Partida y PartidaPregunta 
        /// </summary>
        /// <returns>Retorna true si creo los registros exitosamente y false si se produce algun error</returns>
        public async Task<PartidaDTO> CrearPartida(PartidaDTO partidaDTO)
        {
            try
            {
                PreguntaRepository preguntaRepositor = new PreguntaRepository(_context);
                Usuario usuario = await _context.Usuarios.Where(u => u.UsuarioID.Equals(partidaDTO.Usuario.UsuarioID)).FirstOrDefaultAsync();
                Partida partida = new Partida(usuario);
                await _context.AddAsync(partida);
                List<Pregunta> listaPreguntas;
                if (partidaDTO.CantidadPreguntas > 0) 
                    listaPreguntas = await GenerarPreguntas(usuario.UsuarioID.ToString(), partidaDTO.CantidadPreguntas);
                else
                    listaPreguntas = await GenerarPreguntas(usuario.UsuarioID.ToString());
                List<PreguntaDTO> preguntasDTOs = new List<PreguntaDTO>();
                foreach (Pregunta pregunta in listaPreguntas)
                {
                    PartidaPregunta partidaPregunta = new PartidaPregunta(partida, pregunta);
                    await _context.AddAsync(partidaPregunta);
                    PreguntaDTO preguntaDTO = await preguntaRepositor.GetPregunta(pregunta.PreguntaID.ToString(),partidaPregunta.PartidaPreguntaID.ToString());
                    preguntasDTOs.Add(preguntaDTO);
                }
                await _context.SaveChangesAsync();
                PartidaDTO partidaDTOResultado = new PartidaDTO(partida.PartidaID, null, partida.FechaRegistro, preguntasDTOs, 0);
                return partidaDTOResultado;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Devuelve la partida en progreso si hay alguna partida que tenga respuesta sin contestar para un usuario
        /// </summary>
        /// <param name="usuarioID">El id del usuario</param>
        /// <returns>
        /// Retorna la partida en progreso
        /// Retorna null en caso de no encontrar una partida para el usuario
        /// </returns>
        public async Task<PartidaDTO> GetPartidaEnProgreso(string usuarioID)
        {
            try
            {
                PreguntaRepository preguntaRepositor = new PreguntaRepository(_context);
                List<PartidaPregunta> partidaPreguntasSinResponder = await _context.PartidaPreguntas
                    .Include(p => p.Partida)
                    .Include(pr => pr.Pregunta)
                    .Where(p => p.Partida.Usuario.UsuarioID.Equals(Guid.Parse(usuarioID)) &&
                       p.Contestada == false
                ).ToListAsync();
                if (partidaPreguntasSinResponder == null || partidaPreguntasSinResponder.Count() == 0)
                    return null;
                List<PartidaPregunta> partidaPreguntas = await _context.PartidaPreguntas
                   .Include(p => p.Partida)
                   .Include(pr => pr.Pregunta)
                   .Where(p => p.Partida.Usuario.UsuarioID.Equals(Guid.Parse(usuarioID)) &&
                      p.Partida.PartidaID.Equals(partidaPreguntasSinResponder.First().Partida.PartidaID)
               ).ToListAsync();

                Partida partida = partidaPreguntas.First().Partida;
                List<PreguntaDTO> preguntasDTOs = new List<PreguntaDTO>();
                foreach (PartidaPregunta partidaPregunta in partidaPreguntas)
                {
                    PreguntaDTO preguntaDTO = await preguntaRepositor.GetPregunta(partidaPregunta.Pregunta.PreguntaID.ToString(), partidaPregunta.PartidaPreguntaID.ToString(), 
                        partidaPregunta.Contestada, partidaPregunta.ContestadaCorrectamente);
                    preguntasDTOs.Add(preguntaDTO);
                }
                PartidaDTO partidaDTOResultado = new PartidaDTO(partida.PartidaID, null, partida.FechaRegistro, preguntasDTOs, 0);
                return partidaDTOResultado;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        /// <summary>
        /// Devuelve una lista de preguntas, toma como prioridad retornar preguntas que no han sido
        /// respondidas por el usuario pero si ya respondio todas retorna preguntas repetidas.
        /// </summary>
        /// <param name="usuarioID">El usuario para el cual se generaran las preguntas</param>
        /// <param name="numeroPreguntas">La cantidad de preguntas que se generaran</param>
        /// <returns></returns>
        public async Task<List<Pregunta>> GenerarPreguntas(string usuarioID, int numeroPreguntas = 5)
        {
            List<Pregunta> listaPreguntas = new List<Pregunta>();

            // Obtengo las preguntas respondidas correctamente por el usuario en todas las partida
            List<PartidaPregunta> preguntasRespondidas = await _context.PartidaPreguntas.Include(i => i.Partida)
                .Where(p => p.ContestadaCorrectamente == true && p.Partida.Usuario.UsuarioID.Equals(Guid.Parse(usuarioID)))
                .ToListAsync();
            // Obtengo todas las preguntas disponibles
            List<Pregunta> todasLasPreguntas = await _context.Preguntas.ToListAsync();
            // Agrego a estas listas las preguntas disponibles (Que no ha respondido el usuario)
            List<Pregunta> listaPreguntasDisponibles = new List<Pregunta>();
            foreach (Pregunta pregunta in todasLasPreguntas)
            {
                bool preguntaValida = true;
                foreach (PartidaPregunta partidaPregunta in preguntasRespondidas)
                {   // Si la pregunta no esta en las preguntas respondidas se la añade a las preguntas disponibles
                    if (partidaPregunta.Pregunta.PreguntaID.Equals(pregunta.PreguntaID))
                    {
                        preguntaValida = false;
                        break;
                        //if (listaPreguntasDisponibles.Where(p => p.PreguntaID.Equals(pregunta.PreguntaID)).Count() > 0)
                        //    continue;
                        //listaPreguntasDisponibles.Add(pregunta);
                    }
                }
                if (preguntaValida)
                    listaPreguntasDisponibles.Add(pregunta);
            }
            // 1ra condicion -> Sino ha respondido ninguna pregunta se trabajara con todas las preguntas registradas randomicamente
            // 2da condicion -> Si ya ha respondido todas las preguntas se trabajara con todas las preguntas registradas randomicamente
            if (preguntasRespondidas.Count() == 0 || listaPreguntasDisponibles.Count() == 0)
            {
                listaPreguntasDisponibles = todasLasPreguntas;
            }
            // Si las preguntas disponibles no son suficientes, se asignaran preguntas que el usuario ya haya respondido para cumplir con el numero de preguntas requerido
            if (listaPreguntasDisponibles.Count() > 0 && listaPreguntasDisponibles.Count() < numeroPreguntas)
            {
                List<int> listaIndexPreguntasRespondiasAdicionadas = new List<int>();
                Random random = new Random();
                int contadorPreguntasRespondidasAdicionadas = listaPreguntasDisponibles.Count();
                while (contadorPreguntasRespondidasAdicionadas < numeroPreguntas)
                {
                    int indexRandom = random.Next(0, todasLasPreguntas.Count());
                    if (listaIndexPreguntasRespondiasAdicionadas.Where(li => li == indexRandom).Count() > 0)
                    {
                        continue;
                    }
                    else
                    {
                        // Se verifica que la pregunta a adicionar no este repetida en las preguntas disponibles
                        if (listaPreguntasDisponibles.Where(p => p.PreguntaID.Equals(todasLasPreguntas[indexRandom].PreguntaID)).Count() > 0)
                        {
                            continue;
                        }
                        listaIndexPreguntasRespondiasAdicionadas.Add(indexRandom);
                        listaPreguntasDisponibles.Add(todasLasPreguntas[indexRandom]);
                        contadorPreguntasRespondidasAdicionadas++;
                    }
                }
                // 
            }


            //
            Random rnd = new Random();
            List<int> listaIndexPreguntasGeneradas = new List<int>();
            int contadorPreguntasGeneradas = 0;
            while (contadorPreguntasGeneradas < numeroPreguntas)
            {
                int index = rnd.Next(0, listaPreguntasDisponibles.Count());
                if (contadorPreguntasGeneradas == 0)
                {
                    listaIndexPreguntasGeneradas.Add(index);
                    listaPreguntas.Add(listaPreguntasDisponibles[index]);
                    contadorPreguntasGeneradas++;
                    Console.WriteLine("Pregunta# -> " + contadorPreguntasGeneradas + " pregunta db index -> " + index);
                    continue;
                }

                if (listaIndexPreguntasGeneradas.Where(li => li == index).Count() > 0)
                {
                    continue;
                }
                else
                {
                    listaIndexPreguntasGeneradas.Add(index);
                    listaPreguntas.Add(listaPreguntasDisponibles[index]);
                    contadorPreguntasGeneradas++;
                }

                Console.WriteLine("Pregunta# -> " + contadorPreguntasGeneradas + " pregunta db index -> " + index);
            }
            //


            return listaPreguntas;
        }

        //public async Task<List<Partida>> GetAllPartida()
        //{
        //    List<Partida> result = await _context.Partidas.ToListAsync();
        //    return result;
        //}
    }
}
