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
  selectedTopic: any;

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
    this.loadDefaultTopic();

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

  loadDefaultTopic(): void {
    this.courseService.getTopicsByCourse(this.courseId) // Replace 1 with the actual courseId
      .subscribe({
        next: (data) => {
          if (data && data.length > 0) {
            this.topics = data;
            this.selectedTopic = data[0]; // Automatically set the first topic as the selected topic
          }
        },
        error: (err) => console.error(err)
      });
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
    this.courseService.getTopicsByCourseAndModule(this.courseId, moduleId) // Replace 1 with the actual courseId
      .subscribe({
        next: (data) => {
          this.topics = data;
          this.showTopics[moduleId] = true;
          if (data && data.length > 0) {
            this.selectedTopic = data[0]; // Automatically select the first topic in the module
          }
        },
        error: (err) => console.error(err)
      });
  }

  viewTopicDetails(topicId: number): void {
    this.courseService.getTopicById(topicId).subscribe({
      next: (data) => {
        this.selectedTopic = data;
      },
      error: (err) => console.error(err)
    });
  }

  logout() {
    this.auth.signOut();
  }
 
}
