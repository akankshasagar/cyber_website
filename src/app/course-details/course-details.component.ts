import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../services/course.service';
import { AuthService } from '../services/auth.service';
import { UserstoreService } from '../services/userstore.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
// import * as pdfjsLib from 'pdfjs-dist';

@Component({
  selector: 'app-course-details',
  templateUrl: './course-details.component.html',
  styleUrls: ['./course-details.component.css']
})
export class CourseDetailsComponent {  

  isDropdownOpen = false;
  course: any;
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

    this.checkIfTestSubmitted();
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
        console.error('Error loading modules');
      }
    );
  }

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
    // Ensure all questions have been answered
    if (this.selectedOptions.length !== this.questions.length || this.selectedOptions.some(option => !option)) {
      this.toastr.error('Please answer all the questions before submitting.');
      return;
    }

    // Create a list of answers to be submitted
    const answers: any[] = this.selectedOptions.map((option, index) => {
      const question = this.questions[index];
      return {
        QuestionId: question.id,
        CourseId: this.courseId,
        ModuleId: this.currentModuleId,
        AnswerText: option,
        SubmittedBy: this.email // Assuming the user's email is stored
      };
    });

    // Send the answers to the backend API
    this.http.post( environment.apiURL + "Answers/SubmitAnswers", answers).subscribe({
      next: (response) => {
        this.toastr.success('Answers submitted successfully.');
        this.isSubmitted = true;
        this.goToNextModule();

        // Process the API response to update answersCorrectness
        this.answersCorrectness = this.questions.map((question, index) => {
          return this.selectedOptions[index]?.trim().toLowerCase() === question.correctOption?.trim().toLowerCase();
        });
      },
      error: (err) => {
        console.error('Error submitting answers', err);
        this.toastr.error('There was an error submitting your answers. Please try again.');
      }
    });
  }
  
  goToNextModule(): void {
    // Find the current module and determine the next module
    const currentIndex = this.modules.findIndex(module => module.id === this.currentModuleId);
    if (currentIndex !== -1 && currentIndex < this.modules.length - 1) {
      const nextModule = this.modules[currentIndex + 1];
      this.currentModuleId = nextModule.id; // Update the current module ID
      this.getTopics(nextModule.id); // Optionally, load topics for the next module
      this.isTestPage = false;
    }
  }

  checkIfTestSubmitted() {
    if (!this.currentModuleId) {
      console.error("Current Module ID is not set.");
      return;
    }
  
    this.courseService.checkSubmissionStatus(this.currentModuleId, this.email).subscribe(response => {
      this.isSubmitted = response.isSubmitted;
    });
  }  
    
}

    // this.http.post('https://localhost:7243/api/Answers/SubmitAnswers', answers).subscribe({
