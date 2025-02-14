import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DepartmentServiceService {

  private apiUrl = environment.apiURL + "Department/GetDepartments";

  constructor(private http: HttpClient) { }

  getDepartments(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}


  // private apiUrl = 'https://localhost:7243/api/Department/GetDepartments';
