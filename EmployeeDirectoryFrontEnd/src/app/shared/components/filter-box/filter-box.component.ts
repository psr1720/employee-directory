import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Department } from '../../models/Department';
import { Location } from '../../models/Location';
import { Filters } from '../../models/Filters';
import { CommonService } from '../../services/common/common.service';

@Component({
  selector: 'app-filter-box',
  templateUrl: './filter-box.component.html',
  styleUrl: './filter-box.component.css'
})
export class FilterBoxComponent {

  constructor(private _commonService: CommonService) { }

  @Input() isRolesPage: boolean = false;
  @Input() locations: Location[] = []
  @Input() departments: Department[] = [];
  @Output() onFiltersApplied = new EventEmitter<Filters>();

  filter: Filters = {
    departments: [],
    locations: []
  };

  resetButton(locationSelect : HTMLSelectElement, departmentSelect: HTMLSelectElement) {
    locationSelect.selectedIndex = 0
    departmentSelect.selectedIndex = 0
    this.onFiltersApplied.emit({ departments: [], locations: [] })
    this._commonService.triggerReset();

  }

  applyButton(location: string, department: string, status?: string) {
    let departments: number[] = []
    let locations: number[] = []
    if (Number(department) > 0) {
      departments.push(Number(department))
    }
    if (Number(location) > 0) {
      locations.push(Number(location))
    }
    let filters: Filters = {
      departments: departments,
      locations: locations
    };
    this.onFiltersApplied.emit(filters);
  }
}

