using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using JavaShotAPI.DTOs;
using JavaShotAPI.Models;
using JavaShotAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JavaShotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PreguntasController : ControllerBase
    {
        private readonly PreguntaRepository _preguntaRepository;

        public PreguntasController(PreguntaRepository preguntaRepository)
        {
            this._preguntaRepository = preguntaRepository;
        }

        [HttpPost]
        [Route("insertar")]
        public async Task<IActionResult> Insertar([FromBody] PreguntaDTO preguntaDTO)
        {
            bool resultado = await _preguntaRepository.Insert(preguntaDTO);
            if (resultado)
                return Ok(new RespuestaAPI<string>(200, "success", "Preguntas y respuestas registradas correctamente"));
            else
                return BadRequest("Ocurrio un error intentelo de nuevo");
        }

        [HttpPost]
        [Route("insertar-lista")]
        public async Task<IActionResult> InsertarLista([FromBody] List<PreguntaDTO> preguntaDTOs)
        {
            bool resultado = await _preguntaRepository.InsertList(preguntaDTOs);
            if (resultado)
                return Ok(new RespuestaAPI<string>(200, "success", "Preguntas y respuestas registradas correctamente"));
            else
                return BadRequest("Ocurrio un error intentelo de nuevo");
        }

        [HttpPost]
        [Route("responder-pregunta")]
        public async Task<IActionResult> ResponderPregunta([FromBody] ResponderPreguntaDTO responderPreguntaDTO)
        {
            bool? resultado = await _preguntaRepository.ResponderPregunta(responderPreguntaDTO.PartidaPreguntaID, responderPreguntaDTO.RespuestaID);
            if (resultado == null)
                return BadRequest("Ocurrio un error al responder la pregunta, intentalo de nuevo.");
            else
                return Ok(new RespuestaAPI<bool?>(200, "success", resultado));
        }

        [HttpGet]
        [Route("{preguntaID}")]
        public async Task<IActionResult> GetPregunta([FromRoute] string preguntaID)
        {
            PreguntaDTO preguntaDTO = await _preguntaRepository.GetPregunta(preguntaID);
            if (preguntaDTO != null)
                return Ok(new RespuestaAPI<PreguntaDTO>(200, "success", preguntaDTO));
            else
                return NotFound(new RespuestaAPI<string>(200, "error", "Ocurrio un error intentelo de nuevo"));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllPreguntas()
        {
            List<PreguntaDTO> list = await _preguntaRepository.GetAllPreguntas();
            if (list != null)
                return Ok(new RespuestaAPI<List<PreguntaDTO>>(200, "success", list));
            else
                return NotFound(new RespuestaAPI<string>(200, "error", "Ocurrio un error intentelo de nuevo"));
        }
    }
}