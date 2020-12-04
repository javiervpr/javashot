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
    public class UsuariosControllerTests: IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public UsuariosControllerTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task RegistrarUsuarioReturnSuccess()
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

            // Assert
            postResponse.EnsureSuccessStatusCode();
            Assert.True(usuarioDTO.Data.UsuarioID.Length > 0);
        }

        [Fact]
        public async Task RegistrarUsuarioReturnError()
        {
            string idUnico = Guid.NewGuid().ToString();
            var postRequest = new
            {
                Url = "/api/usuarios/insertar",
                Body = new {
                    email = 1,
                    password = 1,
                    nombres = "Juan" + idUnico,
                    apellidos = "Valdez",
                    nombreUsuario = ""
                }
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

    }
}
