import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { CourseService } from '../services/course.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-allcourses',
  templateUrl: './allcourses.component.html',
  styleUrls: ['./allcourses.component.css']
})
export class AllcoursesComponent {

  start: boolean = false;
  courses: any[] = [];
  selectedCourse: any = null;
  currentPage: number = 1; // Current page number
  coursesPerPage: number = 3; // Number of courses per page
  paginatedCourses: any[] = []; // Courses to display on the current page

  constructor(private auth: AuthService, private http: HttpClient, private courseService: CourseService, private toastr: ToastrService) { 

  }

  ngOnInit(): void {    
    this.courseService.getCourses().subscribe({
      next: (data) => {
        this.courses = data;
        this.updatePaginatedCourses();
      },
      error: (error) => {
        this.toastr.error("Error Fetching Courses");      
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
    this.auth.enroll(email, course)
      .subscribe({
        next: (response)  => {
          // Handle success (e.g., show a success message)
        },
        error: (error) => {
          // Handle error (e.g., show an error message)
        }
      });
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
}
