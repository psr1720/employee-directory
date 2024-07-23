import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeChangePasswordPageComponent } from './employee-change-password-page.component';

describe('EmployeeChangePasswordPageComponent', () => {
  let component: EmployeeChangePasswordPageComponent;
  let fixture: ComponentFixture<EmployeeChangePasswordPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmployeeChangePasswordPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeChangePasswordPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
