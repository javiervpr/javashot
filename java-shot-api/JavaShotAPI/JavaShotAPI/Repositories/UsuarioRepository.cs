using JavaShotAPI.DALContext;
using JavaShotAPI.DTOs;
using JavaShotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.Repositories
{
    public class UsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioDTO> CrearUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                Usuario usuarioCreado = new Usuario(usuarioDTO.Nombres, usuarioDTO.Apellidos, usuarioDTO.NombreUsuario, usuarioDTO.Email, usuarioDTO.Password);
                await _context.AddAsync(usuarioCreado);
                await _context.SaveChangesAsync();
                return new UsuarioDTO(usuarioCreado.UsuarioID.ToString(),usuarioCreado.Nombres, usuarioCreado.Apellidos, usuarioCreado.NombreUsuario, usuarioCreado.Email);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
