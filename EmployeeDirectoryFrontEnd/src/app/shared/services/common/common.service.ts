import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  constructor() { }

  resetSubject = new Subject<void>();
  resetObservable = this.resetSubject.asObservable();

  triggerReset(){
    this.resetSubject.next();
  }
}
