import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Usuario } from '../../../models/usuario';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from '../../../store/app.reducer';
import Swal from 'sweetalert2';
import { registerUsuario } from '../../../store/actions/usuario.actions';

@Component({
  selector: 'app-form-registro',
  templateUrl: './form-registro.component.html',
  styleUrls: ['./form-registro.component.scss']
})
export class FormRegistroComponent implements OnInit, OnDestroy {

  registerForm: FormGroup;
  loading = false;
  subscriptionStore: Subscription;

  constructor(
    private fb: FormBuilder,
    private store: Store<AppState>,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.subscribirseStore();
    this.subscribirseForm();
  }

  ngOnDestroy(): void {
    this.subscriptionStore.unsubscribe();
  }

  guardar() {
    if ( this.registerForm.invalid ) { return; }
    const { email, password, nombre, apellido } = this.registerForm.value;
    const usuario = new Usuario('', nombre, apellido, '', email, password);

    this.store.dispatch( registerUsuario({ usuario }) );
  }

  subscribirseForm() {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email] ],
      password: ['', Validators.required ],
      nombre: ['', Validators.required],
      apellido: ['', Validators.required]
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
        sessionStorage.setItem('currentUser', JSON.stringify(user));
        this.router.navigate(['home']);
      }
    });
  }

}
