import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkOrderDefaultFormComponent } from './work-order-default-form.component';

describe('WorkOrderDefaultFormComponent', () => {
  let component: WorkOrderDefaultFormComponent;
  let fixture: ComponentFixture<WorkOrderDefaultFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WorkOrderDefaultFormComponent]
    });
    fixture = TestBed.createComponent(WorkOrderDefaultFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
