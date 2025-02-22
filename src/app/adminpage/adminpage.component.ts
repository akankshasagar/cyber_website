import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { CourseService } from '../services/course.service';
import { DepartmentServiceService } from '../services/department-service.service';
import { User } from '../Model/user.model';
import { RoleService } from '../services/role.service';
import { RoleMaster } from '../Model/rolemaster.model';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-adminpage',
  templateUrl: './adminpage.component.html',
  styleUrls: ['./adminpage.component.css']
})
export class AdminpageComponent {

  start: boolean = false;
  start2: boolean = false;
  userRole: string | null = null;
  courses: any[] = [];
  selectedCourse: any = null;
  isFormVisible: boolean = false;
  user: User = new User();
  apiUrl = environment.apiURL + "User/RegisterAdminOrManager";
  departments: { deptId: number, deptName: string }[] = [];
  selectedDeptId: number = 0;
  roles: RoleMaster[] = [];
  selectedRoleId: number = 0; // Default roleId value
  currentPage: number = 1; // Current page number
  coursesPerPage: number = 3; // Number of courses per page
  paginatedCourses: any[] = []; // Courses to display on the current page

  constructor(private auth: AuthService, private http: HttpClient, private courseService: CourseService, private departmentService: DepartmentServiceService, private roleService: RoleService, private toastr: ToastrService) { 
  
  }
  
  ngOnInit(): void {
    const tokenPayload = this.auth.decodeToken();
    const userEmail = tokenPayload?.email;
    if (!userEmail) {
      this.toastr.error('You are not logged in');      
      return;
    }

    this.auth.getUserByEmail(userEmail).subscribe({
      next: (user) => {
        this.user.id = user.id; // Assuming the backend returns a user object with an `id` field        
      },
      error: (error) => {
        this.toastr.error("Failed to fetch User");        
      },
    });

    this.userRole = tokenPayload?.role || null; // Fetch user role from decoded token 
    this.loadDepartments();   
    this.loadRoles();
    this.courseService.getCourses().subscribe({
      next: (data) => {
        this.courses = data;
        this.updatePaginatedCourses();
      },
      error: (error) => {
        this.toastr.error("Failed to fetch Courses", error);        
      },
    });
  }  

  updatePaginatedCourses(): void {
    const startIndex = (this.currentPage - 1) * this.coursesPerPage;
    const endIndex = startIndex + this.coursesPerPage;
    this.paginatedCourses = this.courses.slice(startIndex, endIndex);
  }

  goToPage(page: number): void {
    this.currentPage = page;
    this.updatePaginatedCourses();
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages()) {
      this.currentPage++;
      this.updatePaginatedCourses();
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePaginatedCourses();
    }
  }

  totalPages(): number {
    return Math.ceil(this.courses.length / this.coursesPerPage);
  }

  onSubmit(): void {    

    const requestData = {
      name: this.user.name,
      email: this.user.email,
      password: this.user.password,
      roleId: this.selectedRoleId,
      deptId: this.selectedDeptId
    };

    this.http.post(`${environment.apiURL}User/RegisterAdminOrManager`, requestData)
      .subscribe(
        (response) => {
          this.toastr.success("Registration successful");
          this.closeForm();
        },
        (error) => {
          this.toastr.error("Registration failed", error);          
        }
      );

  }


  // Method to load departments from API
  loadDepartments(): void {
    this.departmentService.getDepartments().subscribe(
      (data) => {
        this.departments = data;
      },
      (error) => {
        this.toastr.error("No Departments found");        
      }
    );
  }

  // Load roles from the Role service
  loadRoles(): void {
    this.roleService.getRoles().subscribe(
      (data) => {
        this.roles = data;
      },
      (error) => {
        this.toastr.error("No Roles found");        
      }
    );
  }
  
  logout() {
    this.auth.signOut();
  }
  

  showForm(): void {
    this.isFormVisible = true;
  }

  closeForm(): void {
    this.isFormVisible = false;
  }

  Start(course: any): void {
    this.selectedCourse = course;
    this.start = true; // Display the modal    
    document.body.style.overflow = 'hidden';
  }

  closeCard() {
    this.start = false;
    this.selectedCourse = null;
    document.body.style.overflow = '';
  }

  enroll(): void {
    if (!this.user.id || !this.selectedCourse) {
      this.toastr.error("User or course information is missing.");
      return;
    }
  
    const enrollmentRequest = {
      UserId: this.user.id,
      CourseId: this.selectedCourse.id,
    };
  
    this.http.post( `${environment.apiURL}CourseEnrollments/Enroll`, enrollmentRequest).subscribe({
      next: (response: any) => {        
        this.start = false; // Close the modal
      },
      error: (error) => {        
      },
    });
  }
  
  
}


  // apiUrl = 'https://localhost:7243/api/User/RegisterAdminOrManager';
    // this.http.post('https://localhost:7243/api/CourseEnrollments/Enroll', enrollmentRequest).subscribe({
    // this.http.post('https://localhost:7243/api/User/RegisterAdminOrManager', requestData)
