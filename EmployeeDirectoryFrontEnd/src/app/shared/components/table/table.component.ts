import { Component, ElementRef, EventEmitter, Input, Output, QueryList, ViewChildren } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from '../../models/Employee';
import { EmployeeService } from '../../services/employee/employee.service';


@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrl: './table.component.css'
})
export class TableComponent {
  @ViewChildren('checkBoxRef') checkboxes!: QueryList<ElementRef>;
  @Input() employeeObjects: Employee[] = [];
  @Output() onRecordDelete = new EventEmitter();
  @Output() onMultipleRecordsDelete = new EventEmitter<string[]>();
  deleteRecordIds: string[] = []
  deleteButtonClass: string = "btn-delete";
  status = 'Active';
  ascOrder: boolean = true;

  constructor(private employeeService: EmployeeService, private router: Router) { }

  sortColumn(event: Event) {
    let column = (event.target as HTMLElement).closest('.sortButton')
    let columnName = column?.classList[1] as keyof Employee;
    this.employeeObjects = this.sortingFn(this.employeeObjects, columnName, this.ascOrder)
    this.ascOrder = !this.ascOrder
  }

  sortingFn<T>(objects: T[], property: keyof T, ascOrder: boolean) {
    return objects.sort((a, b) => {
      const x = a[property]
      const y = b[property]
      if (x < y) {
        return ascOrder ? -1 : 1;
      }
      if (x > y) {
        return ascOrder ? 1 : -1;
      }
      return 0
    })
  }

  outerCheckBoxClick(event: Event) {
    let main_checkbox = event.target as HTMLInputElement;
    this.checkboxes.forEach((checkbox) => {
      const input = checkbox.nativeElement as HTMLInputElement;
      input.checked = main_checkbox.checked;
    });
    if (main_checkbox.checked) {
      this.deleteRecordIds = []
      this.employeeObjects.forEach(e =>
        this.deleteRecordIds.push(e.employeeId)
      )
    }
    else {
      this.deleteRecordIds = []
    }
    this.activateDeleteButton();
  }

  innerCheckBoxClick(empId:string) {
    if (this.deleteRecordIds.indexOf(empId) == -1) {
      this.deleteRecordIds.push(empId);
    }
    else {
      this.deleteRecordIds.splice(this.deleteRecordIds.indexOf(empId), 1)
    }
    this.activateDeleteButton();
  }

  activateDeleteButton() {
    let anyCheck = false
    for (let i = 0; i < this.checkboxes.length; i++) {
      const input = this.checkboxes.get(i)?.nativeElement as HTMLInputElement;
      if (input.checked == true) {
        anyCheck = true;
        break;
      }
    }
    if (anyCheck) {
      this.deleteButtonClass = "btn-delete active";
    }
    else {
      this.deleteButtonClass = "btn-delete";
    }

  }

  ellipsesClick(event: Event) {
    let click = event.target as HTMLAnchorElement;
    let ellipse = click.closest('.triple-dot');
    ellipse?.classList.toggle('active');
  }

  ellipseViewClick(empNo: string) {
    this.router.navigate(['/auth/employee/add'], { queryParams: { viewMode: true, employeeId: empNo } });
  }

  ellipseEditClick(empNo: string) {
    this.router.navigate(['/auth/employee/add'], { queryParams: { editMode: true, employeeId: empNo } });
  }

  ellipseDeleteClick(empNo: string) {
    this.employeeService.deleteData(empNo).subscribe({
      next: () => {
        this.onRecordDelete.emit();
      },
      error: () => {

      }
    });

  }

  multipleDeleteButton() {
    this.deleteButtonClass = "btn-delete";
    this.onMultipleRecordsDelete.emit(this.deleteRecordIds);
  }
}