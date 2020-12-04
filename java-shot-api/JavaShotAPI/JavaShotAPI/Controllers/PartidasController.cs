using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaShotAPI.DTOs;
using JavaShotAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JavaShotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidasController : ControllerBase
    {
        private readonly PartidaRepository _partidaRepository;

        public PartidasController(PartidaRepository partidaRepository)
        {
            _partidaRepository = partidaRepository;
        }

        [HttpPost]
        [Route("crear-partida")]
        public async Task<IActionResult> CrearPartida([FromBody] PartidaDTO partidaDTO)
        {
            PartidaDTO partidaDTOResultado = await _partidaRepository.CrearPartida(partidaDTO);
            if (partidaDTOResultado != null)
                return Ok(new RespuestaAPI<PartidaDTO>(200, "success", partidaDTOResultado));
            else
                return BadRequest(new RespuestaAPI<string>(200, "error", "Ocurrio un error al crear la partida intentelo de nuevo"));
        }

        [HttpGet]
        [Route("obtener-partida-actual/{usuarioID}")]
        public async Task<IActionResult> ObtenerPartidaActual([FromRoute] string usuarioID)
        {
            PartidaDTO partidaDTOResultado = await _partidaRepository.GetPartidaEnProgreso(usuarioID);
            if (partidaDTOResultado != null)
                return Ok(new RespuestaAPI<PartidaDTO>(200, "success", partidaDTOResultado));
            else
                return BadRequest("Ocurrio un error al obtener la partida actual intentelo de nuevo");
        }
    }
}