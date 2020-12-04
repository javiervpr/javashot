import { createAction, props } from '@ngrx/store';
import { Usuario } from '../../models/usuario';

export const cargarUsuarioSessionStorage = createAction(
    '[Usuario] Cargar Usuario Session Storage',
    props<{ usuario: Usuario }>()
);

export const loginUsuario = createAction(
    '[Usuario] Login Usuario',
    props<{ email: string, password: string }>()
);

export const loginUsuarioSuccess = createAction(
    '[Usuario] Login Usuario Success',
    props<{ usuario: Usuario }>()
);

export const loginUsuarioError = createAction(
    '[Usuario] Login Usuario Error',
    props<{ payload: any }>()
);

export const cerrarSesionusuario = createAction(
    '[Usuario] Cerrar Sesion Usuario'
);

export const registerUsuario = createAction(
    '[Usuario] Registrar Usuario',
    props<{ usuario: Usuario }>()
);

export const registerUsuarioSuccess = createAction(
    '[Usuario] Registrar Usuario Success',
    props<{ usuario: Usuario }>()
);

export const registerUsuarioError = createAction(
    '[Usuario] Registrar Usuario Error',
    props<{ payload: any }>()
);
