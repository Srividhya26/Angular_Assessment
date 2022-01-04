import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddFeedbackComponent } from './Feedback/add-feedback/add-feedback.component';

const routes: Routes = [
   {
     path : '',component : AddFeedbackComponent
   },
   {
     path : 'Feedback/:id',component : AddFeedbackComponent
   }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
