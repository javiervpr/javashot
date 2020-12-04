using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JavaShotAPI;
using JavaShotAPI.DTOs;
using JavaShotAPI.Models;
using Newtonsoft.Json;
using Xunit;

namespace JavaShotAPIIntegrationTest.TestClasses
{
    public class PreguntasControllerTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public PreguntasControllerTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task GetAllPreguntasReturnSucces()
        {
            // Arrange
            var request = "/api/Preguntas";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetPreguntaByIDReturnSucces()
        {
            // Arrange
            var request = "/api/Preguntas/b8389625-4a76-4d62-87cb-18cd58cad7dc";

            // Act
            var response = await Client.GetAsync(request);
            var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
            RespuestaAPI<PreguntaDTO> respuestaAPI = JsonConvert.DeserializeObject<RespuestaAPI<PreguntaDTO>>(jsonFromPostResponse);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("b8389625-4a76-4d62-87cb-18cd58cad7dc".ToLower(), respuestaAPI.Data.PreguntaID.ToString());
        }
        [Fact]
        public async Task GetPreguntaByIDReturnError()
        {
            // Arrange
            var request = "/api/Preguntas/invalid";

            // Act
            var response = await Client.GetAsync(request);
            var jsonFromPostResponse = await response.Content.ReadAsStringAsync();

            // Assert
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Assert.False(false);
                return;
            }
            Assert.True(true);
        }

        [Fact]
        public async Task InsertarPreguntaSuccess()
        {
            List<Respuesta> respuestas = new List<Respuesta>();
            Respuesta respuesta1 = new Respuesta();
            respuesta1.Contenido = "Respuesta 1"; respuesta1.Correcta = true;
            Respuesta respuesta2 = new Respuesta();
            respuesta2.Contenido = "Respuesta 2"; respuesta1.Correcta = false;
            respuestas.Add(respuesta1);
            respuestas.Add(respuesta2);
            PreguntaDTO preguntaDTO = new PreguntaDTO(Guid.NewGuid().ToString(), "¿Pregunta de prueba?", DateTime.Now, respuestas, null,"Explicacion pregunta");
            var postRequest = new
            {
                Url = "/api/Preguntas/insertar",
                Body = preguntaDTO,
            };

            // Act
            var postResponse = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            RespuestaAPI<string> respuestaAPI = JsonConvert.DeserializeObject<RespuestaAPI<string>>(jsonFromPostResponse);

            // Assert
            postResponse.EnsureSuccessStatusCode();
            Assert.Equal("Preguntas y respuestas registradas correctamente", respuestaAPI.Data);
        }

        [Fact]
        public async Task InsertarPreguntaError()
        {
            PreguntaDTO preguntaDTO = new PreguntaDTO(Guid.NewGuid().ToString(), "¿Pregunta de prueba?", DateTime.Now, null, null, "Explicacion pregunta");
            var postRequest = new
            {
                Url = "/api/Preguntas/insertar",
                Body = preguntaDTO,
            };

            // Act
            var postResponse = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            // Assert
            try
            {
                postResponse.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Assert.False(false);
                return;
            }
        }

        [Fact]
        public async Task InsertarPreguntasSuccess()
        {
            // Arrenge
            #region Arrenge
            List<Respuesta> respuestas = new List<Respuesta>();
            respuestas.Add(new Respuesta("Respuesta 1", true));
            respuestas.Add(new Respuesta("Respuesta 2", false));
            PreguntaDTO preguntaDTO = new PreguntaDTO(Guid.NewGuid().ToString(), "1 ¿Pregunta de prueba?", DateTime.Now, respuestas, null, "Explicacion pregunta");

            List<Respuesta> respuestas2 = new List<Respuesta>();
            respuestas2.Add(new Respuesta("2 Respuesta 1", false));
            respuestas2.Add(new Respuesta("2 Respuesta 2", true));
            PreguntaDTO preguntaDTO2 = new PreguntaDTO(Guid.NewGuid().ToString(), "2 ¿Pregunta de prueba?", DateTime.Now, respuestas2, null, "Explicacion pregunta");
            List<PreguntaDTO> preguntaDTOs = new List<PreguntaDTO>();
            preguntaDTOs.Add(preguntaDTO);
            preguntaDTOs.Add(preguntaDTO2);
            #endregion

            var postRequest = new
            {
                Url = "/api/Preguntas/insertar-lista",
                Body = preguntaDTOs,
            };

            // Act
            var postResponse = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            RespuestaAPI<string> respuestaAPI = JsonConvert.DeserializeObject<RespuestaAPI<string>>(jsonFromPostResponse);

            // Assert
            postResponse.EnsureSuccessStatusCode();
            Assert.Equal("Preguntas y respuestas registradas correctamente", respuestaAPI.Data);
        }

        [Fact]
        public async Task InsertarPreguntasError()
        {
            // Arrenge
            #region Arrenge
            PreguntaDTO preguntaDTO = new PreguntaDTO(Guid.NewGuid().ToString(), "1 ¿Pregunta de prueba?", DateTime.Now, null, null, "Explicacion pregunta");

            List<Respuesta> respuestas2 = new List<Respuesta>();
            respuestas2.Add(new Respuesta("2 Respuesta 1", false));
            respuestas2.Add(new Respuesta("2 Respuesta 2", true));
            PreguntaDTO preguntaDTO2 = new PreguntaDTO(Guid.NewGuid().ToString(), "2 ¿Pregunta de prueba?", DateTime.Now, respuestas2, null, "Explicacion pregunta");
            List<PreguntaDTO> preguntaDTOs = new List<PreguntaDTO>();
            preguntaDTOs.Add(preguntaDTO);
            preguntaDTOs.Add(preguntaDTO2);
            #endregion

            var postRequest = new
            {
                Url = "/api/Preguntas/insertar-lista",
                Body = preguntaDTOs,
            };
            // Act
            var postResponse = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            // Assert
            try
            {
                postResponse.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Assert.False(false);
                return;
            }
        }

        [Fact]
        public async Task ResponderPreguntaReturnSuccess()
        {
            // Arrange
            #region registrar usuario
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
            // Act
            var crearPartidaResponse = await Client.PostAsync(postCrearPartida.Url, ContentHelper.GetStringContent(postCrearPartida.Body));
            var jsonFromPostResponseCrearPartida = await crearPartidaResponse.Content.ReadAsStringAsync();

            RespuestaAPI<PartidaDTO> partidaDTO = JsonConvert.DeserializeObject<RespuestaAPI<PartidaDTO>>(jsonFromPostResponseCrearPartida);
            #region


            // Act
            var postResponderPregunta = new
            {
                Url = "/api/Preguntas/responder-pregunta",
                Body = new
                {
                    partidaPreguntaID = partidaDTO.Data.Preguntas.First().PartidaPreguntaID,
                    respuestaID = partidaDTO.Data.Preguntas.First().Respuestas.First().RespuestaID
                }
            };
            #endregion
            // Act
            var responderPreguntaResponse = await Client.PostAsync(postResponderPregunta.Url, ContentHelper.GetStringContent(postResponderPregunta.Body));
            var jsonFromPostResponseResponderPregunta = await responderPreguntaResponse.Content.ReadAsStringAsync();

            RespuestaAPI<bool> respuestaPreguntaAPI = JsonConvert.DeserializeObject<RespuestaAPI<bool>>(jsonFromPostResponseResponderPregunta);

            // Assert
            responderPreguntaResponse.EnsureSuccessStatusCode();
            if (respuestaPreguntaAPI.Data)
                Assert.True(true == partidaDTO.Data.Preguntas.First().Respuestas.First().Correcta);
            else
                Assert.False(true == partidaDTO.Data.Preguntas.First().Respuestas.First().Correcta);

        }

        [Fact]
        public async Task ResponderPreguntaReturnError()
        {
            // Arrenge
            var postResponderPregunta = new
            {
                Url = "/api/Preguntas/responder-pregunta",
                Body = new
                {
                }
            };
            // Act
            var responderPreguntaResponse = await Client.PostAsync(postResponderPregunta.Url, ContentHelper.GetStringContent(postResponderPregunta.Body));
            // Assert
            try
            {
                responderPreguntaResponse.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Assert.False(false);
                return;
            }
        }

        }
}
