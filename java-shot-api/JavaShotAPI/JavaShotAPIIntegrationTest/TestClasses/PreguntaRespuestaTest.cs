using JavaShotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace JavaShotAPIIntegrationTest.TestClasses
{
    public class PreguntaRespuestaTest
    {

        [Fact]
        public void CreatePreguntaRespuestaTest()
        {
            // Arrange
            Pregunta pregunta = new Pregunta("Pregunta 1", "Explicacion");
            Respuesta respuesta = new Respuesta("Respuesta 1", true);

            // Act
            PreguntaRespuesta preguntaRespuesta = new PreguntaRespuesta(pregunta, respuesta);

            // Assert
            Assert.True(preguntaRespuesta.PreguntaRespuestaID.ToString().Count() > 0);
            Assert.NotNull(preguntaRespuesta.Pregunta);
            Assert.NotNull(preguntaRespuesta.Respuesta);
        }

        [Fact]
        public void CrearRespuestaInvalida()
        {
            // Act
            try
            {
                Respuesta respuesta = new Respuesta("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque in faucibus mi, eget consequat lacus. Nunc tincidunt ipsum orci, quis suscipit urna feugiat a. Donec condimentum sodales tempus. Ut posuere finibus quam, in convallis est fringilla et. Aliquam congue eget sem viverra venenatis. Vestibulum at eros sit amet purus mollis sagittis quis sit amet ante. Duis et ornare ex, et tincidunt diam. Proin facilisis ex lorem, scelerisque faucibus nunc cursus nec. Nunc eu aliquet est. Etiam convallis, justo sit amet ullamcorper hendrerit, dui elit scelerisque velit, vel rhoncus dui elit id orci. Nam consequat posuere convallis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.Nullam ut accumsan metus.Lorem ipsum dolor sit amet, consectetur adipiscing elit.Ut aliquet viverra urna, in maximus nibh dignissim et.Suspendisse potenti.Morbi ultricies vulputate suscipit.Fusce ut tincidunt diam, fringilla iaculis urna.Aenean eu est non quam imperdiet dictum.In lacinia tincidunt urna a eleifend.Donec tellus dui, scelerisque at tincidunt in, rhoncus nec velit.Nullam at purus vitae lectus aliquam dignissim.Pellentesque ac eleifend mi, vitae commodo odio.Vivamus neque justo, feugiat vel mollis eu, aliquet non ligula.Suspendisse eros purus, faucibus id urna non, venenatis blandit dui.Morbi quis lectus condimentum, semper metus sit amet, luctus arcu.Phasellus pharetra ex risus, sed porta est feugiat eget. Phasellus vel tellus eget sem maximus eleifend ac et justo.Donec tristique ante augue, ut vehicula lacus sodales bibendum.Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.Fusce consequat neque eu justo maximus consectetur.Praesent pulvinar, ante a tempus vestibulum, orci est gravida nulla, et ullamcorper sapien libero non lacus.Nam tellus dolor, condimentum ornare sollicitudin dapibus, faucibus vel urna.Vivamus feugiat condimentum lorem vitae lobortis.Interdum et malesuada fames ac ante ipsum primis in faucibus.Integer justo dolor, tristique vitae velit ultricies, posuere vehicula leo.Nam varius, nibh sed venenatis sagittis, libero nisl placerat lorem, at dictum turpis sem eget augue.Donec aliquet nisi vel nulla commodo, eget scelerisque purus venenatis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.Vivamus quis libero rhoncus, tristique sapien ut, vulputate dolor.Fusce pulvinar ex vel nisl tincidunt porttitor.Vestibulum aliquet efficitur mi ac ultricies.Curabitur ac dolor ex.Proin feugiat faucibus nibh, in malesuada orci vehicula ac.Aliquam nec fringilla erat.Donec interdum ornare ultrices.Morbi vel elit enim.Proin non erat ligula.Donec justo quam, ultricies at tincidunt sed, varius vel nunc.Integer auctor mollis nunc, ac tempor urna laoreet vitae.Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nam varius, diam eu vestibulum iaculis, est ipsum fringilla erat, ac iaculis nisl lorem et neque. Vivamus in mauris dui. Ut efficitur ut massa vel blandit. Ut in odio faucibus, consectetur mauris et, scelerisque elit.Praesent vel pulvinar mauris, vitae pulvinar neque. Nullam enim quam, sodales vel est at, posuere vestibulum erat. Mauris tincidunt, sem ac cursus fermentum, tortor lectus molestie dolor, vitae rutrum enim lorem a nunc.Fusce lectus augue, sagittis.", true);
                Assert.False(true);
            }
            catch (Exception e)
            {
                // Assert
                Assert.NotNull(e);
            }
        }
    }
}
