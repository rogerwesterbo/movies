import { ErrorRoutingModule } from './error-routing';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorComponent } from './error.component';
import { errorPages } from './pages';

@NgModule({
  declarations: [ErrorComponent, ...errorPages],
  imports: [CommonModule, ErrorRoutingModule],
})
export class ErrorModule {}
