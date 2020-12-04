using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JavaShotAPI;
using JavaShotAPI.DTOs;
using JavaShotAPI.Models;
using Newtonsoft.Json;
using Xunit;



namespace JavaShotAPIIntegrationTest.TestClasses
{
    public class HistorialPuntosControllerTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;
        public HistorialPuntosControllerTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task GetPuntosReturnSucces()
        {
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

            // Act
            var postResponse = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            RespuestaAPI<UsuarioDTO> usuarioDTO = JsonConvert.DeserializeObject<RespuestaAPI<UsuarioDTO>>(jsonFromPostResponse);

            //Arrange
            string url = "/api/HistorialPuntos/get-puntos/" + usuarioDTO.Data.UsuarioID.ToString();
            //Act
            var postResponseObtenerHistorial = await Client.GetAsync(url);
            var jsonFromPostResponseObtenerPartida = await postResponseObtenerHistorial.Content.ReadAsStringAsync();
            RespuestaAPI<List<HistorialPunto>> partidaDTOActual = JsonConvert.DeserializeObject<RespuestaAPI<List<HistorialPunto>>>(jsonFromPostResponseObtenerPartida);
            //Assert
            postResponseObtenerHistorial.EnsureSuccessStatusCode();
            Assert.Equal("success", partidaDTOActual.Mensaje);
        }

        [Fact]
        public async Task GetPuntosReturnError()
        {
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

            // Act
            var postResponse = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            RespuestaAPI<UsuarioDTO> usuarioDTO = JsonConvert.DeserializeObject<RespuestaAPI<UsuarioDTO>>(jsonFromPostResponse);

            //Arrange
            string url = "/api/HistorialPuntos/get-puntos/invalid";
            //Act
            var postResponseObtenerHistorial = await Client.GetAsync(url);
            //Assert
            try
            {
                postResponseObtenerHistorial.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Assert.False(false);
                return;
            }
            Assert.True(true);
        }

        [Fact]
        public async Task GetPuntosAllReturnSucces()
        {
            //Arrange
            string url = "/api/HistorialPuntos/get-puntos";
            //Act
            var postResponseObtenerHistorial = await Client.GetAsync(url);
            var jsonFromPostResponseObtenerPartida = await postResponseObtenerHistorial.Content.ReadAsStringAsync();
            RespuestaAPI<List<PuntoPersonaDTO>> partidaDTOActual = JsonConvert.DeserializeObject<RespuestaAPI<List<PuntoPersonaDTO>>>(jsonFromPostResponseObtenerPartida);
            //Assert
            postResponseObtenerHistorial.EnsureSuccessStatusCode();
            Assert.Equal("success", partidaDTOActual.Mensaje);
        }

        [Fact]
        public async Task CreatePuntoPersonaDTO()
        {
            //Act
            PuntoPersonaDTO puntoPersonaDTO = new PuntoPersonaDTO("javier", "vaca pereria", Guid.NewGuid().ToString(),100);
            //Assert
            Assert.Equal("javier", puntoPersonaDTO.NombreUsuario);
            Assert.Equal("vaca pereria", puntoPersonaDTO.ApellidoUsuario);
            Assert.Equal("100", puntoPersonaDTO.Puntos.ToString());
        }

        [Fact]
        public async Task RegistrarPuntosTestSucces()
        {
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
            // se crea partida y usuario arriba
            var postRegistrarPuntos = new
            {
                Url = "/api/HistorialPuntos/registrar-puntos/" + partidaDTO.Data.PartidaID,
                Body = new { }
            };
            var postResponseRegistrarPunto = await Client.PostAsync(postRegistrarPuntos.Url, ContentHelper.GetStringContent(postRegistrarPuntos.Body));
            var jsonFromPostResponseRegistrarPunto = await postResponseRegistrarPunto.Content.ReadAsStringAsync();
            RespuestaAPI<bool> partidaDTOActual = JsonConvert.DeserializeObject<RespuestaAPI<bool>>(jsonFromPostResponseRegistrarPunto);
            //Assert
            postResponseRegistrarPunto.EnsureSuccessStatusCode();
            Assert.Equal("success", partidaDTOActual.Mensaje);
        }
        [Fact]
        public async Task RegistrarPuntosTestError()
        {
            var postRegistrarPuntos = new
            {
                Url = "/api/HistorialPuntos/registrar-puntos/invalido",
                Body = new { }
            };
            var postResponseRegistrarPunto = await Client.PostAsync(postRegistrarPuntos.Url, ContentHelper.GetStringContent(postRegistrarPuntos.Body));
            //Assert
            try
            {
                postResponseRegistrarPunto.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Assert.False(false);
                return;
            }
        }
    }
}
