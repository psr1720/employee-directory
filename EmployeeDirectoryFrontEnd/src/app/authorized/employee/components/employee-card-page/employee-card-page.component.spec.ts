import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeCardPageComponent } from './employee-card-page.component';

describe('EmployeeCardPageComponent', () => {
  let component: EmployeeCardPageComponent;
  let fixture: ComponentFixture<EmployeeCardPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmployeeCardPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeCardPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
