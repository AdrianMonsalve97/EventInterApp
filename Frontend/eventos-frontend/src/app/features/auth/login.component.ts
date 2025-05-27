import {AfterViewInit, Component, signal} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { AuthService, LoginRequest } from '../../core/services/auth.service';
import { Router } from '@angular/router';
import {AnimacionHelper} from '../../shared/utils/animacion.helper';



@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    InputTextModule,
    PasswordModule,
    ButtonModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements AfterViewInit  {

  loading = signal(false);
  loginForm;


  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      nombreUsuario: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngAfterViewInit(): void {
    AnimacionHelper.animarCuadrados();
    const layers = document.querySelectorAll('.layer');
    layers.forEach((layer: Element) => {
      const speed = Number(layer.getAttribute('data-speed')) || 0.5;
      });
  }
  cuadros = Array.from({ length: 45 }, (_, i) => {
    const colores = ['#D9EAFD', '#0971d5', '#474f95', '#6f5097', '#C7D2FE', '#474F95FF'];
    const opacidades = [0.1, 0.2, 0.3, 0.4, 0.5, 0.6];

    return {
      clase: i % 3 === 0 ? 'rounded-xl' : i % 3 === 1 ? 'rounded-md' : 'rounded-sm',
      color: colores[i % colores.length],
      opacidad: opacidades[i % opacidades.length],
    };
  });

  /**
   * Maneja el proceso de inicio de sesión del usuario.
   * Verifica la validez del formulario, realiza la solicitud de inicio de sesión
   * y redirige al usuario según el resultado de la autenticación.
   */
  ingresar(): void {
    // Verifica si el formulario es inválido y detiene la ejecución si es el caso.
    if (this.loginForm.invalid) return;

    // Activa el indicador de carga.
    this.loading.set(true);

    // Obtiene los datos del formulario y los castea al tipo LoginRequest.
    const data: LoginRequest = this.loginForm.value as LoginRequest;

    // Llama al servicio de autenticación para iniciar sesión.
    this.authService.login(data).subscribe({
      /**
       * Maneja la respuesta exitosa del inicio de sesión.
       * @param res - Respuesta del servidor.
       */
      next: (res) => {
        const resultado = res.data;

        // Guarda la sesión del usuario.
        this.authService.guardarSesion(resultado);

        // Redirige al usuario según si debe cambiar su contraseña o no.
        if (resultado.debeCambiarPassword) {
          this.router.navigate(['/cambiar-password']);
        } else {
          this.router.navigate(['/crear-evento']);
        }
      },
      /**
       * Maneja los errores ocurridos durante el inicio de sesión.
       * @param err - Error devuelto por el servidor.
       */
      error: (err) => {
        console.error('Login fallido', err);

        // Desactiva el indicador de carga.
        this.loading.set(false);
      }
    });
  }

}
