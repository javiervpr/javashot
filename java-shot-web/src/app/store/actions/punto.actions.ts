import { createAction, props } from '@ngrx/store';
import { PuntoPersona } from '../../models/punto-persona';

export const cargarPuntosPersona = createAction(
    '[PuntosPersona] Cargar Puntos Personas',
    props<{ usuarioID: string }>()
);

export const cargarPuntosPersonaSuccess = createAction(
    '[PuntosPersona] Cargar Puntos Personas Success',
    props<{ puntoPersona: PuntoPersona }>()
);

export const cargarPuntosPersonaError = createAction(
    '[PuntosPersona] Cargar Puntos Personas Error',
    props<{ payload: any }>()
);

