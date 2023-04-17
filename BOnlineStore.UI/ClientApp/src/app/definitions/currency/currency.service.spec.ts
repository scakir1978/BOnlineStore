import { TestBed } from '@angular/core/testing';

import { AssemblerService } from './currency.service';

describe('MıdekGroupService', () => {
  let service: AssemblerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AssemblerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
