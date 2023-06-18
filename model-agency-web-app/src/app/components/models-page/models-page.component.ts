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

  filterParameter: string = 'All';

  women: Model[] = [];
  men: Model[] = [];

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

  showMen() {
    if (this.models) {
      this.filterParameter = "Men";
      this.men = this.models.filter(model => model.genderId === 1);
    }
  }

  showWomen() {
    if (this.models) {
      this.filterParameter = "Women";
      this.women = this.models.filter(model => model.genderId === 2);
    }
  }

  showAll() {
    this.filterParameter = "All";
  }
}
