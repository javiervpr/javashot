import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.reducer';
import { cerrarSesionusuario } from '../../../store/actions/usuario.actions';
import { Router } from '@angular/router';
import { UsuarioService } from '../../../services/usuario.service';
import { Usuario } from '../../../models/usuario';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  usuarioEnSesion: Usuario;
  constructor(
    private store: Store<AppState>,
    private router: Router,
    private usuarioService: UsuarioService
  ) { }

  ngOnInit(): void {
    this.getUsuarioEnSesion();
  }

  getUsuarioEnSesion() {
    this.usuarioEnSesion = this.usuarioService.getUserLoggedIn();
  }

  cerrarSesion() {
    this.store.dispatch(cerrarSesionusuario());
    sessionStorage.removeItem('currentUser');
    this.router.navigate(['login']);
  }
}
