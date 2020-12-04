import { createReducer, on } from '@ngrx/store';
import { Partida } from 'src/app/models/partida';
import * as actions from '../actions/partida.actions';
import { Pregunta } from '../../models/pregunta';


export interface PartidaState {
    partida: Partida;
    loaded: boolean;
    loading: boolean;
    error: any;
    preguntaActual: Pregunta;
    respuestaElegidaID: string;
    partidaTerminada: boolean;
}

export const partidaInitialState: PartidaState = {
    partida: null,
    loaded : false,
    loading: false,
    error  : null,
    preguntaActual: null,
    respuestaElegidaID: null,
    partidaTerminada: null
};

// tslint:disable-next-line: variable-name
const _partidaReducer = createReducer(partidaInitialState,

    on( actions.crearPartida,   state => (
        { ...state,
            loading: true,
            error: null,
            respuestaElegidaID: null,
            partidaTerminada: false,
            loaded: false
        })),

    on( actions.crearPartidaSuccess, (state, { partida }) => ({
        ...state,
        loading: false,
        loaded: true,
        partida: { ...partida },
        error: null,
        preguntaActual: partida.preguntas[0]
    })),

    on( actions.crearPartidaError, (state, { payload }) => ({
        ...state,
        loading: false,
        loaded: false,
        error: {
            url: payload.url,
            name: payload.name,
            message: payload.message,
            status: payload.status,
            error: payload.error
        }
    })),

    on( actions.cargarPartidaEnProgreso,   state => ({ ...state, loading: true, error: null, loaded: false  })),

    on( actions.cargarPartidaEnProgresoSuccess, (state, { partida }) => {
        const preguntaActual: Pregunta = partida.preguntas.filter(p => p.contestada === false)[0];
        return {
        ...state,
        loading: false,
        loaded: true,
        partida: { ...partida },
        error: null,
        preguntaActual: preguntaActual ? preguntaActual : null
        };
    }),

    on( actions.crearPartidaError, (state, { payload }) => ({
        ...state,
        loading: false,
        loaded: false,
        error: {
            url: payload.url,
            name: payload.name,
            message: payload.message,
            status: payload.status,
            error: payload.error
        }
    })),

    on( actions.responderPreguntaEnPartida,   (state, {respuestaID}) => ({
        ...state,
        loading: true,
        error: null,
        loaded: false,
        respuestaElegidaID: respuestaID
    })),

    on( actions.responderPreguntaEnSuccess, (state, { respuestaCorrecta }) => {
        const preguntaActualActualizada = {...state.preguntaActual,
            contestadaCorrectamente: respuestaCorrecta,
            contestada: true
        };
        // tslint:disable-next-line: prefer-const
        let partidaActualizada = { ...state.partida };
        const indexOfPreguntaActual = partidaActualizada.preguntas.indexOf(state.preguntaActual);
        partidaActualizada.preguntas =
        [...partidaActualizada.preguntas.slice(0, indexOfPreguntaActual), preguntaActualActualizada,
            ...partidaActualizada.preguntas.slice(indexOfPreguntaActual + 1)];

        return {
        ...state,
        loading: false,
        loaded: true,
        partida: { ...partidaActualizada },
        error: null,
        preguntaActual: {...preguntaActualActualizada
                        }
        };
    }),

    on( actions.responderPreguntaEnError, (state, { payload }) => ({
        ...state,
        loading: false,
        loaded: false,
        error: {
            url: payload.url,
            name: payload.name,
            message: payload.message,
            status: payload.status,
            error: payload.error
        }
    })),

    on( actions.siguientePreguntaPartida,   (state) => {
        const nuevaPreguntaActual = state.partida.preguntas.filter(p => p.contestada === false)[0];
        return {
        ...state,
        loading: false,
        error: null,
        loaded: true,
        respuestaElegidaID: null,
        preguntaActual: nuevaPreguntaActual ? nuevaPreguntaActual : null,
        partidaTerminada: nuevaPreguntaActual ? false : true
    };
    }),

    // on( actions.terminarPartida,   state => ({ ...partidaInitialState  })),
);

export function partidaReducer(state, action) {
    return _partidaReducer(state, action);
}
