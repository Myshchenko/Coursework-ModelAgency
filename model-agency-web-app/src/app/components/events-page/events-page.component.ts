import { Component } from '@angular/core';
import { EventService } from 'src/app/services/EventService';
import { Event } from 'src/app/models/Event';
import { ModelEvent } from 'src/app/models/ModelEvent';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User';

@Component({
  selector: 'app-events-page',
  templateUrl: './events-page.component.html',
  styleUrls: ['./events-page.component.css'],
})
export class EventsPageComponent {

  events: Event[] | undefined;

  modelEvents: ModelEvent[] | undefined;

  approvedEvents: ModelEvent[] | undefined = [];
  eventsToReview: ModelEvent[] | undefined = [];

  currentUser: User = {} as User;

  userId: number = -1;
  roleId: number = -1;

  filteredFutureEvents: Event[] = [];
  filteredPastEvents: Event[] = [];
  filteredTodayEvents: Event[] = [];

  filterParameter: string = 'All';

  filteredApprovedFutureEvents: Event[] = [];
  filteredApprovedPastEvents: Event[] = [];
  filteredApprovedTodayEvents: Event[] = [];

  filterEventTypeParameter: string = 'All';

  filteredShowEvents: Event[] = [];
  filteredPhotoshootEvents: Event[] = [];

  constructor(private router: Router, private eventService: EventService) {
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras.state) {
      this.currentUser = navigation.extras.state['currentUser'];
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

  loadEvents() {
    if (this.roleId === 2) {
      this.eventService.getEvents().subscribe({
        next: (response) => {
          this.events = response;
        },
      });
    } else if (this.roleId === 1) {
      this.eventService.getModelsEvents(this.userId).subscribe({
        next: (response) => {
          this.modelEvents = response;
          this.setApprovedEvents();
          this.setEventsToReview();
        },
      });
    }
  }

  setApprovedEvents() {
    this.approvedEvents = this.modelEvents!.filter(
      (obj) => obj.acceptingType === 'Accepted'
    );
  }

  setEventsToReview() {
    this.eventsToReview = this.modelEvents!.filter(
      (obj) => obj.acceptingType === 'Not reviewed'
    );
  }

  redirectToAddEventPage() {
    const routeState = {
      currentUser: this.currentUser,
    };
    this.router.navigate(['/add-event'], { state: routeState });
  }

  showFutureEvents() {
    if (this.events && this.roleId === 2) {
      this.filterParameter = 'Future';
      this.filterEventTypeParameter = "All";
      const currentDate = new Date();
      this.filteredFutureEvents = this.events.filter((event) => {
        const targetDate = new Date(event.targetDate);
        return targetDate > currentDate;
      });
    }

    if (this.approvedEvents && this.roleId === 1) {
      this.filterParameter = 'Future';
      this.filterEventTypeParameter = "All";
      const currentDate = new Date();
      this.filteredApprovedFutureEvents = this.approvedEvents.filter((event) => {
        const targetDate = new Date(event.targetDate);
        return targetDate > currentDate;
      });
    }
  }

  showPastEvents() {
    if (this.events && this.roleId === 2) {
      this.filterParameter = 'Past';
      this.filterEventTypeParameter = "All";
      const currentDate = new Date();
      this.filteredPastEvents = this.events.filter((event) => {
        const targetDate = new Date(event.targetDate);
        return targetDate < currentDate;
      });
    }

    if (this.approvedEvents && this.roleId === 1) {
      this.filterParameter = 'Past';
      this.filterEventTypeParameter = "All";
      const currentDate = new Date();
      this.filteredApprovedPastEvents = this.approvedEvents.filter((event) => {
        const targetDate = new Date(event.targetDate);
        return targetDate < currentDate;
      });
    }
  }

  showAllEvents() {
    this.filterParameter = 'All';
    this.filterEventTypeParameter = "All";
  }

  showTodayEvents() {
    if (this.events && this.roleId === 2) {
      this.filterParameter = 'Today';
      this.filterEventTypeParameter = "All";
      const today = new Date().setHours(0, 0, 0, 0);
      this.filteredTodayEvents = this.events.filter((event) => {
        const eventDate = new Date(event.targetDate).setHours(0, 0, 0, 0);
        return eventDate === today;
      });
    }

    if (this.approvedEvents && this.roleId === 1) {
      this.filterParameter = 'Today';
      this.filterEventTypeParameter = "All";
      const today = new Date().setHours(0, 0, 0, 0);
      this.filteredApprovedTodayEvents = this.approvedEvents.filter((event) => {
        const targetDate = new Date(event.targetDate).setHours(0, 0, 0, 0);
        return targetDate === today;
      });
    }
  }

  showShowEvents() {

    console.log("filtering, " + this.events + this.roleId);

    if (this.events && this.roleId === 2) {
      this.filterEventTypeParameter = "Show";
      console.log("filtering for managers");
      this.filterParameter = 'All';
      this.filteredShowEvents = this.events.filter(event => event.eventType === "Показ");
    }
    if (this.approvedEvents && this.roleId === 1) {
      console.log("filtering for models");
      this.filterEventTypeParameter = "Show";
      this.filterParameter = 'All';
      this.filteredShowEvents = this.approvedEvents.filter(event => event.eventType === "Показ");
      console.log(this.filteredShowEvents, "FOunded");
    }
  }

  showPhotoshootEvents() {
    if (this.events && this.roleId === 2) {
      this.filterEventTypeParameter = "Photoshoot";
      this.filterParameter = 'All';
      this.filteredPhotoshootEvents = this.events.filter(event => event.eventType === "Фотосесія");
    }
    if (this.approvedEvents && this.roleId === 1) {
      this.filterEventTypeParameter = "Photoshoot";
      this.filterParameter = 'All';
      this.filteredPhotoshootEvents = this.approvedEvents.filter(event => event.eventType === "Фотосесія");
      console.log(this.filteredPhotoshootEvents, "FOunded");
    }
  }
}
