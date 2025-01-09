import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private apiUrl = 'https://localhost:7243/api/Courses';

  constructor(private http: HttpClient) { }

  addCourse(formData: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, formData);
  }

  getCourses(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
