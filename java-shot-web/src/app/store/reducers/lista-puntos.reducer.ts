import { createReducer, on } from '@ngrx/store';
import * as actions from '../actions/lista-puntos.actions';
import { PuntoPersona } from '../../models/punto-persona';


export interface ListaPuntosState {
    listaPuntos: PuntoPersona[];
    loaded: boolean;
    loading: boolean;
    error: any;
}

export const ListaPuntosInitialState: ListaPuntosState = {
    listaPuntos   : null,
    loaded : false,
    loading: false,
    error  : null
};

// tslint:disable-next-line: variable-name
const _listaPuntosReducer = createReducer( ListaPuntosInitialState,

    on( actions.cargarListaPuntos, (state) => ({
        ...state,
        loading: true,
    })),


    on( actions.cargarListaPuntosSuccess, (state, { listaPuntos }) => ({
        ...state,
        loading: false,
        loaded: true,
        listaPuntos: [...listaPuntos]
    })),

    on( actions.cargarListaPuntosError, (state, { payload }) => ({
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

export function ListaPuntosReducer(state, action) {
    return _listaPuntosReducer(state, action);
}
