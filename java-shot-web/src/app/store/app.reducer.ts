import { ActionReducerMap } from '@ngrx/store';
import * as reducers from './reducers';


export interface AppState {
    usuario: reducers.UsuarioState;
    partida: reducers.PartidaState;
    puntosPersona: reducers.PuntosPersonaState;
    listaPuntos: reducers.ListaPuntosState;
}



export const appReducers: ActionReducerMap<AppState> = {
    usuario: reducers.UsuarioReducer,
    partida: reducers.partidaReducer,
    puntosPersona: reducers.PuntosPersonaReducer,
    listaPuntos: reducers.ListaPuntosReducer
};
