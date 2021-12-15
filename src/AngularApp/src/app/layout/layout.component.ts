import { ThemeService } from './../core/services/theme.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
})
export class LayoutComponent implements OnInit {
  isDark = false;

  constructor(private themeService: ThemeService) {}

  ngOnInit(): void {
    this.isDark = this.themeService.isDark.value;
  }

  toggleDark(): void {
    this.isDark = !this.isDark;
    this.themeService.setDark(this.isDark);
  }
}
