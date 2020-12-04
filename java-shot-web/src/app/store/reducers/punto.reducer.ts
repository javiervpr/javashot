import { createReducer, on } from '@ngrx/store';
import * as actions from '../actions/punto.actions';
import { PuntoPersona } from '../../models/punto-persona';


export interface PuntosPersonaState {
    usuarioID: string;
    puntoPersona: PuntoPersona;
    loaded: boolean;
    loading: boolean;
    error: any;
}

export const PuntosPersonaInitialState: PuntosPersonaState = {
    usuarioID: '',
    puntoPersona   : null,
    loaded : false,
    loading: false,
    error  : null
};

// tslint:disable-next-line: variable-name
const _puntosPersonaReducer = createReducer( PuntosPersonaInitialState,

    on( actions.cargarPuntosPersona, (state, { usuarioID }) => ({
        ...state,
        loading: true,
        usuarioID
    })),


    on( actions.cargarPuntosPersonaSuccess, (state, { puntoPersona }) => ({
        ...state,
        loading: false,
        loaded: true,
        puntoPersona: { ...puntoPersona }
    })),

    on( actions.cargarPuntosPersonaError, (state, { payload }) => ({
        ...state,
        loading: false,
        loaded: false,
        error: {
            url: payload.url,
            name: payload.name,
            message: payload.message
        }
    })),




);

export function PuntosPersonaReducer(state, action) {
    return _puntosPersonaReducer(state, action);
}
