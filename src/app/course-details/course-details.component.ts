import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CourseService } from '../services/course.service';
import { AuthService } from '../services/auth.service';
import { UserstoreService } from '../services/userstore.service';
// import * as pdfjsLib from 'pdfjs-dist';

@Component({
  selector: 'app-course-details',
  templateUrl: './course-details.component.html',
  styleUrls: ['./course-details.component.css']
})
export class CourseDetailsComponent {

  // course: any;

  // constructor(private route: ActivatedRoute, private http: HttpClient) {}

  // ngOnInit(): void {
  //   const courseId = this.route.snapshot.paramMap.get('id');
  //   this.http.get(`https://localhost:7243/api/Courses/${courseId}`).subscribe((data: any) => {
  //       this.course = data;
  //   });
  // }

  isDropdownOpen = false;
  course: any;
  // courseId: number = 1; // Set this dynamically based on your course selection
  courseId = Number(this.route.snapshot.paramMap.get('id'));
  modules: any[] = [];
  public fullName: string = "";
  public email: string = "";
  showChapter1Topics: boolean = false;
  topics: any[] = [];  // List of topics for the selected module
  showTopics: { [key: number]: boolean } = {};

  constructor(
    private route: ActivatedRoute,
    private courseService: CourseService,
    private auth: AuthService,
    private userStore: UserstoreService,
    private http: HttpClient
  ) {
  }

  toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
    console.log('Dropdown state:', this.isDropdownOpen);
  }

  toggleChapter1Topics() {
    this.showChapter1Topics = !this.showChapter1Topics; // Toggle visibility
  }
  
  ngOnInit(): void {
    const courseId = this.route.snapshot.paramMap.get('id');
    if (courseId) {
      this.courseService.getCourseById(+courseId).subscribe((data) => {
        this.course = data;
      });
    }
    this.loadModules();

    this.userStore.getFullNameFromStore()
      .subscribe(val => {
        let fullNameFromToken = this.auth.getFullNameFromToken();
        this.fullName = val || fullNameFromToken
      });
    this.userStore.getEmailFromStore()
      .subscribe(val => {
        let emailFromToken = this.auth.getEmailFromToken();
        this.email = val || emailFromToken
      })
  }

  loadModules(): void {
    this.courseService.getModulesByCourse(this.courseId).subscribe(
      (data) => {
        this.modules = data; // Store the modules in an array
      },
      (error) => {
        console.error('Error loading modules:', error);
      }
    );
  }

  // Fetch topics for a specific module
  getTopics(moduleId: number): void {
    if (this.showTopics[moduleId]) {
      // Collapse the list if already open
      this.showTopics[moduleId] = false;
      return;
    }

    this.http.get<any[]>(`https://localhost:7243/api/Topic/${this.courseId}/${moduleId}/Topics`).subscribe(
      (data) => {
        this.topics = data;
        this.showTopics[moduleId] = true; // Expand the list
      },
      (error) => {
        console.error(`Error fetching topics for module ${moduleId}:`, error);
      }
    );
  }

  logout() {
    this.auth.signOut();
  }
 
}
