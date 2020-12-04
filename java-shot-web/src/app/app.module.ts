import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

// NGRX
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { appReducers } from './store/app.reducer';

import { HttpClientModule } from '@angular/common/http';
import { environment } from '../environments/environment';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { JuegoComponent } from './pages/juego/juego.component';
import { LoginComponent } from './pages/login/login.component';
import { RegistroComponent } from './pages/registro/registro.component';
import { HomeComponent } from './pages/home/home.component';
import { PuntosComponent } from './pages/puntos/puntos.component';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { FormLoginComponent } from './pages/login/form-login/form-login.component';
import { FormRegistroComponent } from './pages/login/form-registro/form-registro.component';
import { EffectsArray } from './store/effects/index';

import { ReactiveFormsModule } from '@angular/forms';
import { ResultadoPartidaComponent } from './pages/resultado-partida/resultado-partida.component';


@NgModule({
  declarations: [
    AppComponent,
    JuegoComponent,
    LoginComponent,
    RegistroComponent,
    HomeComponent,
    PuntosComponent,
    NavbarComponent,
    FormLoginComponent,
    FormRegistroComponent,
    ResultadoPartidaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    StoreModule.forRoot( appReducers ),
    EffectsModule.forRoot( EffectsArray ),
    StoreDevtoolsModule.instrument({
      maxAge: 25, // Retains last 25 states
      logOnly: environment.production, // Restrict extension to log-only mode
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
