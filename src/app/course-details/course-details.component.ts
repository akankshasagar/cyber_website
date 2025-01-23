import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
    private http: HttpClient,
    private router: Router
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
    const courseName = this.route.snapshot.paramMap.get('courseName') || '';
    if (courseId) {
      this.courseService.getCourseById(+courseId, courseName).subscribe((data) => {
        this.course = data;
        this.loadModulesAndTriggerFirstModule();
      });
    }
    const topicId = this.route.snapshot.paramMap.get('id');
    if (topicId) {
        this.courseService.getTopicById(+topicId).subscribe((data) => {
            this.selectedTopic = data;
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
  // getTopics(moduleId: number): void {   
  //   this.courseService.getTopicsByCourseAndModule(this.courseId, moduleId) // Replace 1 with the actual courseId
  //     .subscribe({
  //       next: (data) => {
  //         this.topics = data;
  //         // this.showTopics[moduleId] = true;
  //         this.showTopics[moduleId] = !this.showTopics[moduleId];
  //         if (data && data.length > 0) {
  //           this.selectedTopic = data[0]; // Automatically select the first topic in the module
  //           console.log(data[0]);
  //         }
  //       },
  //       error: (err) => console.error(err)
  //     });
  // }

  getTopics(moduleId: number): void {   
    this.courseService.getTopicsByCourseAndModule(this.courseId, moduleId)  // Replace 1 with the actual courseId
      .subscribe({
        next: (data) => {
          this.topics = data;
          // Toggle the visibility of topics
          this.showTopics[moduleId] = !this.showTopics[moduleId];
          
          if (data && data.length > 0) {
            // Select the first topic in the module
            this.selectedTopic = data[0];  // Automatically select the first topic in the module
            console.log(this.selectedTopic);  // Log the selected topic for debugging
  
            // Ensure the image path is correct and update if needed
            if (this.selectedTopic?.t_ImagePath) {
              // Normalize the image path if necessary (in case backslashes exist)
              this.selectedTopic.t_ImagePath = this.selectedTopic.t_ImagePath.replace("\\", "/");
              // Now the imagePath will hold the correct value
              this.selectedTopic.imagePath = this.selectedTopic.t_ImagePath;
            }
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

  goToNextTopic(): void {
    if (this.selectedTopic) {
      const currentIndex = this.topics.findIndex(topic => topic.id === this.selectedTopic.id);
      const nextIndex = (currentIndex + 1) % this.topics.length; // Loops back to the first topic if the last one is reached
      const nextTopic = this.topics[nextIndex]; // Get the next topic
      
      // Call viewTopicDetails to simulate clicking on the next topic
      this.viewTopicDetails(nextTopic.id);
    }
  }

  goToTest(): void {
    // You can implement your test redirection logic here
    console.log('Redirecting to the test page...');
    // Example: Navigate to a test page
    // this.router.navigate(['/test']);
  }

  isLastTopic(): boolean {
    if (this.selectedTopic) {
      const currentIndex = this.topics.findIndex(topic => topic.id === this.selectedTopic.id);
      return currentIndex === this.topics.length - 1; // Check if it's the last topic
    }
    return false;
  }
  
  

  logout() {
    this.auth.signOut();
  }

  loadModulesAndTriggerFirstModule(): void {
    this.courseService.getModulesByCourse(this.courseId).subscribe({
      next: (modules) => {
        this.modules = modules;
  
        if (modules?.length > 0) {
          const firstModule = modules[0]; // Get the first module
  
          // Simulate clicking the first module
          this.getTopics(firstModule.id);
        }
      },
      error: (err) => console.error('Error loading modules:', err),
    });
  }  
 
}
