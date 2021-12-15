import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  isDark = new BehaviorSubject<boolean>(false);

  constructor() {
    const isDark = localStorage.getItem('isDark') == 'true';
    if (isDark === true) {
      this.isDark.next(true);
    } else {
      this.isDark.next(false);
    }
  }

  setDark(setDark: boolean): void {
    this.isDark.next(setDark);
    localStorage['isDark'] = setDark;
  }
}
