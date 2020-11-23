import { createReducer, on } from '@ngrx/store';
import { Partida } from 'src/app/interfaces/interfaces';
import { responderPregunta } from './partida.actions';


export const initialState: Partida = {
    partidaID: 1,
    usuarioID: 1,
    preguntas : [
        {
            tituloPregunta: '1 ¿Cuanto es 1 + 1?',
            contestadaCorrectamente: false,
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

const _responderPreguntaReducer = createReducer(initialState,
    on( responderPregunta, (state, props) => props.partida)
);

export function responderPreguntaReducer(state, action) {
    return _responderPreguntaReducer(state, action);
}
