import { TestBed } from '@angular/core/testing';

import { InscripcionServiceService } from './inscripcion.service.service';

describe('InscripcionServiceService', () => {
  let service: InscripcionServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InscripcionServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
