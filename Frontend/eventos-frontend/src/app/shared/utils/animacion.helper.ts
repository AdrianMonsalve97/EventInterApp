export class AnimacionHelper {
  /**
   * Aplica animación flotante a los elementos con clase `.animate-float-square`
   */
  static animarCuadrados(): void {
    document.querySelectorAll('.animate-float-square').forEach(square => {
      const element = square as HTMLElement;

      Object.assign(element.style, {
        width: `${Math.random() * (100 - 60) + 60}px`,
        height: `${Math.random() * (100 - 60) + 60}px`,
        top: `${Math.random() * 100}%`,
        left: `${Math.random() * 100}%`,
        animationDuration: `${Math.random() * (10 - 5) + 5}s`
      });
    });
  }
}
