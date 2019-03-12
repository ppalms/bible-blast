/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { KidService } from './kid.service';

describe('Service: Kid', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [KidService]
    });
  });

  it('should ...', inject([KidService], (service: KidService) => {
    expect(service).toBeTruthy();
  }));
});
