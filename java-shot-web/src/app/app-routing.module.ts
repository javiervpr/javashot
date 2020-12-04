import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { JuegoComponent } from './pages/juego/juego.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { PuntosComponent } from './pages/puntos/puntos.component';
import { RegistroComponent } from './pages/registro/registro.component';
import { AuthGuard } from './guards/auth.guard';
import { ResultadoPartidaComponent } from './pages/resultado-partida/resultado-partida.component';


const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'home', component: HomeComponent},
  {path: 'partida', component: JuegoComponent, canActivate: [ AuthGuard ]},
  {path: 'puntos', component: PuntosComponent, canActivate: [ AuthGuard ]},
  {path: 'resultado', component: ResultadoPartidaComponent, canActivate: [ AuthGuard ]},
  {path: 'login', component: LoginComponent},
  {path: 'registrarse', component: RegistroComponent},
  {path: '**', component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
