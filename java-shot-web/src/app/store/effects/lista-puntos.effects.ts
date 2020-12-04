import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { mergeMap, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import * as puntoActions from '../actions';
import { PuntosService } from '../../services/puntos.service';



@Injectable()
export class ListaPuntosEffects {

    constructor(
        private actions$: Actions,
        private puntosService: PuntosService
    ) {}


    cargarListaPuntos$ = createEffect(
        () => this.actions$.pipe(
            ofType( puntoActions.cargarListaPuntos ),
            mergeMap(
                () => this.puntosService.getPuntosAll()
                    .pipe(
                        map( (listaPuntos: any) => puntoActions.cargarListaPuntosSuccess({ listaPuntos }) ),
                        catchError( err => of(puntoActions.cargarListaPuntosError({ payload: err })) )
                    )
            )
        )
    );
}

