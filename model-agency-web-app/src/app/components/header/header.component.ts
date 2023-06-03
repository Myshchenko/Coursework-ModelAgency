import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/UserService';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  user: User | null = {} as User;

  userId: number = -1;
  roleId: number = -1;

  constructor(private router: Router, private userService: UserService){
    
    const role_id = localStorage.getItem('roleId');

    if (role_id !== null) {
      this.roleId = parseInt(role_id);
    }

    const user_id = localStorage.getItem('userId');

    if (user_id !== null) {
      this.userId = parseInt(user_id);
    }
  }

  login() {
    console.log(this.user);
    this.userService.login(this.user!).subscribe(
      (response: any) => {
        this.user = response as User;

        console.log(this.user);
        if(this.user.id){
          this.userId = this.user.id;
          localStorage.setItem('userId', this.user.id.toString());
        }
        if(this.user.roleId){
          this.roleId = this.user.roleId;
          localStorage.setItem('roleId', this.user.roleId.toString());
        }
        
      },
      (error: any) => {
        console.error(error);
      }
    );
  }

  redirectToEventPage(){
    this.router.navigate(['/events'], { state: { currentUser: this.user } });
  }

  redirectToModelPage(){
    this.router.navigate(['/models'], { state: { currentUser: this.user } });
  }

  logout(){
    this.user = null;
    localStorage.setItem('userId', "-1");
    localStorage.setItem('roleId', "-1");
    
    this.router.navigate(['']).then( () => window.location.reload());
  }
}
