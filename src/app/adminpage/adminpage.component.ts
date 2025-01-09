import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { CourseService } from '../services/course.service';

@Component({
  selector: 'app-adminpage',
  templateUrl: './adminpage.component.html',
  styleUrls: ['./adminpage.component.css']
})
export class AdminpageComponent {

  start: boolean = false;
  userRole: string | null = null;
  courses: any[] = [];

  constructor(private auth: AuthService, private http: HttpClient, private courseService: CourseService) { 
  
  }
  
  ngOnInit(): void {
    const tokenPayload = this.auth.decodeToken();
    this.userRole = tokenPayload?.role || null; // Fetch user role from decoded token
    this.courseService.getCourses().subscribe({
      next: (data) => {
        this.courses = data;
      },
      error: (error) => {
        console.error('Failed to fetch courses:', error);
      },
    });
  }
  
  logout() {
    this.auth.signOut();
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

}
