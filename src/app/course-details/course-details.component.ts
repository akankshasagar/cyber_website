import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../services/course.service';
import { AuthService } from '../services/auth.service';
import { UserstoreService } from '../services/userstore.service';
import { ToastrService } from 'ngx-toastr';
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
  isTestPage: boolean = false;
  questions: any[] = [];
  currentModuleId: number | null = null;
  currentModule: any;

  constructor(
    private route: ActivatedRoute,
    private courseService: CourseService,
    private auth: AuthService,
    private userStore: UserstoreService,
    private http: HttpClient,
    private router: Router,
    private toastr: ToastrService
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

  // getTopics(moduleId: number): void {
  //   this.courseService.getTopicsByCourseAndModule(this.courseId, moduleId).subscribe({
  //     next: (data) => {
  //       this.topics = data;
  //       this.showTopics[moduleId] = !this.showTopics[moduleId];
  //       this.currentModuleId = moduleId; // Save the current module ID
  //       if (data && data.length > 0) {
  //         this.selectedTopic = data[0];
  //         if (this.selectedTopic?.t_ImagePath) {
  //           this.selectedTopic.t_ImagePath = this.selectedTopic.t_ImagePath.replace("\\", "/");
  //           this.selectedTopic.imagePath = this.selectedTopic.t_ImagePath;
  //         }
  //       }
  //     },
  //     error: (err) => console.error(err)
  //   });
  // }

  getTopics(moduleId: number): void {
    // Hide topics for all other modules before showing the current module's topics
    for (const key in this.showTopics) {
      if (this.showTopics.hasOwnProperty(key) && Number(key) !== moduleId) {
        this.showTopics[key] = false; // Hide topics for other modules
      }
    }
  
    // Fetch topics for the clicked module
    this.courseService.getTopicsByCourseAndModule(this.courseId, moduleId).subscribe({
      next: (data) => {
        this.topics = data;
        this.showTopics[moduleId] = !this.showTopics[moduleId]; // Toggle visibility for the clicked module
        this.currentModuleId = moduleId; // Save the current module ID
        if (data && data.length > 0) {
          this.selectedTopic = data[0];
          if (this.selectedTopic?.t_ImagePath) {
            this.selectedTopic.t_ImagePath = this.selectedTopic.t_ImagePath.replace("\\", "/");
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
 
  goToTest(moduleId: number): void {
    console.log(`Fetching questions for module ID: ${this.currentModuleId!}`);
  this.getQuestionsForModule(this.currentModuleId!); // Fetch questions for the selected module
  }

  getQuestionsForModule(moduleId: number): void {
    this.courseService.getQuestionsByModule(moduleId).subscribe({
      next: (data) => {
        this.questions = data; // Store the fetched questions
        this.isTestPage = true; // Display the test page
        if (data.length > 0) {
          this.currentModule = data[0].module; // Assuming all questions belong to the same module
        }
      },
      error: (err) => console.error('Error fetching questions:', err)
    });
  }

  selectedOptions: string[] = [];
  answersCorrectness: boolean[] = [];
  isSubmitted: boolean = false;

  
  selectOption(index: number, optionText: string): void {
    if (this.isSubmitted) return;
    
    this.selectedOptions[index] = optionText;
  }

  submitAnswers(): void {
    // Create a list of answers to be submitted
    const answers: any[] = this.selectedOptions.map((option, index) => {
      const question = this.questions[index];
      return {
        QuestionId: question.id,
        CourseId: this.courseId,
        ModuleId: this.currentModuleId,
        AnswerText: option,
        SubmittedBy: this.email // Assuming the user's name is stored
      };
    });
  
    // Send the answers to the backend API
    this.http.post('https://localhost:7243/api/Answers/SubmitAnswers', answers).subscribe({
      next: (response) => {
        this.toastr.success('Answers submitted successfully.');
        console.log('Answers submitted successfully', response);
        // Optionally, you can show success message or navigate
        this.isSubmitted = true;
        // Process the API response to update answersCorrectness
      this.answersCorrectness = this.questions.map((question, index) =>
        this.selectedOptions[index]?.trim().toLowerCase() === question.correctOption?.trim().toLowerCase()
      );
      
      },
      error: (err) => {
        console.error('Error submitting answers', err);
      }
    });
  }
  
  
  // submitAnswers() {
  //   // Log the selected options to the console along with courseId and moduleId
  //   console.log("Selected Answers:", this.selectedOptions);
  //   console.log("Course ID:", this.courseId);
  //   console.log("Module ID:", this.currentModuleId);
    
  //   // Now you can make an API call to submit the answers
  // }
  
  
  
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

  // getTopics(moduleId: number): void {   
  //   this.courseService.getTopicsByCourseAndModule(this.courseId, moduleId)  // Replace 1 with the actual courseId
  //     .subscribe({
  //       next: (data) => {
  //         this.topics = data;
  //         // Toggle the visibility of topics
  //         this.showTopics[moduleId] = !this.showTopics[moduleId];
          
  //         if (data && data.length > 0) {
  //           // Select the first topic in the module
  //           this.selectedTopic = data[0];  // Automatically select the first topic in the module
  //           console.log(this.selectedTopic);  // Log the selected topic for debugging
  
  //           // Ensure the image path is correct and update if needed
  //           if (this.selectedTopic?.t_ImagePath) {
  //             // Normalize the image path if necessary (in case backslashes exist)
  //             this.selectedTopic.t_ImagePath = this.selectedTopic.t_ImagePath.replace("\\", "/");
  //             // Now the imagePath will hold the correct value
  //             this.selectedTopic.imagePath = this.selectedTopic.t_ImagePath;
  //           }
  //         }
  //       },
  //       error: (err) => console.error(err)
  //     });
  // }
}
