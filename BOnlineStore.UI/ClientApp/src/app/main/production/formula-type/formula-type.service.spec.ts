import { TestBed } from '@angular/core/testing';

import { FormulaTypeService } from './formula-type.service';

describe('MıdekGroupService', () => {
  let service: FormulaTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormulaTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
