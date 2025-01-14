import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { CourseService } from '../services/course.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent {

  start: boolean = false;
  courses: any[] = [];
  selectedCourse: any = null;

  constructor(private auth: AuthService, private http: HttpClient, private courseService: CourseService) { 

  }

  ngOnInit(): void {    
    this.courseService.getCourses().subscribe({
      next: (data) => {
        this.courses = data;
      },
      error: (error) => {
        console.error('Failed to fetch courses:', error);
      },
    });
  }

  enroll(email: string, course: string) {
    // this.http.post<any>('https://localhost:7243/api/CourseEnrollments', { course })
    this.auth.enroll(email, course)
      .subscribe({
        next: (response)  => {
          console.log('Enrollment successful', response);
          // Handle success (e.g., show a success message)
        },
        error: (error) => {
          console.error('Error occurred during enrollment', error);
          // Handle error (e.g., show an error message)
        }
      });
  }


  Start(course: any): void {
    this.selectedCourse = course;
    this.start = true; // Display the modal
    console.log(course);
    console.log(this.selectedCourse);
    document.body.style.overflow = 'hidden';
  }

  closeCard() {
    this.start = false;
    this.selectedCourse = null;
    document.body.style.overflow = '';
  }
  
}
