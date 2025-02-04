import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from 'src/environment/environment';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  
  private apiUrl = environment.apiUrl + "Course";  
  private apiUrl2 = environment.apiUrl + "Course/AddCourseWithModulesAndTopics";
  private baseUrl = environment.apiUrl + "Module";  
  private topicurl = environment.apiUrl + "Topic";  
  private moduleurl = environment.apiUrl + "Module/AddModuleToCourse";  
  private delmodule = environment.apiUrl + "Module/DeleteModule";  
  private quesurl = environment.apiUrl + "Answers";

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

  getCourseById(id: number, courseName: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}/${encodeURIComponent(courseName)}`);
  }

  getModulesByCourse(courseId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/${courseId}`);
  }

  getTopicsByCourse(courseId: number): Observable<any> {
    return this.http.get(`${this.topicurl}/${courseId}/Topics`);
  }

  getTopicsByCourseAndModule(courseId: number, moduleId: number): Observable<any> {
    return this.http.get(`${this.topicurl}/${courseId}/${moduleId}/Topics`);
  }

  getTopicById(topicId: number): Observable<any> {
    return this.http.get(`${this.topicurl}/${topicId}`);
  }

  getQuestionsByModule(moduleId: number): Observable<any[]> {    
    return this.http.get<any[]>(`${environment.apiUrl}Questions/Module/${moduleId}`);
  }  

  editCourse(courseId: number, courseData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/EditCourse/${courseId}`, courseData);
  }

  addModuleToCourse(payload: any): Observable<any> {
    return this.http.post<any>(this.moduleurl, payload);
  }

  deleteModule(courseId: number, moduleId: number): Observable<any> {
    return this.http.delete(`${this.delmodule}/${courseId}/${moduleId}`);
  }
}



// private apiUrl = 'https://localhost:7243/api/Course';
// private apiUrl2 = 'https://localhost:7243/api/Course/AddCourseWithModulesAndTopics';
// private baseUrl = 'https://localhost:7243/api/Module';
// private topicurl = 'https://localhost:7243/api/Topic';
// private moduleurl = 'https://localhost:7243/api/Module/AddModuleToCourse';
// private delmodule = 'https://localhost:7243/api/Module/DeleteModule'
// private quesurl = 'https://localhost:7243/api/Answers';
// return this.http.get<any[]>(`https://localhost:7243/api/Questions/Module/${moduleId}`);