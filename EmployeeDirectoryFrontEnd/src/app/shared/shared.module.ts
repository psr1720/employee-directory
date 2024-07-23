import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedRoutingModule } from './shared-routing.module';
import { LeftNavComponent } from './components/left-nav/left-nav.component';
import { SearchBarComponent } from './components/search-bar/search-bar.component';
import { FilterBoxComponent } from './components/filter-box/filter-box.component';
import { AlphabetFilterComponent } from './components/alphabet-filter/alphabet-filter.component';
import { TableComponent } from './components/table/table.component';


@NgModule({
  declarations: [
    LeftNavComponent,
    SearchBarComponent,
    FilterBoxComponent,
    AlphabetFilterComponent,
    TableComponent,
  ],
  imports: [
    CommonModule,
    SharedRoutingModule,
    FormsModule,
  ],
  exports : [
    LeftNavComponent,
    SearchBarComponent,
    AlphabetFilterComponent,
    FilterBoxComponent,
    TableComponent,
  ]
})
export class SharedModule { }
