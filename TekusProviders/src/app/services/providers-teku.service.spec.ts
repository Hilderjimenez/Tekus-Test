import { TestBed } from '@angular/core/testing';

import { ProvidersTekuService } from './providers-teku.service';

describe('ProvidersTekuService', () => {
  let service: ProvidersTekuService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProvidersTekuService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
