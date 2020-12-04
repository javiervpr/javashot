using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.DTOs
{
    public class UsuarioDTO
    {
        public string UsuarioID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UsuarioDTO(string usuarioID, string nombres, string apellidos, string nombreUsuario, string email)
        {
            UsuarioID = usuarioID;
            Nombres = nombres;
            Apellidos = apellidos;
            NombreUsuario = nombreUsuario;
            Email = email;
        }

        public UsuarioDTO()
        {
        }
    }
}
