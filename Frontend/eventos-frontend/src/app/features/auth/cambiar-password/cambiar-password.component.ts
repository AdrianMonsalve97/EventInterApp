import { Component } from '@angular/core';

@Component({
  selector: 'app-cambiar-password',
  imports: [],
  templateUrl: './cambiar-password.component.html',
  styleUrl: './cambiar-password.component.css'
})
export class CambiarPasswordComponent {
  cambiarPassword = 'hola';
  constructor() {
    // Aquí puedes inicializar cualquier lógica necesaria
  }

  onSubmit() {
    // Lógica para cambiar la contraseña
    console.log('Contraseña cambiada:', this.cambiarPassword);
    // Aquí podrías llamar a un servicio para actualizar la contraseña en el backend
  }

  cancelar() {
    // Lógica para cancelar el cambio de contraseña
    console.log('Cambio de contraseña cancelado');
    // Aquí podrías redirigir al usuario a otra página o limpiar el formulario
  }
}
