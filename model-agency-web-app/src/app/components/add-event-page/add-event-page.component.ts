import { Component } from '@angular/core';
import { EventService } from 'src/app/services/EventService';
import { Event } from 'src/app/models/Event';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User';

@Component({
  selector: 'app-add-event-page',
  templateUrl: './add-event-page.component.html',
  styleUrls: ['./add-event-page.component.css']
})
export class AddEventPageComponent {
 
  event: Event = {} as Event;
  currentUser: User = {} as User;

  constructor(private router: Router, private eventService: EventService){
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras.state) {
      if(navigation.extras.state['event']){
        this.event = navigation.extras.state['event'];
      }
      this.currentUser = navigation.extras.state['currentUser'];
    }
  }

  addEvent(){
    if(this.event){
      const userId = localStorage.getItem('userId');
      if (userId !== null) {
      this.event.createdBy = parseInt(userId);
    }

      this.eventService.addEvent(this.event);
    }
  }

  updateEvent(){
    if(this.event){
      this.eventService.updateEvent(this.event);
    }
  }

  getCurrentDateTime(): string {
  const now = new Date();
  now.setDate(now.getDate() + 1);
  const year = now.getFullYear();
  const month = this.padNumber(now.getMonth() + 1);
  const day = this.padNumber(now.getDate());
  const hour = this.padNumber(now.getHours());
  const minute = this.padNumber(now.getMinutes());

  return `${year}-${month}-${day}T${hour}:${minute}`;
}

private padNumber(number: number): string {
  return number.toString().padStart(2, '0');
}
}
