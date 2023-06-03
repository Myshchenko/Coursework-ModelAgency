import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Model } from 'src/app/models/Model';
import { User } from 'src/app/models/User';

@Component({
  selector: 'app-model-card',
  templateUrl: './model-card.component.html',
  styleUrls: ['./model-card.component.css']
})
export class ModelCardComponent {

  @Input() model: Model | undefined;
  @Input() currentUser: User = {} as User;

  roleId: number = -1;
  userId: number = -1;

  constructor(private router: Router){
    const role_id = localStorage.getItem('roleId');

    if (role_id !== null) {
      this.roleId = parseInt(role_id);
    }

    const user_id = localStorage.getItem('userId');

    if (user_id !== null) {
      this.userId = parseInt(user_id);
    }
  }

  redirectToEventPage(model: Model | undefined) {
  this.router.navigate(['/add-model-to-event'], { state: { model: model } });
}
}
