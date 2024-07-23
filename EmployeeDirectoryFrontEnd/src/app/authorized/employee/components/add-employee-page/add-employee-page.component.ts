import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../../../shared/services/employee/employee.service';
import { RoleService } from '../../../../shared/services/role/role.service';
import { ProjectService } from '../../../../shared/services/project/project.service';
import { LocationService } from '../../../../shared/services/location/location.service';
import { Location } from '../../../../shared/models/Location';
import { Role } from '../../../../shared/models/Role';
import { Option } from '../../../../shared/models/Option';
import { Project } from '../../../../shared/models/Project';
import { Employee } from '../../../../shared/models/Employee';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { EmployeeRequest } from '../../../../shared/models/EmployeeRequest';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-add-employee-page',
  templateUrl: './add-employee-page.component.html',
  styleUrl: './add-employee-page.component.css'
})
export class AddEmployeePageComponent implements OnInit {

  constructor(private router: Router,
    private _employeeService: EmployeeService,
    private _roleSerivce: RoleService,
    private _projectService: ProjectService,
    private _locationService: LocationService,
    private activatedRoute: ActivatedRoute,
    private toast: NgToastService
  ) { }

  profilePicSrc: string = "../../assets/images/profilePicDefault.jpg"
  editMode: boolean = false;
  employeeObjects: Employee[] = [];
  locations: Location[] = [];
  jobTitles: Role[] = [];
  managers: Option[] = [];
  projects: Project[] = [];
  prefillEmployee!: Employee;
  employeeIdReadOnly: boolean = false;
  allFieldsReadOnly: boolean = false;
  today = new Date().toISOString().split('T')[0];
  title = "Add Employee";
  pictureUploadButtonText = "Upload Profile Picture";
  submitButtonText = "Add Employee";
  currentURL = '';

  employeeForm = new FormGroup({
    profilePicture: new FormControl('',),
    employeeId: new FormControl('', [Validators.required, Validators.pattern("[T]{1}[Z]{1}[0-9]{4}$")]),
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    dob: new FormControl('', [Validators.required]),
    emailID: new FormControl('', [Validators.required, Validators.email]),
    phoneNo: new FormControl('', [Validators.required, Validators.pattern("[6-9]{1}[0-9]{9}")]),
    joinDate: new FormControl('', [Validators.required]),
    locationId: new FormControl({ value: 0, disabled: this.allFieldsReadOnly }),
    jobTitleId: new FormControl({ value: 0, disabled: this.allFieldsReadOnly }),
    managerId: new FormControl({ value: 0, disabled: this.allFieldsReadOnly }),
    projectId: new FormControl({ value: 0, disabled: this.allFieldsReadOnly })
  })

  ngOnInit(): void {
    this.getEmployeeData();
    this.getLocationData();
    this.getProjectData();
    this.getRoleData();
    let url = this.activatedRoute.snapshot;
    if (url.queryParamMap.has('editMode') == true) {
      this.getEmployeeById(String(url.queryParamMap.get('employeeId')));
      this.employeeIdReadOnly = true;
      this.title = "Edit Employee";
      this.pictureUploadButtonText = "Update Profile Picture"
      this.submitButtonText = "Update Employee"
    }
    if (url.queryParamMap.has('viewMode') == true) {
      this.getEmployeeById(String(url.queryParamMap.get('employeeId')))
      this.employeeIdReadOnly = true;
      this.allFieldsReadOnly = true;
      this.title = "View Employee";
    }
  }

  getEmployeeById(id: string) {
    this._employeeService.getDataByID(id).subscribe({
      next: (resonse) => {
        this.prefillEmployee = resonse.data;
        this.prefillFormValues(this.prefillEmployee)
      },
      error: (err) => {
        this.router.navigate(['auth/employee'])
      }
    })

  }

  getEmployeeData(): void {
    this._employeeService.getData(1, 10).subscribe(response => {
      this.employeeObjects = response.data.data as Employee[];
      this.employeeObjects.forEach(e => {
        const name = e.firstName + ' ' + e.lastName;
        const id = e.id;
        const opt: Option = { id: id, name: name };
        this.managers.push(opt);
      });
    });
  }

  getRoleData(): void {
    this._roleSerivce.getData(1, 10).subscribe(response => {
      this.jobTitles = response.data.data;
    });
  }

  getProjectData(): void {
    this._projectService.getData(1, 10).subscribe(response => {
      this.projects = response.data.data;
    });
  }

  getLocationData(): void {
    this._locationService.getData(1, 10).subscribe(response => {
      this.locations = response.data.data;
    });
  }

  viewSrcChange(event: Event) {
    let displayPicture = event.target as HTMLInputElement
    let newFile: File
    if (displayPicture.files != null) {
      newFile = displayPicture.files[0];
      this.profilePicSrc = URL.createObjectURL(newFile);
      const fr = new FileReader();
      fr.addEventListener('load', () => {
        this.currentURL = fr.result as string
      });
      fr.readAsDataURL(newFile);
    }
  }

  prefillFormValues(employee: Employee) {
    if (employee.profilePicture != null && employee.profilePicture.trim() != '') {
      this.profilePicSrc = employee.profilePicture
    }
    let locationId;
    let jobTitleId;
    let managerId;
    let projectId;

    for (let location of this.locations) {
      if (employee.location == location.name) {
        locationId = location.id
      }
    }

    for (let jobTitle of this.jobTitles) {
      if (employee.jobTitle == jobTitle.name) {
        jobTitleId = jobTitle.id
      }
    }

    for (let manager of this.managers) {
      if (employee.manager == manager.name) {
        managerId = manager.id
      }
    }

    for (let project of this.projects) {
      if (employee.project == project.name) {
        projectId = project.id
      }
    }

    this.employeeForm.patchValue({
      employeeId: employee.employeeId,
      firstName: employee.firstName,
      lastName: employee.lastName,
      dob: employee.dob.substring(0, 10),
      emailID: employee.emailID,
      phoneNo: employee.phoneNo,
      joinDate: employee.joinDate.substring(0, 10),
      locationId: locationId,
      jobTitleId: jobTitleId,
      managerId: managerId,
      projectId: projectId
    })
  }

  cancelButton() {
    this.router.navigate(['/auth/employee'])
  }

  onSubmit() {
    if (this.employeeForm.valid) {
      const formValues = this.employeeForm.value;
      let employeeRequest: EmployeeRequest = {
        id: this.prefillEmployee.id != 0 ? this.prefillEmployee.id : 0,
        profilePicture: this.currentURL,
        employeeId: formValues.employeeId,
        firstName: formValues.firstName,
        lastName: formValues.lastName,
        dob: formValues.dob,
        emailID: formValues.emailID,
        phoneNo: formValues.phoneNo,
        joinDate: formValues.joinDate,
        locationId: formValues.locationId,
        jobTitleId: formValues.jobTitleId,
        managerId: formValues.managerId,
        projectId: formValues.projectId,
        password: "password",
      }
      let url = this.activatedRoute.snapshot;
      if (url.queryParamMap.has('editMode') == true) {
        this._employeeService.updateData(employeeRequest.id, employeeRequest).subscribe({
          next: (response) =>{
            this.toast.success("Employee has been updated successfully!", "Success", 3000);
            this.router.navigate(['/auth/employee']);
          },
          error: (err) =>{
            this.toast.danger("Error occured while updating employee!", "Failure", 3000);
            this.router.navigate(['/auth/employee']);
          }
        })
      }
      else {
        this._employeeService.postData(employeeRequest).subscribe({
          next: (response) => {
            this.toast.success("Employee has been added successfully!", "Success", 3000);
            this.router.navigate(['/auth/employee']);
          },
          error: (err) => {
            this.toast.danger("Error occured while adding employee!", "Failure", 3000);
            this.router.navigate(['/auth/employee']);
          }
        })
      }
    }
    else{
      
    }
  }
}
