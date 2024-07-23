import { Component } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { EmployeeService } from '../../services/employee/employee.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrl: './search-bar.component.css'
})
export class SearchBarComponent {
  nameIdentifier: string | null = null;
  employeeName? : string;
  employeeRole?: string
  employeeProfilePicture = '';
  arrowDirection: boolean = false;

  constructor(private router: Router,
    private _employeeService: EmployeeService) { }

  ngOnInit() {
    const token = localStorage.getItem('AuthToken');
    if (token) {
      const decodedToken = jwtDecode<any>(token);
      this.nameIdentifier = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] as string
      this._employeeService.getDataByID(this.nameIdentifier?.trim()).subscribe({
        next: (response) => {
          this.employeeName = response.data.firstName + " " + response.data.lastName;
          this.employeeRole = response.data.jobTitle;
          this.employeeProfilePicture = response.data.profilePicture;
        }
      });
    }
  }

  toggleArrow() {
    console.log(this.arrowDirection)
    this.arrowDirection = !this.arrowDirection;
  }

  changePasswordButton() {
    this.arrowDirection = !this.arrowDirection;
    this.router.navigate(['/auth/employee/changepassword'])

  }
  logoutButton() {
    localStorage.removeItem('AuthToken');
    this.router.navigate(['/unauth/login'])
  }


}
