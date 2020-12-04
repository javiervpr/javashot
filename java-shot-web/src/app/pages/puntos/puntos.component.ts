import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.reducer';
import { Subscription } from 'rxjs';
import { Partida } from '../../models/partida';
import { UsuarioService } from '../../services/usuario.service';
import { Usuario } from '../../models/usuario';
import { PuntosService } from '../../services/puntos.service';
import { PuntoPersona } from '../../models/punto-persona';
import { cargarPuntosPersona } from '../../store/actions/punto.actions';
import { cargarListaPuntos } from '../../store/actions/lista-puntos.actions';

@Component({
  selector: 'app-puntos',
  templateUrl: './puntos.component.html',
  styleUrls: ['./puntos.component.scss']
})
export class PuntosComponent implements OnInit, OnDestroy {

  usuarioEnSesion: Usuario;
  subscriptionPuntos: Subscription;
  subscriptionListaPuntos: Subscription;
  partida: Partida;
  listaDePuntosAll: PuntoPersona[];
  puntoPersona: PuntoPersona;
  constructor(
    private store: Store<AppState>,
    private userService: UsuarioService,
  ) { }

  ngOnInit(): void {
    this.usuarioEnSesion = this.userService.getUserLoggedIn();
    this.subscriptionPuntos = this.store.select('puntosPersona').subscribe(({puntoPersona, error}) => {
      if (error) {
        console.log(error);
      }
      this.puntoPersona = puntoPersona;
    });
    this.subscriptionListaPuntos = this.store.select('listaPuntos').subscribe(({loaded, listaPuntos, error}) => {
      if (error) {
        console.log(error);
      }
      if (loaded) {
        this.listaDePuntosAll = listaPuntos;
        console.log(typeof this.listaDePuntosAll, this.listaDePuntosAll);
      }
    });
    this.store.dispatch(cargarPuntosPersona({usuarioID: this.usuarioEnSesion.usuarioID}));
    this.store.dispatch(cargarListaPuntos());
  }

  ngOnDestroy(): void {
    this.subscriptionListaPuntos.unsubscribe();
    this.subscriptionPuntos.unsubscribe();
  }

}
