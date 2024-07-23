import { Component, OnInit } from '@angular/core';
import { Employee } from '../../../../shared/models/Employee';
import { ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../../../../shared/services/employee/employee.service';

@Component({
  selector: 'app-employee-card-page',
  templateUrl: './employee-card-page.component.html',
  styleUrl: './employee-card-page.component.css'
})
export class EmployeeCardPageComponent implements OnInit {

  constructor(private readonly route: ActivatedRoute,
    private _employeeService: EmployeeService) { }

  employeeObjects: Employee[] = []
  
  ngOnInit(): void {
    let roleID = this.route.snapshot.params['id'];
    this._employeeService.getDataByRoleID(roleID).subscribe({
      next: (response) => {
        this.employeeObjects = response.data.data;
      }
    })
  }
}
