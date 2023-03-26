import { TestBed } from '@angular/core/testing';

import { ColorGroupService } from './color-group.service';

describe('MıdekGroupService', () => {
  let service: ColorGroupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ColorGroupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
