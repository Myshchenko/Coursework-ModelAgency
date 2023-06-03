import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Model } from 'src/app/models/Model';
import { EventService } from 'src/app/services/EventService';
import { Event } from 'src/app/models/Event';
import { ModelEventCoordinates } from 'src/app/models/ModelEventCoordinates';
import { ModelEventResponceType } from 'src/app/models/ModelEventResponceType';

@Component({
  selector: 'app-add-model-to-event',
  templateUrl: './add-model-to-event.component.html',
  styleUrls: ['./add-model-to-event.component.css']
})
export class AddModelToEventComponent {

  
  model: Model | undefined;

  events: Event[] | undefined;

  selectedEventId: number | undefined;
  
  constructor(private router: Router, private eventService: EventService) {
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras.state) {
      this.model = navigation.extras.state['model'];
    }
  }

  ngOnInit(): void {
    this.loadAvailableEvents();
  }

  loadAvailableEvents(){
    
     this.eventService.getAvailableEventsForSpecificModel(this.model!.id).subscribe({
      next: response => {
        this.events = response;
        console.log(this.events)
      }
    })    
  }

  onSelectEvent(event: any) {
    this.selectedEventId = event.target.value;
    console.log('Selected ID:', this.selectedEventId);
  }

  addModelToTheEvent(){
    const modelEventCoordinates: ModelEventCoordinates = {
      eventId: this.selectedEventId!, 
      modelId: this.model!.id,
      modelEventResponceType: ModelEventResponceType.NotReviewed
    };
    this.eventService.addModelToTheEvent(modelEventCoordinates);
  }

}
