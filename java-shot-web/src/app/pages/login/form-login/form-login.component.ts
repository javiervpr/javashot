import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Store } from '@ngrx/store';
import { AppState } from '../../../store/app.reducer';
import { Usuario } from '../../../models/usuario';
import { loginUsuario } from '../../../store/actions/usuario.actions';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-form-login',
  templateUrl: './form-login.component.html',
  styleUrls: ['./form-login.component.scss']
})
export class FormLoginComponent implements OnInit, OnDestroy {

  loginForm: FormGroup;
  loading = false;
  subscriptionStore: Subscription;
  usuario: Usuario;

  constructor(
    private fb: FormBuilder,
    private store: Store<AppState>,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.subscribirseStore();
    this.subscribirseForm();
  }

  ngOnDestroy(): void {
    this.subscriptionStore.unsubscribe();
  }

  guardar() {
    if ( this.loginForm.invalid ) { return; }
    const { email, password } = this.loginForm.value;
    console.log(email, password);
    this.store.dispatch( loginUsuario({ email, password }) );
  }

  subscribirseForm() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email] ],
      password: ['', Validators.required ],
    });
  }

  subscribirseStore() {
    this.subscriptionStore = this.store.select('usuario').subscribe( ({ user, loading, error }) => {
      this.loading = loading;
      if (error) {
        Swal.fire('Error', error.error , 'error');
        console.log(error);
        return;
      }
      if (user) {
        this.usuario = user;
        sessionStorage.setItem('currentUser', JSON.stringify(this.usuario));
        this.router.navigate(['home']);
      }
    });
  }
}
