using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        public Guid UsuarioID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Usuario(string nombres, string apellidos, string nombreUsuario, string email, string password)
        {
            UsuarioID = Guid.NewGuid();
            Nombres = nombres;
            Apellidos = apellidos;
            NombreUsuario = nombreUsuario;
            Email = email;
            Password = password;
        }

        protected Usuario() { }
    }
}
