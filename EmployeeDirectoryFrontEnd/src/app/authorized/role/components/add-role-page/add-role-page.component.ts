import { Component, OnInit } from '@angular/core';
import { Employee } from '../../../../shared/models/Employee';
import { EmployeeService } from '../../../../shared/services/employee/employee.service';
import { RoleService } from '../../../../shared/services/role/role.service';
import { DepartmentService } from '../../../../shared/services/department/department.service';
import { LocationService } from '../../../../shared/services/location/location.service';
import { Department } from '../../../../shared/models/Department';
import { Location } from '../../../../shared/models/Location';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { RoleRequest } from '../../../../shared/models/RoleRequest';
import { Role } from '../../../../shared/models/Role';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';


@Component({
  selector: 'app-add-role-page',
  templateUrl: './add-role-page.component.html',
  styleUrl: './add-role-page.component.css'
})
export class AddRolePageComponent implements OnInit {

  employeeObjects: Employee[] = [];
  locations: Location[] = [];
  departments: Department[] = [];
  prefillRole: Role = {
    id: 0,
    name: '',
    description: '',
    department: '',
    location: '',
    imgArray: []
  }
  selectedEmployee: number[] = [];
  submitButtonText = "Add Role";
  title = "Create New Role";

  constructor(private route: ActivatedRoute,
    private router: Router,
    private _employeeService: EmployeeService,
    private _roleService: RoleService,
    private _departmentService: DepartmentService,
    private _locationService: LocationService,
    private toast: NgToastService) { }

  ngOnInit(): void {
    let url = this.route.snapshot;

    if (url.queryParamMap.has('editMode')) {
      this.getRoleById(Number(url.queryParamMap.get('roleId')));
      this.submitButtonText = "Update Role"
      this.title = "Edit Role"
    }

    this._employeeService.getData(1, 10).subscribe({
      next: (response) => {
        this.employeeObjects = response.data.data;
      }
    });
    this._departmentService.getData(1, 10).subscribe({
      next: (response) => {
        this.departments = response.data.data
        this.roleForm.get('departmentId')?.setValue(this.departments[0].id);
      }
    });
    this._locationService.getData(1, 10).subscribe({
      next: (response) => {
        this.locations = response.data.data;
        this.roleForm.get('locationId')?.setValue(this.locations[0].id);
      }
    })
  }

  roleForm = new FormGroup({
    name: new FormControl('', Validators.required),
    description: new FormControl(''),
    locationId: new FormControl(),
    departmentId: new FormControl(),
  })

  getRoleById(id: number) {
    this._roleService.getDataById(id).subscribe({
      next: (response) => {
        this.prefillRole = response.data
        this.prefillFormValues(this.prefillRole);
        let employees = response.data.imgArray as Employee[]
        for(let i = 0;i<employees.length; i++){
          this.selectedEmployee.push(employees[i].id);
        }
      }
    })
  }

  prefillFormValues(role: Role) {
    let locationId;
    let departmentId;

    for (let location of this.locations) {
      if (role.location == location.name) {
        locationId = location.id
      }
    }

    for (let department of this.departments) {
      if (role.department == department.name) {
        departmentId = department.id
      }
    }

    this.roleForm.patchValue({
      name: role.name,
      description: role.description,
      locationId: locationId,
      departmentId: departmentId,
    })
      
  }
  isSelected(employeeId: number): boolean {
    return this.selectedEmployee.includes(employeeId);
  }

  employeeChecked(empId: number) {
    if (this.selectedEmployee.indexOf(empId) == -1) {
      this.selectedEmployee.push(empId);
    }
    else {
      this.selectedEmployee.splice(this.selectedEmployee.indexOf(empId), 1)
    }
    console.log(this.selectedEmployee);
  }

  onSubmit() {
    if (this.roleForm.valid) {
      const formValues = this.roleForm.value;
      let roleRequest: RoleRequest = {
        id: this.prefillRole.id != 0 ? this.prefillRole.id : 0,
        name: formValues.name,
        description: formValues.description,
        departmentId: formValues.departmentId,
        locationId: formValues.locationId,
      }
      let url = this.route.snapshot;
      if (url.queryParamMap.has('editMode')) {
        this._roleService.updateData(roleRequest.id, roleRequest).subscribe({
          next: (response) => {
            this.selectedEmployee.forEach(e => {
              this._employeeService.updateJobTitleId(e, response.data.id).subscribe({})
            });
            this.toast.success("Role has been updated successfully!", "Success", 3000);
            this.router.navigate(['/auth/role'])
          },
          error: (err) => {
            this.toast.danger("Error occured while updating role!", "Failure", 3000);
            this.router.navigate(['/auth/role'])
          }
        })
      }
      else {
        this._roleService.postData(roleRequest).subscribe({
          next: (response) => {
            this.selectedEmployee.forEach(e => {

              this._employeeService.updateJobTitleId(e, response.data.id).subscribe({})
            });
            this.toast.success("Role has been added successfully!", "Success", 3000);
            this.router.navigate(['/auth/role'])
          },
          error: (err) => {
            this.toast.danger("Error occured while adding role!", "Failure", 3000);
            this.router.navigate(['/auth/role'])
          }
        })
      }
    }
  }
}
