import { createAction, props  } from '@ngrx/store';
import { Partida } from '../../interfaces/interfaces';


export const responderPregunta = createAction(
    '[Partida] ResponderPregunta',
    props<{partida: Partida}>()
);

