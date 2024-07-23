import { Component } from '@angular/core';
import { EmployeeService } from '../../../../shared/services/employee/employee.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ChangePassword } from '../../../../shared/models/ChangePassword';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-employee-change-password-page',
  templateUrl: './employee-change-password-page.component.html',
  styleUrl: './employee-change-password-page.component.css'
})
export class EmployeeChangePasswordPageComponent {

  constructor(private router: Router,
    private _employeeService: EmployeeService,
    private toast: NgToastService) { }

  changePasswordForm = new FormGroup({
    username: new FormControl('', Validators.required),
    oldPassword: new FormControl('', Validators.required),
    newPassword: new FormControl('', Validators.required),
  })

  onSubmit() {
    if (this.changePasswordForm.valid) {
      const formValues = this.changePasswordForm.value;
      let changePassword: ChangePassword = {
        username: formValues.username,
        oldPassword: formValues.oldPassword,
        newPassword: formValues.newPassword,
      }
      this._employeeService.changePassword(changePassword).subscribe({
        next: () => {
          this.toast.success("Employee Password has been changed successfully", "Success", 3000);
          this.router.navigate(['/auth/employee']);
        },
        error: () => {
          this.toast.danger("Error occured while changing password!", "Failure", 3000);
          this.router.navigate(['/auth/employee']);
        }
      })
    }
  }
}