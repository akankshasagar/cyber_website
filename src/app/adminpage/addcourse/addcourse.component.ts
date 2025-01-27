import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';
import { CourseService } from 'src/app/services/course.service';

@Component({
  selector: 'app-addcourse',
  templateUrl: './addcourse.component.html',
  styleUrls: ['./addcourse.component.css']
})
export class AddcourseComponent {

  userRole: string | null = null;
  // courseForm: FormGroup;
  // selectedImage: File | null = null;
  // selectedFile: File | null = null;
  // imageError: string | null = null;

  // constructor(private auth: AuthService, private http: HttpClient, private fb: FormBuilder, private courseService: CourseService, private toastr: ToastrService) { 
  //   this.courseForm = this.fb.group({
  //     courseName: [''],
  //     courseDescription: [''],
  //   });
  // }

  // onFileSelect(event: Event, type: string): void {
  //   const file = (event.target as HTMLInputElement).files![0];
    
  //   if (type === 'image') {
  //     this.validateImageDimensions(file).then(isValid => {
  //       if (isValid) {
  //         this.selectedImage = file;
  //         this.imageError = null;
  //       } else {
  //         this.selectedImage = null;
  //         this.imageError = 'Please upload a horizontal image (width > height).';
  //       }
  //     });
  //   } else if (type === 'file') {
  //     this.selectedFile = file;
  //   }
  // }

  // validateImageDimensions(file: File): Promise<boolean> {
  //   return new Promise((resolve) => {
  //     const img = new Image();
  //     img.src = URL.createObjectURL(file);
  //     img.onload = () => {
  //       const isHorizontal = img.width > img.height;
  //       resolve(isHorizontal);
  //     };
  //     img.onerror = () => resolve(false);
  //   });
  // }

  // onSubmit(): void {

  //   if (this.imageError) {
  //     this.toastr.error('Please resolve the image error before submitting.');
  //     return;
  //   }
  //   const formData = new FormData();
  // formData.append('Name', this.courseForm.value.courseName);
  // formData.append('Description', this.courseForm.value.courseDescription);
  // if (this.selectedImage) formData.append('Image', this.selectedImage);
  // if (this.selectedFile) formData.append('File', this.selectedFile);

  // this.courseService.addCourse(formData).subscribe({
  //   next: (response) => {
  //     this.toastr.success(response.message);
  //     // alert('Course added successfully!');
  //     console.log('Response:', response);

  //     this.courseForm.reset();
  //     this.selectedImage = null;
  //     this.selectedFile = null;
  //     this.imageError = null;

  //     // Manually reset the file inputs
  //     (document.getElementById('imageInput') as HTMLInputElement).value = '';
  //     (document.getElementById('fileInput') as HTMLInputElement).value = '';
  //   },
  //   error: (error) => {
  //     this.toastr.error(error?.error.message);
  //     // alert('Failed to add course.');
  //     console.error('Error:', error);
  //   },
  // });
  // }

  // ngOnInit(): void {
  //   const tokenPayload = this.auth.decodeToken();
  //   this.userRole = tokenPayload?.role || null; // Fetch user role from decoded token
  //   this.courseForm = this.fb.group({
  //     courseName: ['', Validators.required],
  //     courseDescription: ['', Validators.required],
  //     image: [null],
  //     modules: this.fb.array([])
  //   });
  // }

  logout() {
    this.auth.signOut();
  }


  // courseForm: FormGroup;
  // imageError: string = '';
  // apiUrl: string = 'https://localhost:7243/api/Course/AddCourseWithModulesAndTopics';

  // constructor(private fb: FormBuilder, private http: HttpClient, private auth: AuthService, private toastr: ToastrService) {
  //   this.courseForm = this.fb.group({
  //     courseName: ['', Validators.required],
  //     courseDescription: ['', Validators.required],
  //     image: [null, Validators.required],
  //     modules: this.fb.array([])
  //   });
  // }

  // get modules(): FormArray {
  //   return this.courseForm.get('modules') as FormArray;
  // }

  // getTopics(moduleIndex: number): FormArray {
  //   return this.modules.at(moduleIndex).get('topics') as FormArray;
  // }

  // addModule(): void {
  //   this.modules.push(
  //     this.fb.group({
  //       moduleName: ['', Validators.required],
  //       topics: this.fb.array([])
  //     })
  //   );
  // }

  // removeModule(moduleIndex: number): void {
  //   this.modules.removeAt(moduleIndex);
  // }

  // addTopic(moduleIndex: number): void {
  //   this.getTopics(moduleIndex).push(
  //     this.fb.group({
  //       topicName: ['', Validators.required],
  //       topicDescription: ['', Validators.required],
  //       topicImage: [null]
  //     })
  //   );
  // }

  // removeTopic(moduleIndex: number, topicIndex: number): void {
  //   this.getTopics(moduleIndex).removeAt(topicIndex);
  // }

  // onFileSelect(event: any, type: string, moduleIndex?: number, topicIndex?: number): void {
  //   const file = event.target.files[0];    

  //   // if (file) {
  //   //   const reader = new FileReader();
  //   //   reader.onload = () => {
  //   //     if (type === 'image') {
  //   //       this.courseForm.patchValue({ image: reader.result });
  //   //     } else if (type === 'topic' && moduleIndex !== undefined && topicIndex !== undefined) {
  //   //       const topics = this.getTopics(moduleIndex);
  //   //       topics.at(topicIndex).patchValue({ topicImage: reader.result });
  //   //     }
  //   //   };
  //   //   reader.readAsDataURL(file);
  //   // }

  //   if (file) {
  //     const reader = new FileReader();
  //     reader.onload = () => {
  //       if (type === 'image') {
  //         this.courseForm.patchValue({ image: reader.result });
  //         this.courseForm.get('image')?.setErrors(null); // Clear any previous errors
  //       } else if (type === 'topic' && moduleIndex !== undefined && topicIndex !== undefined) {
  //         const topics = this.getTopics(moduleIndex);
  //         topics.at(topicIndex).patchValue({ topicImage: reader.result });
  //       }
  //     };
  //     reader.readAsDataURL(file);
  //   } else {
  //     this.courseForm.get('image')?.setErrors({ required: true }); // Set error if no file is selected
  //   }
  // }

  // onSubmit(): void {
  //   if (this.courseForm.invalid) {
  //     // Mark all controls as touched to show validation errors
  //     this.courseForm.markAllAsTouched();
  //     this.toastr.error('Please fill all required fields', 'Validation Error');
  //     return;
  //   }
  //   if (this.courseForm.valid) {
  //     const formData = this.courseForm.value;

  //     // Transform Form Data for API
  //     const payload = {
  //       CourseName: formData.courseName,
  //       CourseDescription: formData.courseDescription,
  //       ImagePath: formData.image, // Base64 encoded image
  //       Modules: formData.modules.map((module: any) => ({
  //         ModuleName: module.moduleName,
  //         Topics: module.topics.map((topic: any) => ({
  //           TopicName: topic.topicName,
  //           TopicDescription: topic.topicDescription,
  //           TImagePath: topic.topicImage // Base64 encoded image
  //         }))
  //       }))
  //     };
      
  //     // API call
  //     this.http.post(this.apiUrl, payload).subscribe({
  //       next: (response) => {
  //         this.toastr.success('Course added successfully!', 'Success');
  //         this.resetForm();
  //         // alert('Course added successfully!');
  //         // console.log(response);
  //       },
  //       error: (error) => {
  //         // alert('Failed to add course.');  
  //         // this.toastr.error('Failed to add course.', 'Error');    
  //         this.toastr.error(error?.error.message);              
  //         // console.error(error);
  //       }
  //     });
  //   }    
  // }

  // private resetForm(): void {
  //   this.courseForm.reset(); // Reset the form controls
  //   while (this.modules.length) {
  //     this.modules.removeAt(0); // Clear all modules and topics
  //   }
  //   this.imageError = ''; // Clear any image error messages
  // } 



  courseForm!: FormGroup;
  imageError: string = '';
  apiUrl: string = 'https://localhost:7243/api/Course/AddCourseWithModulesAndTopics';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private toastr: ToastrService,
    private auth: AuthService
  ) {}

  ngOnInit(): void {
    const tokenPayload = this.auth.decodeToken();
    this.userRole = tokenPayload?.role || null;
    this.courseForm = this.fb.group({
      courseName: ['', Validators.required],
      courseDescription: ['', Validators.required],
      image: [null, Validators.required],
      modules: this.fb.array([]),
    });
  }

  get modules(): FormArray {
    return this.courseForm.get('modules') as FormArray;
  }

  getTopics(moduleIndex: number): FormArray {
    return this.modules.at(moduleIndex).get('topics') as FormArray;
  }

  getQuestions(moduleIndex: number): FormArray {
    return this.modules.at(moduleIndex).get('questions') as FormArray;
  }

  addModule(): void {
    this.modules.push(
      this.fb.group({
        moduleName: ['', Validators.required],
        topics: this.fb.array([]),
        questions: this.fb.array([]),
      })
    );
  }

  removeModule(index: number): void {
    this.modules.removeAt(index);
  }

  addTopic(moduleIndex: number): void {
    this.getTopics(moduleIndex).push(
      this.fb.group({
        topicName: ['', Validators.required],
        topicDescription: ['', Validators.required],
        topicImage: [null],
      })
    );
  }

  removeTopic(moduleIndex: number, topicIndex: number): void {
    this.getTopics(moduleIndex).removeAt(topicIndex);
  }

  addQuestion(moduleIndex: number): void {
    this.getQuestions(moduleIndex).push(
      this.fb.group({
        questionText: ['', Validators.required],
        optionA: ['', Validators.required],
        optionB: ['', Validators.required],
        optionC: [''],
        optionD: [''],
        correctOption: ['', Validators.required],
      })
    );
  }

  removeQuestion(moduleIndex: number, questionIndex: number): void {
    this.getQuestions(moduleIndex).removeAt(questionIndex);
  }

  onFileSelect(event: any, type: string, moduleIndex?: number, topicIndex?: number): void {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        if (type === 'image') {
          this.courseForm.patchValue({ image: reader.result });
          this.courseForm.get('image')?.setErrors(null);
        } else if (type === 'topic' && moduleIndex !== undefined && topicIndex !== undefined) {
          this.getTopics(moduleIndex).at(topicIndex).patchValue({ topicImage: reader.result });
        }
      };
      reader.readAsDataURL(file);
    } else if (type === 'image') {
      this.courseForm.get('image')?.setErrors({ required: true });
    }
  }

  removeTopicImage(moduleIndex: number, topicIndex: number): void {
    // Get the topic form group
    const topicsArray = this.getTopics(moduleIndex);
    // Check if topicsArray is valid and contains the topic
    if (topicsArray) {
      const topicGroup = topicsArray.at(topicIndex);

      if (topicGroup) {
        // Set the value of the form control to null
        topicGroup.get('topicImage')?.setValue(null);

        // Clear the file input element (optional)
        const fileInput = document.getElementById(`topicImage${moduleIndex}${topicIndex}`) as HTMLInputElement | null;
        if (fileInput) {
          fileInput.value = ''; // Clear the file input value, removing the filename
        }
      }
    }
  }

  onSubmit(): void {
    if (this.courseForm.invalid) {
      this.courseForm.markAllAsTouched();
      this.toastr.error('Please fill all required fields', 'Validation Error');
      return;
    }

    const formData = this.courseForm.value;

    const payload = {
      CourseName: formData.courseName,
      CourseDescription: formData.courseDescription,
      ImagePath: formData.image,
      Modules: formData.modules.map((module: any) => ({
        ModuleName: module.moduleName,
        Topics: module.topics.map((topic: any) => ({
          TopicName: topic.topicName,
          TopicDescription: topic.topicDescription,
          TImagePath: topic.topicImage,
        })),
        Questions: module.questions.map((question: any) => ({
          QuestionText: question.questionText,
          OptionA: question.optionA,
          OptionB: question.optionB,
          OptionC: question.optionC,
          OptionD: question.optionD,
          CorrectOption: question.correctOption,
        })),
      })),
    };

    this.http.post(this.apiUrl, payload).subscribe({
      next: () => {
        this.toastr.success('Course added successfully!', 'Success');
        this.resetForm();
      },
      error: (err) => {
        this.toastr.error(err?.error?.message || 'Failed to add course.', 'Error');
      },
    });
  }

  private resetForm(): void {
    this.courseForm.reset();
    while (this.modules.length) {
      this.modules.removeAt(0);
    }
  }
  
}
