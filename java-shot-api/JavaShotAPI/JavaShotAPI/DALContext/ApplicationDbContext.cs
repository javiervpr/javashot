using JavaShotAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace JavaShotAPI.DALContext
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<PreguntaRespuesta> PreguntaRespuestas { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<PartidaPregunta> PartidaPreguntas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<HistorialPunto> HistorialPuntos { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
