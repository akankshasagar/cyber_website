import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RoleMaster } from '../Model/rolemaster.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  private apiUrl = environment.apiURL + "Rolemaster/GetRoles";

  constructor(private http: HttpClient) { }

  getRoles(): Observable<RoleMaster[]> {
    return this.http.get<RoleMaster[]>(this.apiUrl);
  }
}


  // private apiUrl = 'https://localhost:7243/api/Rolemaster/GetRoles';
