import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.reducer';
import { PartidaState } from '../../store/reducers/partida.reducer';
import { Partida } from '../../models/partida';
import { Subscription } from 'rxjs';
import Swal from 'sweetalert2';
import { Pregunta } from '../../models/pregunta';

@Component({
  selector: 'app-resultado-partida',
  templateUrl: './resultado-partida.component.html',
  styleUrls: ['./resultado-partida.component.scss']
})
export class ResultadoPartidaComponent implements OnInit, OnDestroy, AfterViewInit {

  partidaActual: Partida;
  subscriptionStorePartida: Subscription;
  puntosSumados = 0;
  constructor(
    private store: Store<AppState>
  ) { }

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.subscriptionStorePartida.unsubscribe();
  }

  ngAfterViewInit(): void {
    this.subscriptionStorePartida = this.store.select('partida').subscribe((partidaState: PartidaState) => {
      if (partidaState.loaded) {
        this.partidaActual = partidaState.partida;
        this.puntosSumados = this.partidaActual.preguntas.filter(p => p.contestadaCorrectamente === true).length;
      }
    });
  }

  mostrarAyuda(pregunta: Pregunta) {
    Swal.fire({
        title: 'Ayuda en la pregunta',
        html: pregunta.explicacionRespuesta,
        showClass: {
            popup: 'animate__animated animate__faster animate__zoomInLeft'
        },
        hideClass: {
            popup: 'animate__animated animate__faster animate__zoomOutRight'
        }
    });
}
}
