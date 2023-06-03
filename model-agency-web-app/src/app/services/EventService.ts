import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map, of, take, tap, throwError } from 'rxjs';
import { environment } from 'src/environment';
import { Event } from '../models/Event';
import { ModelEvent } from '../models/ModelEvent';
import { AddModelToEventComponent } from '../components/add-model-to-event/add-model-to-event.component';
import { ModelEventCoordinates } from '../models/ModelEventCoordinates';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private toastr: ToastrService, private router: Router) {
  }

  getEvents(){
    return this.http.get<Event[]>(this.baseUrl + 'event');
  }

  getModelsEvents(modelId: number){
    let params = new HttpParams().set('Id', modelId);
    return this.http.get<ModelEvent[]>(this.baseUrl + 'event/eventsForModel', { params: params });
  }

  getAvailableEventsForSpecificModel(modelId: number){
    let params = new HttpParams().set('ModelId', modelId);
    return this.http.get<Event[]>(this.baseUrl + 'event/availableEventsForSpecificModel', { params: params });
  }

  updateModelResponce(modelEventCoordinates: ModelEventCoordinates){
    return this.http.put(this.baseUrl + 'event/updateModelEventResponce', modelEventCoordinates)
    .pipe(
      catchError((error) => {
        this.toastr.error("Щось пішло не так. Повторіть запит пізніше.");
        return throwError('An error occurred while accepting/declining the event.');
      })
    ).subscribe(() => {
      this.toastr.success("Відповідь прийнята.");
      setTimeout(() => {window.location.reload();}, 1000);
    });
  }

  addEvent(event: Event){
    return this.http.post(this.baseUrl + 'event/add', event)
    .pipe(
      catchError((error) => {
        this.toastr.error("Щось пішло не так. Перевірте правильність введення даних.");
        return throwError('An error occurred while adding the event.');
      })
    )
    .subscribe(() => {
      console.log('Event added successfully');
      this.toastr.success("Захід додано.");
      setTimeout(() => {this.router.navigate(['/events']);}, 1000);
    });
  }

  addModelToTheEvent(modelEventCoordinates: ModelEventCoordinates){
    return this.http.post(this.baseUrl + 'event/addModelToTheEvent', modelEventCoordinates)
    .pipe(
      catchError((error) => {
        this.toastr.error("Щось пішло не так. Повторіть запит пізніше.");
        return throwError('An error occurred.');
      })
    ).subscribe(() => {
      this.toastr.success("Модель додана до заходу.");
      setTimeout(() => {this.router.navigate(['/models']);}, 1000);
    });
  }

  updateEvent(event: Event){
    return this.http.put(this.baseUrl + 'event/update', event)
    .pipe(
      catchError((error) => {
        this.toastr.error("Щось пішло не так. Повторіть запит пізніше.");
        return throwError('An error occurred.');
      })
    ).subscribe(() => {
      this.toastr.success("Захід оновено.");
      setTimeout(() => {this.router.navigate(['/events']);}, 1000);
    });
  }

  deletePhoto(eventId: number) {
    return this.http.delete(this.baseUrl + 'event/delete/' + eventId)
    .pipe(
      catchError((error) => {
        this.toastr.error("Щось пішло не так. Повторіть запит пізніше.");
        return throwError('An error occurred.');
      })
    ).subscribe(() => {
      this.toastr.success("Захід видалено.");
      setTimeout(() => {window.location.reload();}, 1000);
    });
  }
}
