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

  ngOnInit(): void {
    const tokenPayload = this.auth.decodeToken();
    this.userRole = tokenPayload?.role || null; // Fetch user role from decoded token
  }

  logout() {
    this.auth.signOut();
  }


  courseForm: FormGroup;
  imageError: string | null = null;

  constructor(private fb: FormBuilder, private courseService: CourseService, private auth: AuthService) {
    this.courseForm = this.fb.group({
      courseName: ['', Validators.required],
      courseDescription: ['', Validators.required],
      image: [null, Validators.required],
      modules: this.fb.array([])
    });
  }

  get modules(): FormArray {
    return this.courseForm.get('modules') as FormArray;
  }

  addModule(): void {
    this.modules.push(
      this.fb.group({
        moduleName: ['', Validators.required],
        topics: this.fb.array([])
      })
    );
  }

  getTopics(moduleIndex: number): FormArray {
    return this.modules.at(moduleIndex).get('topics') as FormArray;
  }

  addTopic(moduleIndex: number): void {
    this.getTopics(moduleIndex).push(
      this.fb.group({
        topicName: ['', Validators.required],
        topicDescription: ['', Validators.required],
        tImagePath: [null, Validators.required]
      })
    );
  }

  onFileSelect(event: Event, field: string, moduleIndex?: number, topicIndex?: number): void {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      if (field === 'image') {
        this.courseForm.patchValue({ image: file });
      } else if (field === 'topic' && moduleIndex !== undefined && topicIndex !== undefined) {
        this.getTopics(moduleIndex).at(topicIndex).patchValue({ tImagePath: file });
      }
    }
  }

  onSubmit(): void {
    if (this.courseForm.valid) {
      const payload = this.buildPayload();
      console.log('Payload sent to API:', payload); // Debugging log
  
      this.courseService.addCourse2(payload).subscribe({
        next: (response) => {
          alert('Course added successfully!');
          console.log('Response:', response);
        },
        error: (error) => {
          alert('Failed to add course.');
          console.error('Error details:', error);
        }
      });
      
    } else {
      alert('Please fill out all required fields.');
    }
  }    

  private buildPayload(): any {
    const formValue = this.courseForm.value;
  
    const modules = formValue.modules.map((module: any) => ({
      ModuleName: module.moduleName,
      Topics: module.topics.map((topic: any) => ({
        TopicName: topic.topicName,
        TopicDescription: topic.topicDescription,
        TImagePath: topic.tImagePath
      }))
    }));
  
    return {
      CourseName: formValue.courseName,
      CourseDescription: formValue.courseDescription,
      ImagePath: formValue.image, // Ensure this is a string path or base64 if backend requires it
      Modules: modules
    };
  }
  
}
