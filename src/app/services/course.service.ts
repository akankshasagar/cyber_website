import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private apiUrl = 'https://localhost:7243/api/Course';
  private apiUrl2 = 'https://localhost:7243/api/Course/AddCourseWithModulesAndTopics';
  private baseUrl = 'https://localhost:7243/api/Module';

  constructor(private http: HttpClient) { }

  addCourse(formData: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, formData);
  }

  addCourse2(payload: any): Observable<any> {
    return this.http.post<any>(this.apiUrl2, payload).pipe(
      catchError((error) => {
        console.error('API Error:', error);
        return throwError(error); // Re-throw the error for component handling
      })
    );
  }
  

  getCourses(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getCourseById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  getModulesByCourse(courseId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/${courseId}`);
  }
}
