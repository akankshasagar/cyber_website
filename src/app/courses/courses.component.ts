import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { UserstoreService } from '../services/userstore.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent {

  start: boolean = false;
  userRole: string | null = null;
  constructor(private auth: AuthService, private http: HttpClient) { 

  }

  ngOnInit(): void {
    const tokenPayload = this.auth.decodeToken();
    this.userRole = tokenPayload?.role || null; // Fetch user role from decoded token
  }

  enroll(email: string, course: string) {
    this.auth.enroll(email, course)
      .subscribe({
        next: (response)  => {
          console.log('Enrollment successful', response);
        },
        error: (error) => {
          console.error('Error occurred during enrollment', error);
        }
      });
  }

  Start(){
    this.start = true;
  }

  closeCard() {
    this.start = false;
  }


  logout() {
    this.auth.signOut();
  }
}
