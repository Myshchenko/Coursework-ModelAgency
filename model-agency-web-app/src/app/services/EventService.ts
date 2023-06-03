import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of, take } from 'rxjs';
import { environment } from 'src/environment';
import { Event } from '../models/Event';
import { ModelEvent } from '../models/ModelEvent';
import { AddModelToEventComponent } from '../components/add-model-to-event/add-model-to-event.component';
import { ModelEventCoordinates } from '../models/ModelEventCoordinates';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
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
    return this.http.put(this.baseUrl + 'event/updateModelEventResponce', modelEventCoordinates).subscribe(() => {
      window.location.reload();
    });;
  }

  addEvent(event: Event){
    return this.http.post(this.baseUrl + 'event/add', event).subscribe(() => {
      console.log("ss");
    });;
  }

  addModelToTheEvent(modelEventCoordinates: ModelEventCoordinates){
    return this.http.post(this.baseUrl + 'event/addModelToTheEvent', modelEventCoordinates).subscribe(() => {
      console.log("ok");
    });;
  }

  updateEvent(event: Event){
    return this.http.put(this.baseUrl + 'event/update', event).subscribe(() => {
      console.log("event updated");
    });;
  }

  deletePhoto(eventId: number) {
    return this.http.delete(this.baseUrl + 'event/delete/' + eventId);
  }
}
