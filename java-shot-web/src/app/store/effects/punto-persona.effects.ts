import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { mergeMap, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import * as puntoActions from '../actions';
import { PuntosService } from '../../services/puntos.service';



@Injectable()
export class PuntosPersonaEffects {

    constructor(
        private actions$: Actions,
        private puntosService: PuntosService
    ) {}


    cargarPuntoPersona$ = createEffect(
        () => this.actions$.pipe(
            ofType( puntoActions.cargarPuntosPersona ),
            mergeMap(
                ( action ) => this.puntosService.getPuntos( action.usuarioID)
                    .pipe(
                        map( (puntoPersona: any) => puntoActions.cargarPuntosPersonaSuccess({ puntoPersona }) ),
                        catchError( err => of(puntoActions.cargarPuntosPersonaError({ payload: err })) )
                    )
            )
        )
    );
}

