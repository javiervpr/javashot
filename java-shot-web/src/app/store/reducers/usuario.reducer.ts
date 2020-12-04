import { createReducer, on } from '@ngrx/store';
import * as actions from '../actions';
import { Usuario } from '../../models/usuario';

export interface UsuarioState {
    user: Usuario;
    loaded: boolean;
    loading: boolean;
    error: any;
}

export const UsuarioInitialState: UsuarioState = {
    user   : null,
    loaded : false,
    loading: false,
    error  : null
};

// tslint:disable-next-line: variable-name
const _UsuarioReducer = createReducer( UsuarioInitialState,

    on( actions.loginUsuario, (state) => ({
        ...state,
        loading: true,
        error: null
    })),


    on( actions.loginUsuarioSuccess, (state, { usuario }) => ({
        ...state,
        loading: false,
        loaded: true,
        user: { ...usuario },
        error: null
    })),

    on( actions.loginUsuarioError, (state, { payload }) => ({
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

    on( actions.cerrarSesionusuario, (state) => ({ ...state, ...UsuarioInitialState })),

    on( actions.registerUsuario, (state) => {
        return {
        ...state,
        loading: true,
        error: null
        };
    }
    ),


    on( actions.registerUsuarioSuccess, (state, { usuario }) => {
        return {
        ...state,
        loading: false,
        loaded: true,
        user: { ...usuario },
        error: null
        };
    }
    ),

    on( actions.registerUsuarioError, (state, { payload }) => {
        return {
        ...state,
        loading: false,
        loaded: false,
        error: {
            payload
            // url: payload.url,
            // name: payload.name,
            // message: payload.message,
            // status: payload.status,
            // error: payload.error
        }
    };
}
    ),

    on( actions.cargarUsuarioSessionStorage , (state, { usuario }) => ({
        ...state,
        loading: false,
        loaded: true,
        user: { ...usuario },
        error: null
    })),

);

export function UsuarioReducer(state, action) {
    return _UsuarioReducer(state, action);
}
