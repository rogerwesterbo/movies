import { ActorsRoutingModule } from './actors.routing';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActorsComponent } from './actors.component';

@NgModule({
  declarations: [ActorsComponent],
  imports: [CommonModule, ActorsRoutingModule],
})
export class ActorsModule {}
