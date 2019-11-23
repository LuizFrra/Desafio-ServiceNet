/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FBService } from './FB.service';

describe('Service: FB', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FBService]
    });
  });

  it('should ...', inject([FBService], (service: FBService) => {
    expect(service).toBeTruthy();
  }));
});
