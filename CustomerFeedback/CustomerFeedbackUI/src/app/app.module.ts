import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddFeedbackComponent } from './Feedback/add-feedback/add-feedback.component';
import { HttpClient,HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { EmailDirective } from './Shared/email.directive';
import { DisplayFeedbackComponent } from './Feedback/display-feedback/display-feedback.component';
import { Routes } from '@angular/router';



@NgModule({
  declarations: [
    AppComponent,
    AddFeedbackComponent,
    EmailDirective,
    DisplayFeedbackComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
