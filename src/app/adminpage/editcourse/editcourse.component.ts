import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
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

  constructor(private auth: AuthService,
    private courseService: CourseService,
    private route: ActivatedRoute,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    // Get the courseId from the route parameters
    this.route.paramMap.subscribe(params => {
      this.courseId = +params.get('courseId')!;  // Extract courseId from route, and convert it to a number
      console.log('Editing course with ID:', this.courseId);
    });
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

  saveChanges() {
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
          console.log('Course updated successfully:', response);
        },
        error: (error) => {
          console.error('Error updating course:', error);
        }
      });
  }


  logout() {
    this.auth.signOut();
  }
}
