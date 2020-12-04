import { Component, OnInit, OnDestroy, ViewChild, AfterViewInit } from '@angular/core';
import { Partida } from '../../models/partida';
import { Subscription } from 'rxjs';
import { AppState } from '../../store/app.reducer';
import { Store } from '@ngrx/store';
import { Pregunta } from '../../models/pregunta';
import { Respuesta } from '../../models/respuesta';
import { responderPreguntaEnPartida } from 'src/app/store/actions';
import { PartidaState } from '../../store/reducers/partida.reducer';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { siguientePreguntaPartida } from '../../store/actions/partida.actions';

@Component({
    selector: 'app-juego',
    templateUrl: './juego.component.html',
    styleUrls: ['./juego.component.scss']
})
export class JuegoComponent implements OnInit, OnDestroy, AfterViewInit {

    subscriptionPartidaStore: Subscription;
    partida: Partida;
    preguntaActual: Pregunta;
    partidaState: PartidaState;
    @ViewChild('ulListGroup') ulListGroup;
    constructor(
        private store: Store<AppState>,
        private router: Router
    ) { }

    ngOnInit(): void {
    }

    ngOnDestroy(): void {
        this.subscriptionPartidaStore.unsubscribe();
    }

    ngAfterViewInit(): void {
        this.subscriptionPartidaStore = this.store.select('partida').subscribe( (partidaState) => {
                this.partidaState = partidaState;
                if (partidaState.error) {
                    Swal.fire('Mensaje', 'No se encontraron partidas en progreso...' , 'error');
                    this.router.navigate(['']);
                }
                if (partidaState.partidaTerminada) {
                    // this.store.dispatch( terminarPartida() );
                    this.router.navigate(['/resultado']);
                    return;
                }
                if (partidaState.loaded) {
                    this.partida = partidaState.partida;
                    this.preguntaActual = partidaState.preguntaActual;

                    if (this.preguntaActual.contestadaCorrectamente) {
                        this.animarRespuesta(true);
                    } else if (this.preguntaActual.contestada && this.preguntaActual.contestadaCorrectamente === false) {
                        this.animarRespuesta(false);
                    }
                }
            }
        );
    }

    seleccionarRespuesta(respuesta: Respuesta): void {
        if (this.preguntaActual.contestada) { return; }
        this.store.dispatch( responderPreguntaEnPartida({
            partidaPreguntaID: this.preguntaActual.partidaPreguntaID,
            respuestaID: respuesta.respuestaID
        }));
    }

    mostrarAyuda() {
        Swal.fire({
            title: 'Ayuda en la pregunta',
            html: this.preguntaActual.explicacionRespuesta,
            showClass: {
                popup: 'animate__animated animate__faster animate__zoomInLeft'
            },
            hideClass: {
                popup: 'animate__animated animate__faster animate__zoomOutRight'
            }
        });
    }

    siguientePregunta() {
        this.store.dispatch( siguientePreguntaPartida() );
        this.animarSiguientePregunta();
        this.playCardSound();
    }

    /**
     * @param respuestaCorrecta Recibe si la respuesta es correcta para decidir animacion
     */
    animarRespuesta(respuestaCorrecta: boolean): void {
        this.removeClassesAnimate();
        let animacionName = '';
        respuestaCorrecta ? animacionName = 'animate__shakeY' : animacionName = 'animate__shakeX';
        respuestaCorrecta ? this.playSuccesSound() : this.playErrorSound();
        this.ulListGroup.nativeElement.classList.add('animate__animated');
        this.ulListGroup.nativeElement.classList.add(animacionName);
    }

    animarSiguientePregunta() {
        this.removeClassesAnimate();
        this.ulListGroup.nativeElement.classList.add('animate__animated');
        this.ulListGroup.nativeElement.classList.add('animate__rollIn');
    }

    removeClassesAnimate() {
        this.ulListGroup.nativeElement.classList.remove('animate__animated');
        this.ulListGroup.nativeElement.classList.remove('animate__heartBeat');
        this.ulListGroup.nativeElement.classList.remove('animate__shakeY');
        this.ulListGroup.nativeElement.classList.remove('animate__shakeX');
        this.ulListGroup.nativeElement.classList.remove('animate__rollIn');
    }
    playSuccesSound() {
        try {
            const audio = new Audio('/assets/sounds/success.ogg');
            audio.play();
            return true;
        } catch (error) {
            console.log(error);
            return false;
        }
    }

    playErrorSound() {
        try {
            const audio = new Audio('/assets/sounds/error.mp3');
            audio.play();
            return true;
        } catch (error) {
            console.log(error);
            return false;
        }
    }

    playCardSound() {
        try {
            const audio = new Audio('/assets/sounds/card.wav');
            audio.play();
            return true;
        } catch (error) {
            console.log(error);
            return false;
        }
    }

}
