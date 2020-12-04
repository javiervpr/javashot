using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.Models
{
    [Table("Partida")]
    public class Partida
    {
        public Guid PartidaID { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Partida(Usuario usuario)
        {
            PartidaID = Guid.NewGuid();
            Usuario = usuario;
            FechaRegistro = DateTime.Now;
        }

        protected Partida() { }
    }
}
