import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
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

  constructor(private auth: AuthService, private http: HttpClient, private fb: FormBuilder, private courseService: CourseService) { 
    this.courseForm = this.fb.group({
      courseName: [''],
      courseDescription: [''],
    });
  }

  onFileSelect(event: Event, type: string): void {
    const file = (event.target as HTMLInputElement).files![0];
    if (type === 'image') this.selectedImage = file;
    else if (type === 'file') this.selectedFile = file;
  }

  onSubmit(): void {
    const formData = new FormData();
  formData.append('Name', this.courseForm.value.courseName);
  formData.append('Description', this.courseForm.value.courseDescription);
  if (this.selectedImage) formData.append('Image', this.selectedImage);
  if (this.selectedFile) formData.append('File', this.selectedFile);

  this.courseService.addCourse(formData).subscribe({
    next: (response) => {
      alert('Course added successfully!');
      console.log('Response:', response);
    },
    error: (error) => {
      alert('Failed to add course.');
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
