import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { CourseService } from '../services/course.service';
import { DepartmentServiceService } from '../services/department-service.service';
import { User } from '../Model/user.model';
import { RoleService } from '../services/role.service';
import { RoleMaster } from '../Model/rolemaster.model';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environment/environment';

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
  apiUrl = environment.apiUrl + "User/RegisterAdminOrManager";
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
    console.log('Decoded Token:', tokenPayload);

  const userEmail = tokenPayload?.email;
  if (!userEmail) {
    console.error('Email is missing in the token.');
    return;
  }

  this.auth.getUserByEmail(userEmail).subscribe({
    next: (user) => {
      this.user.id = user.id; // Assuming the backend returns a user object with an `id` field
      console.log('UserId fetched successfully:', this.user.id);
    },
    error: (error) => {
      console.error('Failed to fetch UserId:', error);
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
        console.error('Failed to fetch courses:', error);
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
    // Set the departmentId and roleId based on the selected values
    // this.user.deptId = this.selectedDeptId;
    // this.user.roleId = this.selectedRoleId;
    // console.log("Submitting user:", this.user);
    // // Send the user data to the backend for registration
    // this.auth.registerUser(this.user).subscribe(
    //   (response) => {
    //     console.log('Registration Successful:', response);
    //     // alert('Registration Successful');
    //     this.toastr.success(response.message);
    //     this.isFormVisible = false;  // Hide form on success
    //   },
    //   (error) => {
    //     console.error('Registration Failed:', error);
    //     // let errorMessage = 'Some Other Error Occured';
    //     // if (error?.error?.Message) {
    //     //   errorMessage = error.error.Message;
    //     // }
    //     this.toastr.error(error?.error.message);
    //   }
    // );

    const requestData = {
      name: this.user.name,
      email: this.user.email,
      password: this.user.password,
      roleId: this.selectedRoleId,
      deptId: this.selectedDeptId
    };

    this.http.post(`${environment.apiUrl}User/RegisterAdminOrManager`, requestData)
      .subscribe(
        (response) => {
          console.log('Registration successful', response);
          alert('User registered successfully!');
          this.closeForm();
        },
        (error) => {
          console.error('Error during registration', error);
          alert(error.error?.Message || 'Registration failed!');
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
        console.error('Error loading departments', error);
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
        console.error('Error loading roles', error);
      }
    );
  }
  
  logout() {
    this.auth.signOut();
  }

  // enroll(email: string, course: string) {
  //   this.auth.enroll(email, course)
  //     .subscribe({
  //       next: (response)  => {
  //         console.log('Enrollment successful', response);
  //       },
  //       error: (error) => {
  //         console.error('Error occurred during enrollment', error);
  //       }
  //     });
  // }

  showForm(): void {
    this.isFormVisible = true;
  }

  closeForm(): void {
    this.isFormVisible = false;
  }

  Start(course: any): void {
    this.selectedCourse = course;
    this.start = true; // Display the modal
    console.log(course);
    console.log(this.selectedCourse);
    document.body.style.overflow = 'hidden';
  }

  closeCard() {
    this.start = false;
    this.selectedCourse = null;
    document.body.style.overflow = '';
  }

  enroll(): void {
    if (!this.user.id || !this.selectedCourse) {
      console.error('User or course information is missing.');
      alert('Please ensure you are logged in and have selected a course.');
      return;
    }
  
    const enrollmentRequest = {
      UserId: this.user.id,
      CourseId: this.selectedCourse.id,
    };
  
    this.http.post( `${environment.apiUrl}CourseEnrollments/Enroll`, enrollmentRequest).subscribe({
      next: (response: any) => {
        // console.log('Enrollment successful', response);
        // alert(response.Message);
        this.start = false; // Close the modal
      },
      error: (error) => {
        // console.error('Error occurred during enrollment:', error);
        // const errorMessage = error?.error?.message || 'An unknown error occurred.';
        // alert(errorMessage);
      },
    });
  }
  
  
}


  // apiUrl = 'https://localhost:7243/api/User/RegisterAdminOrManager';
    // this.http.post('https://localhost:7243/api/CourseEnrollments/Enroll', enrollmentRequest).subscribe({
    // this.http.post('https://localhost:7243/api/User/RegisterAdminOrManager', requestData)
