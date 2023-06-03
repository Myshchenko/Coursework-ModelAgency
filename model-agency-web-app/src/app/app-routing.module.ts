import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ModelsPageComponent } from './components/models-page/models-page.component';
import { MainComponent } from './components/main/main.component';
import { EventsPageComponent } from './components/events-page/events-page.component';
import { AddEventPageComponent } from './components/add-event-page/add-event-page.component';
import { AddModelToEventComponent } from './components/add-model-to-event/add-model-to-event.component';
import { ReportsComponent } from './components/reports/reports.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  { path: '', component: MainComponent },
  { path: 'models', component: ModelsPageComponent },
  { path: 'events', component: EventsPageComponent},
  { path: 'add-event', component: AddEventPageComponent},
  { path: 'add-model-to-event', component: AddModelToEventComponent},
  { path: 'reports', component: ReportsComponent},
  { path: 'login', component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }