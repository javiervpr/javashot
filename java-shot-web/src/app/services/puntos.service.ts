import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { RespuestaAPI } from '../interfaces/interfaces';
import { PuntoPersona } from '../models/punto-persona';

const URL = `${environment.apiURL}/HistorialPuntos`;
const headers = new HttpHeaders({
  'Content-Type': 'application/json'
});

@Injectable({
  providedIn: 'root'
})
export class PuntosService {

  constructor(
    private http: HttpClient
  ) { }

  getPuntos(usuarioID: string) {
    return this.http.get<RespuestaAPI<PuntoPersona>>(`${URL}/get-puntos/${usuarioID}`)
      .pipe( map( result => result.data));
  }

  getPuntosAll() {
    return this.http.get<RespuestaAPI<PuntoPersona[]>>(`${URL}/get-puntos`)
    .pipe( map( result => result.data) );
  }
}
