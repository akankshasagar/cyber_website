import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
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
  courseForm: FormGroup;
  selectedImage: File | null = null;
  selectedFile: File | null = null;
  imageError: string | null = null;

  constructor(private auth: AuthService, private http: HttpClient, private fb: FormBuilder, private courseService: CourseService, private toastr: ToastrService) { 
    this.courseForm = this.fb.group({
      courseName: [''],
      courseDescription: [''],
    });
  }

  onFileSelect(event: Event, type: string): void {
    const file = (event.target as HTMLInputElement).files![0];
    
    if (type === 'image') {
      this.validateImageDimensions(file).then(isValid => {
        if (isValid) {
          this.selectedImage = file;
          this.imageError = null;
        } else {
          this.selectedImage = null;
          this.imageError = 'Please upload a horizontal image (width > height).';
        }
      });
    } else if (type === 'file') {
      this.selectedFile = file;
    }
  }

  validateImageDimensions(file: File): Promise<boolean> {
    return new Promise((resolve) => {
      const img = new Image();
      img.src = URL.createObjectURL(file);
      img.onload = () => {
        const isHorizontal = img.width > img.height;
        resolve(isHorizontal);
      };
      img.onerror = () => resolve(false);
    });
  }

  onSubmit(): void {

    if (this.imageError) {
      this.toastr.error('Please resolve the image error before submitting.');
      return;
    }
    const formData = new FormData();
  formData.append('Name', this.courseForm.value.courseName);
  formData.append('Description', this.courseForm.value.courseDescription);
  if (this.selectedImage) formData.append('Image', this.selectedImage);
  if (this.selectedFile) formData.append('File', this.selectedFile);

  this.courseService.addCourse(formData).subscribe({
    next: (response) => {
      this.toastr.success(response.message);
      // alert('Course added successfully!');
      console.log('Response:', response);

      this.courseForm.reset();
      this.selectedImage = null;
      this.selectedFile = null;
      this.imageError = null;

      // Manually reset the file inputs
      (document.getElementById('imageInput') as HTMLInputElement).value = '';
      (document.getElementById('fileInput') as HTMLInputElement).value = '';
    },
    error: (error) => {
      this.toastr.error(error?.error.message);
      // alert('Failed to add course.');
      console.error('Error:', error);
    },
  });
  }

  ngOnInit(): void {
    const tokenPayload = this.auth.decodeToken();
    this.userRole = tokenPayload?.role || null; // Fetch user role from decoded token
  }

  logout() {
    this.auth.signOut();
  }

}
