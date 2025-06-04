import {AfterViewInit, Component} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { SideNavComponent } from '../../shared/components/side-nav/side-nav.component';
import {AnimacionHelper} from '../../shared/utils/animacion.helper';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, SideNavComponent],
  templateUrl: './main-layout.component.html',
})
export class MainLayoutComponent implements AfterViewInit {

  ngAfterViewInit(): void {
    AnimacionHelper.animarCuadrados();
    const layers = document.querySelectorAll('.layer');
    layers.forEach((layer: Element) => {
      const speed = Number(layer.getAttribute('data-speed')) || 3;
    });
  }
  cuadros = Array.from({ length: 25 }, (_, i) => {
    const colores = ['#D9EAFD', '#0971d5', '#474f95', '#6f5097', '#C7D2FE', '#474F95FF'];
    const opacidades = [0.1, 0.2, 0.3, 0.4, 0.5, 0.6];
    return {
      clase: i % 3 === 0 ? 'rounded-xl' : i % 3 === 1 ? 'rounded-md' : 'rounded-sm',
      color: colores[i % colores.length],
      opacidad: opacidades[i % opacidades.length],
    };
  });
}
