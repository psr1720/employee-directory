import { Component, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-left-nav',
  templateUrl: './left-nav.component.html',
  styleUrl: './left-nav.component.css'
})
export class LeftNavComponent {

  constructor(private router: Router) { }

  sideBar = "sidebar"
  @Output() expandBody = new EventEmitter()

  collapseSidebar() {
    if (this.sideBar == "sidebar") {
      this.sideBar = "sidebar active"
    }
    else {
      this.sideBar = "sidebar"
    }
    this.expandBody.emit();
  }
  homePage() {
    this.router.navigate(['/auth/employee']);
  }
  rolesPage() {
    this.router.navigate(['/auth/role']);
  }
  isActive(currentUrl: string) {
    return this.router.url.includes(currentUrl);
  }
}
