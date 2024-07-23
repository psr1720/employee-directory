import { Component, EventEmitter, ElementRef, Output, QueryList, ViewChildren, Input, AfterViewInit, OnInit } from '@angular/core';
import { CommonService } from '../../services/common/common.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-alphabet-filter',
  templateUrl: './alphabet-filter.component.html',
  styleUrl: './alphabet-filter.component.css'
})
export class AlphabetFilterComponent implements OnInit {

  constructor(private _commonService: CommonService) { }

  ngOnInit(): void {
    this.resetSubscription = this._commonService.resetObservable.subscribe(() => {
      this.filteredCharacters = [];
      this.resetCharacters();
    })
  }

  @Output() onFiltersApplied = new EventEmitter<string[]>();
  @Input() filteredCharacters: string[] = []
  resetSubscription: Subscription = new Subscription();
  alphabetButtonClassName = 'btn-alphabet'
  classMap: Map<string, string> = new Map<string, string>([
    ["A", "btn-alphabet"],
    ["B", "btn-alphabet"],
    ["C", "btn-alphabet"],
    ["D", "btn-alphabet"],
    ["E", "btn-alphabet"],
    ["F", "btn-alphabet"],
    ["G", "btn-alphabet"],
    ["H", "btn-alphabet"],
    ["I", "btn-alphabet"],
    ["J", "btn-alphabet"],
    ["K", "btn-alphabet"],
    ["L", "btn-alphabet"],
    ["M", "btn-alphabet"],
    ["N", "btn-alphabet"],
    ["O", "btn-alphabet"],
    ["P", "btn-alphabet"],
    ["Q", "btn-alphabet"],
    ["S", "btn-alphabet"],
    ["R", "btn-alphabet"],
    ["T", "btn-alphabet"],
    ["U", "btn-alphabet"],
    ["V", "btn-alphabet"],
    ["W", "btn-alphabet"],
    ["X", "btn-alphabet"],
    ["Y", "btn-alphabet"],
    ["Z", "btn-alphabet"],
  ])

  alphabetFilter(event: Event) {
    let button = event.target as HTMLElement;
    let letter = button?.innerText;
    if (this.filteredCharacters.indexOf(letter) == -1) {
      this.filteredCharacters.push(letter);
      this.classMap.set(letter,"btn-alphabet active")
    }
    else if (this.filteredCharacters.indexOf(letter) != -1) {
      this.filteredCharacters.splice(this.filteredCharacters.indexOf(letter), 1);
      this.classMap.set(letter,"btn-alphabet")
    }
    this.onFiltersApplied.emit(this.filteredCharacters);
  }

  resetCharacters() {
    this.onFiltersApplied.emit([])
    this.classMap.forEach((value, key) => {
      this.classMap.set(key, "btn-alphabet");
  });
  }

}
