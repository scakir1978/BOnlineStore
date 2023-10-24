import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormulaFormComponent } from './formula-form.component';

describe('FormulaFormComponent', () => {
  let component: FormulaFormComponent;
  let fixture: ComponentFixture<FormulaFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FormulaFormComponent]
    });
    fixture = TestBed.createComponent(FormulaFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
