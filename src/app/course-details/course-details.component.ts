import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
// import * as pdfjsLib from 'pdfjs-dist';

@Component({
  selector: 'app-course-details',
  templateUrl: './course-details.component.html',
  styleUrls: ['./course-details.component.css']
})
export class CourseDetailsComponent {

  course: any;

  constructor(private route: ActivatedRoute, private http: HttpClient) {}

  ngOnInit(): void {
    const courseId = this.route.snapshot.paramMap.get('id');
    this.http.get(`https://localhost:7243/api/Courses/${courseId}`).subscribe((data: any) => {
        this.course = data;
    });
  }
 
}
