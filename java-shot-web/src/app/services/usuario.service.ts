import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { RespuestaAPI } from '../interfaces/interfaces';
import { Usuario } from '../models/usuario';
import { Store } from '@ngrx/store';
import { AppState } from '../store/app.reducer';

const URL = `${environment.apiURL}/login`;
const headers = new HttpHeaders({
  'Content-Type': 'application/json'
});
@Injectable({
  providedIn: 'root'
})
export class UsuarioService {


  constructor(
    private http: HttpClient,
    private store: Store<AppState>
  ) { }

  login(email: string, password: string) {
    const parametros = {
      email, password
    };
    return this.http.post(`${URL}`, parametros).pipe(
      map( (resp: RespuestaAPI<Usuario>) => resp.data)
    );
  }

  register(usuario: Usuario) {
    return this.http.post(`${environment.apiURL}/usuarios/insertar`, usuario, { headers } ).pipe(
      map( (resp: RespuestaAPI<Usuario>) => {
        return resp.data;
      })
    );
  }

  getPuntos(usuarioID: string) {
    return this.http.get(`${environment.apiURL}/HistorialPuntos/get-puntos/${usuarioID}`);
  }

  getUserLoggedIn(): Usuario {
    if (sessionStorage.getItem('currentUser') == null ||
      sessionStorage.getItem('currentUser') === undefined) {
      return null;
    }
    return JSON.parse(sessionStorage.getItem('currentUser'));
  }
}
