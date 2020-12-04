import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as usuariosActions from '../actions';
import { tap, mergeMap, map, catchError } from 'rxjs/operators';
import { UsuarioService } from '../../services/usuario.service';
import { of } from 'rxjs';



@Injectable()
export class UsuarioEffects {

    constructor(
        private actions$: Actions,
        private usuarioService: UsuarioService
    ) {}


    loginUsuario$ = createEffect(
        () => this.actions$.pipe(
            ofType( usuariosActions.loginUsuario ),
            mergeMap(
                ( action ) => this.usuarioService.login( action.email, action.password )
                    .pipe(
                        map( (user: any) => usuariosActions.loginUsuarioSuccess({ usuario: user }) ),
                        catchError( err => of(usuariosActions.loginUsuarioError({ payload: err })) )
                    )
            )
        )
    );

    registerUsuario$ = createEffect(
        () => this.actions$.pipe(
            ofType( usuariosActions.registerUsuario ),
            tap( action => console.log('tap mergemap', action)),
            mergeMap(
                ( action ) => this.usuarioService.register( action.usuario )
                    .pipe(
                        map( (user: any) => {
                            return usuariosActions.registerUsuarioSuccess({ usuario: user });
                        } ),
                        catchError( err => {
                            return of(usuariosActions.registerUsuarioError({ payload: err }) );
                        } ),
                    )
            )
        )
    );
}
