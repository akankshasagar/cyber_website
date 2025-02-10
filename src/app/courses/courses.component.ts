import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { UserstoreService } from '../services/userstore.service';
import { HttpClient } from '@angular/common/http';
import { CourseService } from '../services/course.service';
import { User } from '../Model/user.model';
import { environment } from 'src/environment/environment';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent {

  start: boolean = false;
  userRole: string | null = null;
  courses: any[] = [];
  selectedCourse: any = null;
  currentPage: number = 1; // Current page number
  coursesPerPage: number = 3; // Number of courses per page
  paginatedCourses: any[] = []; // Courses to display on the current page
  user: User = new User();
  constructor(private auth: AuthService, private http: HttpClient, private courseService: CourseService) { 

  }

  ngOnInit(): void {
    const tokenPayload = this.auth.decodeToken();

    const userEmail = tokenPayload?.email;
    if (!userEmail) {
      console.error('Email Not Found');
      return;
    }

    this.auth.getUserByEmail(userEmail).subscribe({
      next: (user) => {
        this.user.id = user.id; // Assuming the backend returns a user object with an `id` field
      },
      error: (error) => {
        console.error('Failed to fetch UserId:', error);
      },
    });
    this.userRole = tokenPayload?.role || null; // Fetch user role from decoded token
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

  Start(course: any): void {
    this.selectedCourse = course;
    this.start = true; // Display the modal    
    document.body.style.overflow = 'hidden';
  }

  closeCard() {
    this.start = false;
    this.selectedCourse = null;
    document.body.style.overflow = '';
  }


  logout() {
    this.auth.signOut();
  }

  enroll(): void {
    if (!this.user.id || !this.selectedCourse) {
      console.error('User or course information is missing.');
      return;
    }
  
    const enrollmentRequest = {
      UserId: this.user.id,
      CourseId: this.selectedCourse.id,
    };
  
    this.http.post(`${environment.apiUrl}CourseEnrollments/Enroll`, enrollmentRequest).subscribe({
      next: (response: any) => {        
        this.start = false; // Close the modal
      },
      error: (error) => {        
      },
    });
  }
}
