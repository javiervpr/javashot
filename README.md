# JavaShot
Es un juego desarrollado con el paradigma de programación funcional aplicando técnicas de ludificación para aprender conceptos básicos de programación en Java. 
Se utilizo Angular con la libreria Ngrx que esta basada en observables que trabajan con Rxjs. Al utilizar estas tecnologias nos permiten manejar los estados de la aplicación sin mutar nuestras variables (como es requisito en la programación funcional).
La capa de acceso a datos esta construida con programación orientada a objetos con ASP.Net Core.
La aplicación también cuenta con test de integración para todos los endpoints de los controladores creados con la librería para pruebas Xunit. Por el lado del frontend se esta usando selenium en un proyecto con el lenguaje de programación Python para realizar las pruebas automatizadas EndToEnd.
## Tecnologías
- Angular con Ngrx para manejar los estados de la aplicación
- ASP.Net Core
- Selenium
- Python
## Mecánica de puntos y dinámicas de estatus y competencia
El juego consistirá en duelos en temas básicos de programación en los cuales un jugador podrá ingresar a una partidas de preguntas.
En la partida existirán un banco de preguntas sobre aspectos básicos de programación en java con la respuesta correcta invisible hasta que el usuario responda.
El número de preguntas será establecido por el usuario en grupos de 5, 10 y 15  preguntas.
Una vez comienza el duelo se les mostraran preguntas al usuario, las preguntas se irán mostrando de una en una con opciones de selección múltiple.
Se deberá seleccionar la respuesta que consideran correcta y si acierta se mostrará como respuesta correcta y se sumará 1 punto al jugador. En caso de que el jugador se equivoque se mostrará la respuesta correcta y no se sumará ningún punto.
Cuando se haya completado el número de respuestas el usuario podrá ver el podio con los puntos acumulados en todas las partidas. En el podio se encontrarán sus amigos y un podio global.
También el usuario podrá ver un resumen de la partida al terminar esta, con las preguntas que fueron contestadas correctamente y las que no.
El jugador podrá obtener una ayuda explicando el tema de la pregunta durante la partida.
La dinámica establecida en el juego es la competición y estatus ya que los jugadores podrán competir entre ellos por ser el que sume más puntos con el objetivo de aumentar y consolidar sus conocimientos de programación.
