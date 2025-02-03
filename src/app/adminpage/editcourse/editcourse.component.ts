import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';
import { CourseService } from 'src/app/services/course.service';

@Component({
  selector: 'app-editcourse',
  templateUrl: './editcourse.component.html',
  styleUrls: ['./editcourse.component.css']
})
export class EditcourseComponent {

  courseName: string = '';
  courseDescription: string = '';
  imagePath: any;  // To hold the selected file
  courseId: number = 0;
  moduleForm!: FormGroup;
  modules: any[] = [];
  selectedModuleId?: number;
  moduleName: string = '';
  showConfirmDeletePopup: boolean = false;

  constructor(private auth: AuthService,
    private courseService: CourseService,
    private route: ActivatedRoute,
    private http: HttpClient,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) {
    this.moduleForm = this.fb.group({
      moduleName: ['', Validators.required],
      topics: this.fb.array([this.createTopic()]),
      questions: this.fb.array([])
    });
  }

  get topics() {
    return (this.moduleForm.get('topics') as FormArray);
  }

  get questions(): FormArray {
    return this.moduleForm.get('questions') as FormArray;
  }

  createTopic(): FormGroup {
    return this.fb.group({
      topicName: ['', Validators.required],
      topicDescription: ['', Validators.required],
      tImagePath: ['']
    });
  }

  onImageSelected(event: any, index: number) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        // Convert the file to base64 and update the form
        const base64Image = reader.result as string;
        this.topics.at(index).patchValue({
          tImagePath: base64Image,
          imagePreview: base64Image // Store base64 image for preview
        });
      };
      reader.readAsDataURL(file); // Read file as base64 string
    }
  }

  addTopic() {
    this.topics.push(this.createTopic());
  }

  removeTopic(index: number) {
    this.topics.removeAt(index);
  }

  addQuestion(): void {
    const questionGroup = this.fb.group({
      questionText: ['', Validators.required],
      optionA: ['', Validators.required],
      optionB: ['', Validators.required],
      optionC: [''],
      optionD: [''],
      correctOption: ['', Validators.required]
    });
    this.questions.push(questionGroup);
  }

  removeQuestion(index: number): void {
    this.questions.removeAt(index);
  }

  onSubmit() {
    if (this.moduleForm.valid) {
      const requestPayload = this.prepareRequestPayload();
      this.courseService.addModuleToCourse(this.moduleForm.value).subscribe(
        (response) => {
          alert('Module added successfully');
          this.toastr.success(response.message);
          this.moduleForm.reset();
          console.log('Module added successfully', response);
        },
        (error) => {
          alert('Error adding module');
        }
      );
    } else {
      alert('Form is not valid');
    }

    // const questions = this.moduleForm.get('questions').value;
    // if (!questions || questions.length === 0) {
    //     alert('Each module must have at least one question.');
    //     return; // Stop submitting the form if no questions exist
    // }
  }

  private prepareRequestPayload() {
    const formValues = this.moduleForm.value;
    return {
      ModuleName: formValues.moduleName,
      CourseId: formValues.courseId,
      Topics: formValues.topics.map((topic: any) => ({
        TopicName: topic.topicName,
        TopicDescription: topic.topicDescription,
        TImagePath: topic.tImagePath
      }))
    };
  }


  ngOnInit(): void {
    // Get the courseId from the route parameters
    this.route.paramMap.subscribe(params => {
      this.courseId = +params.get('courseId')!;  // Extract courseId from route, and convert it to a number
      console.log('Editing course with ID:', this.courseId);
    });

    if (this.courseId) {
      // Set the courseId in the form without showing it
      this.moduleForm.addControl('courseId', this.fb.control(this.courseId));
    } else {
      // Handle the case when courseId is not present in the URL
      alert('Course ID is required');
    }
    this.fetchModulesByCourse();
  }
  
  onFileSelect(event: any) {
    // this.imagePath = event.target.files[0];  // Get the file object
    const file = event.target.files[0];  // Get the file object

  if (file) {
    const reader = new FileReader();

    reader.onload = (e: any) => {
      const img = new Image();
      img.src = e.target.result;

      img.onload = () => {
        // Check if width is less than height
        if (img.width < img.height) {
          // Display error if width is less than height
          alert('Please upload an image with width greater than height.');
          return;  // Stop further processing if validation fails
        }

        // Desired width and height
        const desiredWidth = 1080;
        const desiredHeight = 675;

        // Get original image dimensions
        const originalWidth = img.width;
        const originalHeight = img.height;

        // Calculate the aspect ratio of the original image
        const aspectRatio = originalWidth / originalHeight;

        // Calculate the new dimensions based on the desired width and aspect ratio
        let newWidth = desiredWidth;
        let newHeight = desiredHeight;

        // Adjust the new dimensions to maintain the aspect ratio
        if (originalWidth > originalHeight) {
          // If the original image is wider, scale based on width
          newHeight = newWidth / aspectRatio;
        } else if (originalWidth < originalHeight) {
          // If the original image is taller, scale based on height
          newWidth = newHeight * aspectRatio;
        } else {
          // If it's a square, no change needed other than setting the desired size
          newWidth = desiredWidth;
          newHeight = desiredHeight;
        }

        // Create a canvas to resize the image
        const canvas = document.createElement('canvas');
        const ctx = canvas.getContext('2d');

        // Set canvas dimensions to the calculated new width and height
        canvas.width = newWidth;
        canvas.height = newHeight;

        // Draw the image on the canvas with the new dimensions
        ctx?.drawImage(img, 0, 0, newWidth, newHeight);

        // Convert the canvas to a data URL (base64-encoded image)
        canvas.toBlob((blob) => {
          if (blob) {
            // Convert blob to file
            const resizedFile = new File([blob], file.name, { type: file.type });

            // Now, append the resized image file to FormData
            this.imagePath = resizedFile;  // Save the resized image file
          }
        }, file.type);
      };
    };

    // Read the image as a data URL
    reader.readAsDataURL(file);
  }
  }

  saveChanges(form: any): void {
    const formData = new FormData();
    formData.append('courseName', this.courseName);
    formData.append('courseDescription', this.courseDescription);
    if (this.imagePath) {
      formData.append('imagePath', this.imagePath, this.imagePath.name);  // Append the image file
    }

    // const courseId = 1;  // Example courseId, replace with actual course ID

    this.http.put(`https://localhost:7243/api/Course/EditCourse/${this.courseId}`, formData)
      .subscribe({
        next: (response) => {
          this.toastr.success("Course Details updated successfully");
          console.log('Course updated successfully:', response);

          form.reset();
        },
        error: (error) => {
          console.error('Error updating course:', error);
        }
      });
  }  

  fetchModulesByCourse(): void {
    this.http.get<any[]>(`https://localhost:7243/api/Module/${this.courseId}`)
      .subscribe(
        (data) => {
          this.modules = data;
        },
        (error) => {
          console.error('Error fetching modules:', error);
        }
      );
  }

  onModuleSelect(event: any): void {
    this.selectedModuleId = event.target.value;
    const selectedModule = this.modules.find(module => module.id === +event.target.value);
    if (selectedModule) {
      this.moduleName = selectedModule.module_Name; // Set the module name here
      this.selectedModuleId = selectedModule.id; // Also set the selected module ID
    } else {
      this.moduleName = ''; // Clear the name if no module is selected
      this.selectedModuleId = undefined; // Clear the selected ID
    }
  }

  onSubmitM(form: any): void {
    if (!this.selectedModuleId || !this.moduleName) {
      alert('Please select a module and enter a new module name.');
      return;
    }

    const requestPayload = {
      moduleId: this.selectedModuleId,
      moduleName: this.moduleName
    };

    this.http.put('https://localhost:7243/api/Module/EditModuleDetails', requestPayload)
      .subscribe(
        (response) => {
          alert('Module details updated successfully!');
          form.reset();
        },
        (error) => {
          console.error('Error updating module:', error);
          alert('Error updating module details.');
        }
      );
  }

  confirmDelete(): void {
    this.showConfirmDeletePopup = true;
  }

  closeConfirmDeletePopup(): void {
    this.showConfirmDeletePopup = false;
  }

  deleteModule(): void {
    if (this.selectedModuleId !== undefined && this.selectedModuleId !== null) {
    this.courseService.deleteModule(this.courseId, this.selectedModuleId).subscribe(
      (response) => {
        // Handle successful deletion (show success message or refresh data)
        console.log('Module deleted successfully', response);
        this.closeConfirmDeletePopup(); // Close the popup
        window.location.reload();
        // Optionally, you can also refresh the module list here
      },
      (error) => {
        // Handle error (show error message)
        console.error('Error deleting module', error);
      }
    );
  }else {
    console.error('Module ID is not selected or invalid.');
  }
}

  logout() {
    this.auth.signOut();
  }
}
