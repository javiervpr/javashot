import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

const URL = `${environment.apiURL}/Partidas`;
@Injectable({
  providedIn: 'root'
})
export class PartidaService {

  constructor(
    private http: HttpClient
  ) { }

  crearPartida(usuarioID: string, cantidadPreguntas) {
    const parametros = {
      CantidadPreguntas: cantidadPreguntas,
      Usuario: { UsuarioID: usuarioID }
    };
    return this.http.post(`${URL}/crear-partida`, parametros)
      .pipe(
        map( (respuesta: any) => {
            delete respuesta.data.usuario;
            return respuesta.data;
            }
          )
      );
  }

  cargarPartidaEnProgreso(usuarioID: string) {
    return this.http.get(`${URL}/obtener-partida-actual/${usuarioID}`, {})
      .pipe(
        map( (respuesta: any) => {
            delete respuesta.data.usuario;
            return respuesta.data;
            }
          )
      );
  }

  responderPregunta(partidaPreguntaID: string, respuestaID: string) {
    return this.http.post(`${environment.apiURL}/Preguntas/responder-pregunta`, { partidaPreguntaID, respuestaID })
      .pipe(
        map( (respuesta: any) => {
            return respuesta.data;
            }
          )
      );
  }
}
