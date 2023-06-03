import { Component } from '@angular/core';
import { EventService } from 'src/app/services/EventService';
import { Event } from 'src/app/models/Event';
import { ModelEvent } from 'src/app/models/ModelEvent';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User';

@Component({
  selector: 'app-events-page',
  templateUrl: './events-page.component.html',
  styleUrls: ['./events-page.component.css']
})
export class EventsPageComponent {
  title:string;
  myHero:string;
  heroes: any[];

  events: Event[] | undefined;

  modelEvents: ModelEvent[] | undefined;

  approvedEvents: ModelEvent[] | undefined = [];
  eventsToReview: ModelEvent[] | undefined = [];

  currentUser: User = {} as User;

  userId: number = -1;
  roleId: number = -1;

  constructor(private router: Router, private eventService: EventService) {
     this.title = 'Tour of Heros';
     this.heroes=['Windstorm','Bombasto','Magneta', 'Windstorm','Bombasto','Magneta']
     this.myHero = this.heroes[0];

     const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras.state) {
      this.currentUser = navigation.extras.state['currentUser'];
      console.log(navigation.extras.state['currentUser'], "EVENTS COMPONENT");
    }

    const role_id = localStorage.getItem('roleId');

    if (role_id !== null) {
      this.roleId = parseInt(role_id);
    }

    const user_id = localStorage.getItem('userId');

    if (user_id !== null) {
      this.userId = parseInt(user_id);
    }
  }

  ngOnInit(): void {
    this.loadEvents();
  }

  loadEvents(){
    if(this.roleId === 2) {
     this.eventService.getEvents().subscribe({
      next: response => {
        this.events = response
      }
    })     
    } else if(this.roleId === 1){
        this.eventService.getModelsEvents(this.userId).subscribe({
          next: response => {
            this.modelEvents = response;
            this.setApprovedEvents();
            this.setEventsToReview();
       }
      });
    }

  }

  setApprovedEvents(){
    this.approvedEvents = this.modelEvents!.filter(obj => obj.acceptingType === "Accepted");
  }

  setEventsToReview(){
    this.eventsToReview = this.modelEvents!.filter(obj => obj.acceptingType === "Not reviewed");
  }

  redirectToAddEventPage() {
    const routeState = {
      currentUser: this.currentUser
    };
    this.router.navigate(['/add-event'], { state: routeState });
  }

}