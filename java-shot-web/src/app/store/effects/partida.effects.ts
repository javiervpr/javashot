import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { tap, mergeMap, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import * as partidasActions from '../actions';
import { PartidaService } from '../../services/partida.service';



@Injectable()
export class PartidaEffects {

    constructor(
        private actions$: Actions,
        private partidaService: PartidaService
    ) {}


    crearPartida$ = createEffect(
        () => this.actions$.pipe(
            ofType( partidasActions.crearPartida ),
            mergeMap(
                ( action ) => this.partidaService.crearPartida( action.usuarioID, action.cantidadPreguntas)
                    .pipe(
                        map( (partida: any) => partidasActions.crearPartidaSuccess({ partida }) ),
                        catchError( err => of(partidasActions.crearPartidaError({ payload: err })) )
                    )
            )
        )
    );

    cargarPartidaEnProgreso$ = createEffect(
        () => this.actions$.pipe(
            ofType( partidasActions.cargarPartidaEnProgreso ),
            mergeMap(
                ( action ) => this.partidaService.cargarPartidaEnProgreso( action.usuarioID)
                    .pipe(
                        map( (partida: any) => partidasActions.cargarPartidaEnProgresoSuccess({ partida }) ),
                        catchError( err => of(partidasActions.cargarPartidaEnProgresoError({ payload: err })) )
                    )
            )
        )
    );

    responderPreguntaEnPartida$ = createEffect(
        () => this.actions$.pipe(
            ofType( partidasActions.responderPreguntaEnPartida ),
            mergeMap(
                ( action ) => this.partidaService.responderPregunta( action.partidaPreguntaID, action.respuestaID)
                    .pipe(
                        map( (respuesta: any) => partidasActions.responderPreguntaEnSuccess({ respuestaCorrecta: respuesta }) ),
                        catchError( err => of(partidasActions.responderPreguntaEnError({ payload: err })) )
                    )
            )
        )
    );
}
