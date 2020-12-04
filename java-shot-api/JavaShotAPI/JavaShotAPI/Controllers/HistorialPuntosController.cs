using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaShotAPI.DALContext;
using JavaShotAPI.DTOs;
using JavaShotAPI.Models;
using JavaShotAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JavaShotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialPuntosController : ControllerBase
    {
        private readonly HistorialPuntoRepository _historialPuntoRepository;

        public HistorialPuntosController(HistorialPuntoRepository historialPuntoRepository)
        {
            _historialPuntoRepository = historialPuntoRepository;
        }

        [HttpGet]
        [Route("get-puntos/{usuarioID}")]
        public async Task<IActionResult> GetPuntos([FromRoute] string usuarioID)
        {
            try
            {
                PuntoPersonaDTO puntoPersonaDTO = await _historialPuntoRepository.GetPuntos(usuarioID);
                return Ok(new RespuestaAPI<PuntoPersonaDTO>(200, "success", puntoPersonaDTO));
            }
            catch (Exception e)
            {
                return BadRequest("Ocurrio un error intentalo de nuevo");
            }
        }

        [HttpGet]
        [Route("get-puntos")]
        public async Task<IActionResult> GetPuntosAll()
        {
            try
            {
                List<PuntoPersonaDTO> historialPuntos = await _historialPuntoRepository.GetPuntosDeTodos();
                return Ok(new RespuestaAPI<List<PuntoPersonaDTO>>(200, "success", historialPuntos));
            }
            catch (Exception e)
            {
                return BadRequest("Ocurrio un error intentalo de nuevo");
            }
        }

        [HttpPost]
        [Route("registrar-puntos/{partidaID}")]
        public async Task<IActionResult> RegistrarPuntos([FromRoute] string partidaID)
        {
            bool resultado = await _historialPuntoRepository.RegistrarPuntos(partidaID);
            if (resultado)
                return Ok(new RespuestaAPI<bool>(200, "success", resultado));
            else
                return BadRequest("Ocurrio un error intentalo de nuevo");
        }
    }
}