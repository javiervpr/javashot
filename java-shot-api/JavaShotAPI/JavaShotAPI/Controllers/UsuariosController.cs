using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;
        public UsuariosController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [Route("insertar")]
        public async Task<IActionResult> Insert([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                UsuarioDTO usuarioResultado = await _usuarioRepository.CrearUsuario(usuarioDTO);
                if (usuarioResultado == null)
                    return BadRequest("Error al registrar el usuario. Intentalo mas tarde.");
                return Ok(new RespuestaAPI<UsuarioDTO>(200, "success", usuarioResultado));
            }
            catch (Exception e)
            {
                return BadRequest("Error al registrar el usuario. Intentalo mas tarde.");
            }
        }
    }
}