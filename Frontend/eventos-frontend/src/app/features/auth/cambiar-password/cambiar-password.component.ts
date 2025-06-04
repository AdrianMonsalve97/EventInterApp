import { Component, inject, signal } from '@angular/core';
import { FormBuilder, Validators, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ToastModule } from 'primeng/toast';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { CambiarPasswordRequest } from '../../../core/models/password.model';
import { Password } from 'primeng/password';

@Component({
  selector: 'app-cambiar-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, InputTextModule, ButtonModule, ToastModule, Password],
  providers: [MessageService],
  templateUrl: './cambiar-password.component.html'
})
export class CambiarPasswordComponent {
  private fb = inject(FormBuilder);
  private auth = inject(AuthService);
  private toast = inject(MessageService);
  private router = inject(Router);

  cargando = signal(false);

  form = this.fb.group({
    usuarioId: [this.auth.usuarioId() ?? '', Validators.required],
    passwordActual: ['', Validators.required],
    passwordNueva: ['', [
      Validators.required,
      Validators.minLength(8),
      Validators.pattern(/^(?=.*[!@#$%^&*(),.?":{}|<>]).+$/)
    ]],
    confirmar: ['', Validators.required]
  }, { validators: CambiarPasswordComponent.passwordsIguales });

  static passwordsIguales(group: FormGroup) {
    const nueva = group.get('passwordNueva')?.value;
    const confirmar = group.get('confirmar')?.value;
    return nueva === confirmar ? null : { noCoinciden: true };
  }

  cambiarPassword() {
    this.cargando.set(true);
    const payload: CambiarPasswordRequest = {
      idUsuario: localStorage.getItem('usuarioId') ? Number(localStorage.getItem('usuarioId')) : 0,
      passwordActual: this.form.value.passwordActual!,
      passwordNueva: this.form.value.passwordNueva!
    };

    this.auth.cambiarPassword(payload).subscribe({
      next: () => {
        this.toast.add({ severity: 'success', summary: 'Contraseña actualizada' });
        this.router.navigate(['/login']);
      },
      error: () => {
        this.toast.add({ severity: 'error', summary: 'Error', detail: 'No se pudo cambiar la contraseña' });
        this.cargando.set(false);
      }
    });
  }
}
