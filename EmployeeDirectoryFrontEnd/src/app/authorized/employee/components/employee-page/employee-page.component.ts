import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Filters } from '../../../../shared/models/Filters';
import { Employee } from '../../../../shared/models/Employee';
import { EmployeeService } from '../../../../shared/services/employee/employee.service';
import { Location } from '../../../../shared/models/Location';
import { LocationService } from '../../../../shared/services/location/location.service';
import { DepartmentService } from '../../../../shared/services/department/department.service';
import { Department } from '../../../../shared/models/Department';

@Component({
  selector: 'app-employee-page',
  templateUrl: './employee-page.component.html',
  styleUrl: './employee-page.component.css'
})
export class EmployeePageComponent implements OnInit {

  employees: Employee[] = [];
  locations: Location[] = [];
  departments: Department[] = [];
  alphabetsSelected: string[] = [];
  selectedFilters?: Filters;

  constructor(private router: Router,
    private _employeeService: EmployeeService,
    private _locationService: LocationService,
    private _departmentService: DepartmentService) { }

  ngOnInit(): void {
    this._employeeService.getData(1, 10).subscribe({
      next: (response) => {
        this.employees = response.data.data;
      }
    });

    this._locationService.getData(1, 10).subscribe({
      next: (response) => {
        this.locations = response.data.data;
      }
    });

    this._departmentService.getData(1, 10).subscribe({
      next: (response) => {
        this.departments = response.data.data;
      }
    })
  }

  alphabetFiltering(alphabetSelected: string[]) {
    this.alphabetsSelected = alphabetSelected;
    this.filterBoxFiltering(this.selectedFilters);
  }

  filterBoxFiltering(filtersObj?: Filters) {
    this.selectedFilters = filtersObj;
    this.getEmployeesData(this.selectedFilters);
  }

  resetFilters() {
    this.alphabetsSelected = []
  }

  getEmployeesData(filters?: Filters) {
    this._employeeService.getData(1, 10, filters).subscribe({
      next: (response) => {
        this.employees = response.data.data
        if (this.alphabetsSelected.length > 0) {
          this.employees = this.employees.filter((emp: Employee) => {
            return this.alphabetsSelected.indexOf(emp.firstName[0].toUpperCase()) !== -1
          })
        }
        else {
          this.employees = response.data.data
        }
      }
    })
  }

  appliedFilters: Filters = { departments: [], locations: [] };
  isRolesPage = false;

  onFiltersApplied(filters: Filters) {
    this.appliedFilters = filters;
  }

  addEmployeeButton(event: Event) {
    this.router.navigate(['/auth/employee/add']);
  }

  exportToCSV() {
    let rows: Array<Employee> = this.employees;
    let data = [];
    let headers = ['Name', 'Email', 'Location', 'Department', 'Role', 'Employee Id', 'Join Date'];
    data.push(headers.join(","));
    rows.forEach((r) => {
      let row: string[] = [];
      row.push(r.firstName + " " + r.lastName);
      row.push(r.emailID);
      row.push(r.location);
      row.push(r.department);
      row.push(r.jobTitle);
      row.push(r.employeeId);
      row.push(r.joinDate);
      data.push(row.join(","));
    })
    this.downloadFile(data.join("\n"))

  }

  downloadFile(csv: string, file: string = "Employees_" + Date.now().toString() + ".csv") {
    let csv_file, download_link;
    csv_file = new Blob([csv], { type: "text/csv" });
    download_link = document.createElement('a');
    download_link.download = file;
    download_link.href = window.URL.createObjectURL(csv_file);
    download_link.style.display = "none";
    document.body.appendChild(download_link);
    download_link.click();
  }
  singleRecordDeleted() {
    this.getEmployeesData(this.selectedFilters);
  }
  multipleRecordsDeleted(employeesToDelete: string[]) {
    employeesToDelete.forEach(emp => {
      this._employeeService.deleteData(emp).subscribe({
        next: (response) => {
          this.getEmployeesData(this.appliedFilters);
        }
      })
    })
  }
}
