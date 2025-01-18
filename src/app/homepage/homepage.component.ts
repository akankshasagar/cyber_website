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
  currentPage: number = 1; // Current page number
  coursesPerPage: number = 3; // Number of courses per page
  paginatedCourses: any[] = []; // Courses to display on the current page

  constructor(private auth: AuthService, private http: HttpClient, private courseService: CourseService) { 

  }

  ngOnInit(): void {    
    this.courseService.getCourses().subscribe({
      next: (data) => {
        this.courses = data;
        this.updatePaginatedCourses();
      },
      error: (error) => {
        console.error('Failed to fetch courses:', error);
      },
    });
  }

  updatePaginatedCourses(): void {
    const startIndex = (this.currentPage - 1) * this.coursesPerPage;
    const endIndex = startIndex + this.coursesPerPage;
    this.paginatedCourses = this.courses.slice(startIndex, endIndex);
  }

  goToPage(page: number): void {
    this.currentPage = page;
    this.updatePaginatedCourses();
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages()) {
      this.currentPage++;
      this.updatePaginatedCourses();
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePaginatedCourses();
    }
  }

  totalPages(): number {
    return Math.ceil(this.courses.length / this.coursesPerPage);
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
    console.log('Selected course:', this.selectedCourse);
    // console.log(course);
    // console.log(this.selectedCourse);
    document.body.style.overflow = 'hidden';
  }

  closeCard() {
    this.start = false;
    this.selectedCourse = null;
    document.body.style.overflow = '';
  }
  
}
