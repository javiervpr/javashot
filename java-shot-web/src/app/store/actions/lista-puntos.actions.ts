import { createAction, props } from '@ngrx/store';
import { PuntoPersona } from '../../models/punto-persona';

export const cargarListaPuntos = createAction(
    '[ListaPuntos] Cargar Lista Puntos Personas'
);

export const cargarListaPuntosSuccess = createAction(
    '[ListaPuntos] Cargar Lista Puntos Personas Success',
    props<{ listaPuntos: PuntoPersona[] }>()
);

export const cargarListaPuntosError = createAction(
    '[ListaPuntos] Cargar Lista Puntos Personas Error',
    props<{ payload: any }>()
);

