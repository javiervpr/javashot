using System;
using System.Net.Http;
using System.Threading.Tasks;
using JavaShotAPI;
using JavaShotAPI.DTOs;
using JavaShotAPI.Models;
using Newtonsoft.Json;
using Xunit;


namespace JavaShotAPIIntegrationTest.TestClasses
{
    public class PartidasControllerTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;
        public PartidasControllerTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task ObtenerPartidaActualTestError()
        {
            #region registrar usuario
            string idUnico = Guid.NewGuid().ToString();
            var postRequest = new
            {
                Url = "/api/usuarios/insertar",
                Body = new
                {
                    email = "juanvaldez_"+ idUnico +"@ejemplo.com",
                    password = "123456",
                    nombres = "Juan" + idUnico,
                    apellidos = "Valdez",
                    nombreUsuario = ""
                }
            };


            var usuarioResponse = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponseUsuario = await usuarioResponse.Content.ReadAsStringAsync();

            RespuestaAPI<UsuarioDTO> usuarioDTO = JsonConvert.DeserializeObject<RespuestaAPI<UsuarioDTO>>(jsonFromPostResponseUsuario);
            #endregion

            #region crearpartida
            PartidaDTO partidaDtoParam = new PartidaDTO();
            partidaDtoParam.Usuario = new Usuario(usuarioDTO.Data.Nombres, usuarioDTO.Data.Apellidos, "", usuarioDTO.Data.Email, "");
            partidaDtoParam.Usuario.UsuarioID = Guid.Parse(usuarioDTO.Data.UsuarioID);
            var postCrearPartida = new
            {
                Url = "/api/Partidas/crear-partida",
                Body = partidaDtoParam
            };
            #endregion
            
            var crearPartidaResponse = await Client.PostAsync(postCrearPartida.Url, ContentHelper.GetStringContent(postCrearPartida.Body));
            var jsonFromPostResponseCrearPartida = await crearPartidaResponse.Content.ReadAsStringAsync();

            RespuestaAPI<PartidaDTO> partidaDTO = JsonConvert.DeserializeObject<RespuestaAPI<PartidaDTO>>(jsonFromPostResponseCrearPartida);
            //
            //Arrange
            string url = "/api/Partidas/obtener-partida-actual/" + usuarioDTO.Data.UsuarioID.ToString();
            //Act
            var postResponseObtenerPartida = await Client.GetAsync(url);
            var jsonFromPostResponseObtenerPartida = await postResponseObtenerPartida.Content.ReadAsStringAsync();
            RespuestaAPI<PartidaDTO> partidaDTOActual = JsonConvert.DeserializeObject<RespuestaAPI<PartidaDTO>>(jsonFromPostResponseObtenerPartida);
            //Assert
            postResponseObtenerPartida.EnsureSuccessStatusCode();
            Assert.Equal("success", partidaDTOActual.Mensaje);
        }

        [Fact]
        public async Task CrearPartidaTest()
        {
            // Arrange

            string idUnico = Guid.NewGuid().ToString();
            var postRequest = new
            {
                Url = "/api/usuarios/insertar",
                Body = new
                {
                    email = "juanvaldez_" + idUnico + "@ejemplo.com",
                    password = "123456",
                    nombres = "Juan" + idUnico,
                    apellidos = "Valdez",
                    nombreUsuario = ""
                }
            };


            var usuarioResponse = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponseUsuario = await usuarioResponse.Content.ReadAsStringAsync();

            RespuestaAPI<UsuarioDTO> usuarioDTO = JsonConvert.DeserializeObject<RespuestaAPI<UsuarioDTO>>(jsonFromPostResponseUsuario);
            

            
            PartidaDTO partidaDtoParam = new PartidaDTO();
            partidaDtoParam.Usuario = new Usuario(usuarioDTO.Data.Nombres, usuarioDTO.Data.Apellidos, "", usuarioDTO.Data.Email, "");
            partidaDtoParam.Usuario.UsuarioID = Guid.Parse(usuarioDTO.Data.UsuarioID);
            var postCrearPartida = new
            {
                Url = "/api/Partidas/crear-partida",
                Body = partidaDtoParam
            };
            
            // Act
            var crearPartidaResponse = await Client.PostAsync(postCrearPartida.Url, ContentHelper.GetStringContent(postCrearPartida.Body));
            var jsonFromPostResponseCrearPartida = await crearPartidaResponse.Content.ReadAsStringAsync();

            RespuestaAPI<PartidaDTO> partidaDTO = JsonConvert.DeserializeObject<RespuestaAPI<PartidaDTO>>(jsonFromPostResponseCrearPartida);
            

            crearPartidaResponse.EnsureSuccessStatusCode();
            Assert.Equal("success", partidaDTO.Mensaje);
        }

        [Fact]
        public void CrearPartidaPreguntaDTOTestSuccess()
        {
            //Arrange
            Pregunta pregunta = new Pregunta("Pregunta 1", "Explicacion");
            Usuario usuario = new Usuario("javier", "vpr", "javiervpr", "javier@ejemplo.com", "1234556789");
            Partida partida = new Partida(usuario);
            //Act
            PartidaPregunta partidaPregunta = new PartidaPregunta(partida, pregunta);
            partidaPregunta.PartidaID = partida.PartidaID;
            partidaPregunta.PreguntaID = pregunta.PreguntaID;
            DateTime fechaActual = DateTime.Now;
            partidaPregunta.FechaRespuesta = fechaActual;
            //Assert
            Assert.Equal(partidaPregunta.FechaRespuesta, fechaActual);
            Assert.Equal(partida.PartidaID, partidaPregunta.PartidaID);
            Assert.Equal(pregunta.PreguntaID, partidaPregunta.PreguntaID);

        }
    }
}
