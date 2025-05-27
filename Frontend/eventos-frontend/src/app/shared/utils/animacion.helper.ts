export class AnimacionHelper {
  /**
   * Aplica animación flotante aleatoria a los elementos con clase `.animate-float-square`
   */
  static animarCuadrados(): void {
    document.querySelectorAll('.animate-float-square').forEach(square => {
      const element = square as HTMLElement;

      const width = this.random(60, 100);
      const height = this.random(60, 100);
      const top = this.random(0, 90); // para evitar que se salgan
      const left = this.random(0, 90);
      const duration = this.random(5, 30); // segundos

      Object.assign(element.style, {
        width: `${width}px`,
        height: `${height}px`,
        top: `${top}%`,
        left: `${left}%`,
        animationDuration: `${duration}s`,
        position: 'absolute',
        animationName: 'bounce',
        animationTimingFunction: 'ease-in-out',
        animationIterationCount: 'infinite',
      });
    });
  }

  private static random(min: number, max: number): number {
    return Math.random() * (max - min) + min;
  }
}
