import { Component, inject, signal } from '@angular/core';
import { UsuarioService } from '../../../core/services/usuario.service';
import { Usuario } from '../../../core/models/usuario.model';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-listar-usuario',
  standalone: true,
  imports: [CommonModule, TableModule, ButtonModule, RouterModule],
  templateUrl: './listar-usuario.component.html',
  styleUrls: ['./listar-usuario.component.css']
})
export class ListarUsuarioComponent {
  private usuarioService = inject(UsuarioService);
  private router = inject(Router);

  usuarios = signal<Usuario[]>([]);

  constructor() {
    this.usuarioService.listarUsuarios().subscribe({
      next: (usuarios) => {
        this.usuarios.set(usuarios); // ✅ sin `.data` porque ya viene el array directamente
      },
      error: (err) => {
        console.error('Error cargando usuarios', err);
      }
    });
  }


  crearNuevoUsuario() {
    this.router.navigate(['/usuarios/crear']);
  }

  editarUsuario(usuario: Usuario) {
    console.log('Editar usuario', usuario);
    // Redirigir o abrir modal
  }
}
