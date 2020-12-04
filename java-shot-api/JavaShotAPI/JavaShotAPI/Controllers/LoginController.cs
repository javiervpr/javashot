using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JavaShotAPI.DALContext;
using JavaShotAPI.DTOs;
using JavaShotAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JavaShotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;


        public LoginController(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody]UsuarioDTO login)
        {
            try
            {
                IActionResult response = Unauthorized("Usuario/Contraseña incorrecta");
                Usuario user = _context.Usuarios.SingleOrDefault(x => x.Email == login.Email && x.Password == login.Password);
                if (user != null)
                {
                    response = Ok(new RespuestaAPI<UsuarioDTO>(200, "success", new UsuarioDTO(user.UsuarioID.ToString(), user.Nombres, user.Apellidos, user.NombreUsuario, user.Email)));
                }
                return response;
            }
            catch (Exception e)
            {
                return BadRequest("Ocurrio un error al procesar la solicitud");
            }

        }

    }
}