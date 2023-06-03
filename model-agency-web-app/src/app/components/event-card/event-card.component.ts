import { Component, Input } from '@angular/core';
import { Event } from 'src/app/models/Event';
import { EventService } from 'src/app/services/EventService';
import { ModelEventCoordinates } from 'src/app/models/ModelEventCoordinates';
import { ModelEventResponceType } from 'src/app/models/ModelEventResponceType';
import { Router, ActivatedRoute } from '@angular/router';
import { User } from 'src/app/models/User';

@Component({
  selector: 'app-event-card',
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.css']
})
export class EventCardComponent {
 @Input() event: Event | undefined;
 @Input() status: string = "";
 @Input() currentUser: User = {} as User;

 roleId: number = -1;
 userId: number = -1;

  constructor(private router: Router, private eventService: EventService){
    
    const role_id = localStorage.getItem('roleId');

    if (role_id !== null) {
      this.roleId = parseInt(role_id);
    }

    const user_id = localStorage.getItem('userId');

    if (user_id !== null) {
      this.userId = parseInt(user_id);
    }
  }

  acceptEvent(){
    const modelEventCoordinates: ModelEventCoordinates = {
      eventId: this.event!.id, 
      modelId: this.userId,
      modelEventResponceType: ModelEventResponceType.Accepted
    };
    this.eventService.updateModelResponce(modelEventCoordinates);
  }

  declineEvent(){
    const modelEventCoordinates: ModelEventCoordinates = {
      eventId: this.event!.id, 
      modelId: this.userId,
      modelEventResponceType: ModelEventResponceType.Declined
    };
    this.eventService.updateModelResponce(modelEventCoordinates);
  }

  redirectToAddEventPage(event: Event | undefined) {
    const routeState = {
      event: event,
      currentUser: this.currentUser
    };
    this.router.navigate(['/add-event'], { state: routeState });
  }

  deteleEvent(){
    this.eventService.deletePhoto(this.event!.id).subscribe(() => {
      window.location.reload();
    });;
  }
}

