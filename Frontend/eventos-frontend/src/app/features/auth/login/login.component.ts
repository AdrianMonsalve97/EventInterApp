import { AfterViewInit, Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { Toast } from 'primeng/toast';
import { MessageService } from 'primeng/api'; // ✅ Import del servicio
import { AuthService, LoginRequest } from '../../../core/services/auth.service';
import { AnimacionHelper } from '../../../shared/utils/animacion.helper';

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
    ButtonModule,
    Toast
  ],
  providers: [MessageService], // ✅ Registrar MessageService
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements AfterViewInit {

  loading = signal(false);
  loginForm;

  cuadros = Array.from({ length: 30 }, (_, i) => {
    const colores = ['#D9EAFD', '#0971d5', '#474f95', '#6f5097', '#C7D2FE', '#474F95FF'];
    const opacidades = [0.1, 0.2, 0.3, 0.4, 0.5, 0.6];
    return {
      clase: i % 3 === 0 ? 'rounded-xl' : i % 3 === 1 ? 'rounded-md' : 'rounded-sm',
      color: colores[i % colores.length],
      opacidad: opacidades[i % opacidades.length],
    };
  });

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private messageService: MessageService // ✅ Inyectar el servicio
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

  ingresar(): void {
    if (this.loginForm.invalid) return;
    this.loading.set(true);
    const data: LoginRequest = this.loginForm.value as LoginRequest;

    this.authService.login(data).subscribe({
      next: (res) => {
        const resultado = res.data;
        this.authService.guardarSesion(resultado);
        if (resultado.debeCambiarPassword) {
          this.router.navigate(['/cambiar-password']);
        } else {
          this.router.navigate(['/eventos-disponible']);
        }
      },
      error: (err) => {
        this.loading.set(false);
        this.messageService.add({
          severity: 'error',
          summary: 'Error al iniciar sesión',
          detail: err?.error?.mensaje ?? 'Credenciales inválidas',
        });
      }
    });
  }

}
