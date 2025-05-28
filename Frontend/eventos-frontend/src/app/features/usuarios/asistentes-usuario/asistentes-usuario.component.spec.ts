import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AsistentesUsuarioComponent } from './asistentes-usuario.component';

describe('AsistentesUsuarioComponent', () => {
  let component: AsistentesUsuarioComponent;
  let fixture: ComponentFixture<AsistentesUsuarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AsistentesUsuarioComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AsistentesUsuarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
