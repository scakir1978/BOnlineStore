import { TestBed } from '@angular/core/testing';

import { ModelGroupService } from './model-group.service';

describe('MıdekGroupService', () => {
  let service: ModelGroupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ModelGroupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
