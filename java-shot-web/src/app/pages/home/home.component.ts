import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.reducer';
import { Usuario } from '../../models/usuario';
import { Subscription } from 'rxjs';
import { crearPartida } from '../../store/actions/partida.actions';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {

  usuarioEnSesion: Usuario;
  subscriptionStore: Subscription;
  subscriptionPartida: Subscription;
  constructor(
    private store: Store<AppState>,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.subscriptionStore = this.store.select('usuario').subscribe( ({user}) => {
      this.usuarioEnSesion = user;
      }
    );

    this.subscriptionPartida = this.store.select('partida').subscribe( (partidaState) => {
      if (partidaState.partidaTerminada != null && partidaState.partidaTerminada === false) {
        this.router.navigate(['/partida']);
      }
      }
    );
  }

  ngOnDestroy(): void {
    this.subscriptionStore.unsubscribe();
    this.subscriptionPartida.unsubscribe();
  }

  async mostrarAlert() {
    const opciones = {
        cinco: 5,
        diez: 10,
        quince: 15
    };
    const { value: cantidadElegida } = await Swal.fire({
      title: 'Selecciona la cantidad de preguntas para la partida',
      input: 'select',
      showClass: {
        popup: 'animate__animated animate__faster animate__zoomInLeft'
    },
    hideClass: {
        popup: 'animate__animated animate__faster animate__zoomOutRight'
    },
      inputOptions: opciones,
      inputPlaceholder: 'Seleccionar',
      inputValidator: (value) => {
        return new Promise((resolve) => {
          if (value.length === 0) {
            resolve('Selecciona la cantidad');
          } else {
            resolve();
          }
        });
      }
    });

    if (cantidadElegida) {
      this.jugar(opciones[cantidadElegida]);
    }
  }

  jugar(cantidadElegida: number) {
    if (!this.usuarioEnSesion) {
      this.router.navigate(['login']);
      return;
    }
    this.store.dispatch( crearPartida({usuarioID: this.usuarioEnSesion.usuarioID, cantidadPreguntas: cantidadElegida}));
  }
}
