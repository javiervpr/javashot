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
    public class LoginControllerTests: IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;
        public LoginControllerTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }
        [Fact]
        public async Task LoginUsuarioReturnSesionSuccess()
        {
            var postRequest = new
            {
                Url = "/api/Login",
                Body = new
                {
                    email = "javier@ejemplo.com",
                    password = "123456"
                }
            };

            // Act
            var postResponse = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            RespuestaAPI<UsuarioDTO> usuarioDTO = JsonConvert.DeserializeObject<RespuestaAPI<UsuarioDTO>>(jsonFromPostResponse);

            // Assert
            postResponse.EnsureSuccessStatusCode();
            Assert.Equal("289f65db-9ff8-467d-a50c-785aacd680ef", usuarioDTO.Data.UsuarioID);
        }

        [Fact]
        public async Task LoginUsuarioReturnSesionErrorUsuarioIncorrecto()
        {
            var postRequest = new
            {
                Url = "/api/Login",
                Body = new
                {
                    email = "javier@ejemplo.com",
                    password = "12345698231923"
                }
            };

            // Act
            var postResponse = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            var respuestaError = JsonConvert.DeserializeObject<string>(jsonFromPostResponse);

            // Assert
            Assert.Equal("Usuario/Contraseña incorrecta", respuestaError);
        }

        [Fact]
        public async Task LoginUsuarioReturnSesionErrorParams()
        {
            var postRequest = new
            {
                Url = "/api/Login",
                Body = new
                {
                    email = DateTime.Now
                }
            };
            // Act
            var postResponse = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            //Assert
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
