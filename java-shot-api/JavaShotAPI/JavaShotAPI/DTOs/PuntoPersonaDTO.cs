using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.DTOs
{
    public class PuntoPersonaDTO
    {
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string UsuarioID { get; set; }
        public int Puntos { get; set; }

        public PuntoPersonaDTO() { }

        public PuntoPersonaDTO(string nombreUsuario, string apellidoUsuario, string usuarioID, int puntos)
        {
            NombreUsuario = nombreUsuario;
            ApellidoUsuario = apellidoUsuario;
            UsuarioID = usuarioID;
            Puntos = puntos;
        }
    }
}
