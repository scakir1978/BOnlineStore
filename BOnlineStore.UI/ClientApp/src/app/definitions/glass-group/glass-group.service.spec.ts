import { TestBed } from '@angular/core/testing';

import { GlassGroupService } from './glass-group.service';

describe('MıdekGroupService', () => {
  let service: GlassGroupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GlassGroupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
