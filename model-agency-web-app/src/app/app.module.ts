import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ModelsPageComponent } from './components/models-page/models-page.component';
import { ModelCardComponent } from './components/model-card/model-card.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HeaderComponent } from './components/header/header.component';
import { MainComponent } from './components/main/main.component';
import { RouterModule } from '@angular/router';
import { FooterComponent } from './components/footer/footer.component';
import { EventsPageComponent } from './components/events-page/events-page.component';
import { EventCardComponent } from './components/event-card/event-card.component';
import { AddEventPageComponent } from './components/add-event-page/add-event-page.component';
import { AddModelToEventComponent } from './components/add-model-to-event/add-model-to-event.component';
import { ReportsComponent } from './components/reports/reports.component';
import { ReportCardComponent } from './components/report-card/report-card.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    ModelsPageComponent,
    ModelCardComponent,
    HeaderComponent,
    MainComponent,
    FooterComponent,
    EventsPageComponent,
    EventCardComponent,
    AddEventPageComponent,
    AddModelToEventComponent,
    ReportsComponent,
    ReportCardComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    NgbModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
