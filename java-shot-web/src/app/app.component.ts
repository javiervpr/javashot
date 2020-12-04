import { Component, OnInit } from '@angular/core';
import { UsuarioService } from './services/usuario.service';
import { Store } from '@ngrx/store';
import { AppState } from './store/app.reducer';
import { cargarUsuarioSessionStorage } from './store/actions/usuario.actions';
import { cargarPartidaEnProgreso } from './store/actions/partida.actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(
    private usuarioService: UsuarioService,
    private store: Store<AppState>
  ) {}

  ngOnInit(): void {
    try {
      const user = this.usuarioService.getUserLoggedIn();
      if (user) {
        this.store.dispatch( cargarUsuarioSessionStorage({usuario: user}));
        this.store.dispatch( cargarPartidaEnProgreso({usuarioID: user.usuarioID}) );
      }
    } catch (error) {
      console.log(error);
    }
  }
}
