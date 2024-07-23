import { Component, OnInit } from '@angular/core';
import { RoleService } from '../../../../shared/services/role/role.service';
import { Role } from '../../../../shared/models/Role';
import { Router } from '@angular/router';
import { Location } from '../../../../shared/models/Location';
import { Department } from '../../../../shared/models/Department';
import { LocationService } from '../../../../shared/services/location/location.service';
import { DepartmentService } from '../../../../shared/services/department/department.service';
import { Filters } from '../../../../shared/models/Filters';

@Component({
  selector: 'app-role-page',
  templateUrl: './role-page.component.html',
  styleUrl: './role-page.component.css'
})
export class RolePageComponent implements OnInit {

  roleObjects: Role[] = []
  locations: Location[] = []
  departments: Department[] = []

  constructor(private router: Router,
    private _roleService: RoleService,
    private _locationService: LocationService,
    private _departmentService: DepartmentService
    ) { }

  ngOnInit(): void {
    this._roleService.getData(1, 10).subscribe({
      next: (response) => {
        this.roleObjects = response.data.data;
      }
    });
    this._locationService.getData(1,10).subscribe({
      next: (response) =>{
        this.locations = response.data.data;
      }
    });
    this._departmentService.getData(1,10).subscribe({
      next: (response) =>{
        this.departments = response.data.data;
      } 
    })
  }

  getRolesData(filters?: Filters) {
    this._roleService.getData(1, 10, filters).subscribe({
      next: (response) => {
        this.roleObjects = response.data.data       
      }
    })
  }

  addRoleButton() {
    this.router.navigate(['/auth/role/add']);
  }

  editRoleButton(id: number){
    this.router.navigate(['/auth/role/add'],{queryParams : {editMode: true, roleId: id}})
  }

  filterBoxFiltering(filters: Filters){
    this.getRolesData(filters)
  }

}
