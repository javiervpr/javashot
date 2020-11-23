import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Partida } from 'src/app/interfaces/interfaces';
import * as actions from './partida.actions';

@Component({
  selector: 'app-juego',
  templateUrl: './juego.component.html',
  styleUrls: ['./juego.component.scss']
})
export class JuegoComponent implements OnInit {

  partida: Partida;

  segundoEstado: Partida = {
    partidaID: 1,
    usuarioID: 1,
    preguntas : [
        {
            tituloPregunta: '1 ¿Cuanto es 1 + 1?',
            contestadaCorrectamente: true,
            respuestas: [
                {
                    contenidoRespuesta: 'La respuesta opcion 1',
                    correcta: true
                },
                {
                    contenidoRespuesta: 'La respuesta opcion 2',
                    correcta: false
                }
            ]
        },
        {
            tituloPregunta: 'PREGUNTA 2 ¿?',
            contestadaCorrectamente: false,
            respuestas: [
                {
                    contenidoRespuesta: 'La respuesta opcion 1',
                    correcta: false
                },
                {
                    contenidoRespuesta: 'La respuesta opcion 2',
                    correcta: true
                }
            ]
        }
    ]
};
tercerEstado: Partida = {
  partidaID: 1,
  usuarioID: 1,
  preguntas : [
      {
          tituloPregunta: '1 ¿Cuanto es 1 + 1?',
          contestadaCorrectamente: true,
          respuestas: [
              {
                  contenidoRespuesta: 'La respuesta opcion 1',
                  correcta: true
              },
              {
                  contenidoRespuesta: 'La respuesta opcion 2',
                  correcta: false
              }
          ]
      },
      {
          tituloPregunta: 'PREGUNTA 2 ¿?',
          contestadaCorrectamente: true,
          respuestas: [
              {
                  contenidoRespuesta: 'La respuesta opcion 1',
                  correcta: false
              },
              {
                  contenidoRespuesta: 'La respuesta opcion 2',
                  correcta: true
              }
          ]
      }
  ]
};

  constructor(
    private store: Store<Partida>
  ) { }

  ngOnInit(): void {
    this.store
    .subscribe( contador => this.partida = this.partida );
    console.log(this.partida);
  }

  cambiarEstado() {
    this.store.dispatch( actions.responderPregunta({partida: this.segundoEstado}) );
  }

  cambiarEstadoPorSegundaVez() {
    this.store.dispatch( actions.responderPregunta({partida: this.tercerEstado}) );
  }
}
