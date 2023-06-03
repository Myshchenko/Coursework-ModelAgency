import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Model } from 'src/app/models/Model';
import { User } from 'src/app/models/User';
import { ModelService } from 'src/app/services/ModelService';

@Component({
  selector: 'app-models-page',
  templateUrl: './models-page.component.html',
  styleUrls: ['./models-page.component.css'],
})
export class ModelsPageComponent {
  models: Model[] | undefined;

  currentUser: User = {} as User;

  constructor(private router: Router, private modelService: ModelService) {
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras.state) {
      this.currentUser = navigation.extras.state['currentUser'];
    }
  }

  ngOnInit(): void {
    this.loadModels();
  }

  loadModels() {
    this.modelService.getModels().subscribe({
      next: (response) => {
        this.models = response;
        console.log(this.models);
      },
    });
  }

}
