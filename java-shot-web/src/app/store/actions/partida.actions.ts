import { createAction, props } from '@ngrx/store';
import { Partida } from 'src/app/models/partida';

// CREAR PARTIDA
export const crearPartida = createAction(
    '[Partida] Crear Partida',
    props<{ usuarioID: string, cantidadPreguntas: number }>()
);

export const crearPartidaSuccess = createAction(
    '[Partida] Crear Partida Success',
    props<{ partida: Partida }>()
);

export const crearPartidaError = createAction(
    '[Partida] Crear Partida Error',
    props<{ payload: any }>()
);

// CARGAR PARTIDA EN PROGRESO
export const cargarPartidaEnProgreso = createAction(
    '[Partida] Cargar Partida en Progreso',
    props<{ usuarioID: string }>()
);

export const cargarPartidaEnProgresoSuccess = createAction(
    '[Partida] Cargar Partida en Progreso Success',
    props<{ partida: Partida }>()
);

export const cargarPartidaEnProgresoError = createAction(
    '[Partida] Cargar Partida en Progreso Error',
    props<{ payload: any }>()
);
// RESPONDER PREGUNTA
export const responderPreguntaEnPartida = createAction(
    '[Partida] Responder Pregunta -> Partida en Progreso',
    props<{ partidaPreguntaID: string, respuestaID: string }>()
);

export const responderPreguntaEnSuccess = createAction(
    '[Partida] Responder Pregunta en Progreso Success',
    props<{ respuestaCorrecta: boolean }>()
);

export const responderPreguntaEnError = createAction(
    '[Partida] Responder Pregunta  en Progreso Error',
    props<{ payload: any }>()
);
// CAMBIAR A SIGUIENTE PREGUNTA
export const siguientePreguntaPartida = createAction(
    '[Partida] Siguiente Pregunta'
);
// ACABAR PARTIDA
// export const terminarPartida = createAction(
//     '[Partida] Terminar Partida'
// );
