import { NgIf } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AppRoutingModule } from 'src/app/app-routing.module';
import ValidateForm from 'src/app/helpers/validateform';
import { AuthService } from 'src/app/services/auth.service';
import { DepartmentServiceService } from 'src/app/services/department-service.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent {

  signupForm!: FormGroup;
  hide = true;
  showPassword: boolean = false;
  departments: any[] = [];

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router, private toastr: ToastrService, private departmentService: DepartmentServiceService) {
    this.signupForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      deptName: ['', Validators.required],
      code: ['', Validators.required]
    })
  }

  ngOnInit(): void {
    this.departmentService.getDepartments().subscribe((data) => {
      this.departments = data;
    });
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  onSignup(){
    if(this.signupForm.valid){
      const payload = {
        name: this.signupForm.value.name,
        email: this.signupForm.value.email,
        password: this.signupForm.value.password,
        // department: {
          deptName: this.signupForm.value.deptName,
          code: this.signupForm.value.code
        // }
      };


      this.auth.signUp(payload)
      .subscribe({
        next:(res => {
          this.toastr.success(res.message);
          this.signupForm.reset();
          this.router.navigate(['homepage/signin']);
        })
        ,error: (err) => {
          console.error("Full Error Response:", err);

          let errorMessage = "Something went wrong!";

          if (err.error?.errors) {
            const messages: string[] = Object.values(err.error.errors).flat() as string[];
            messages.forEach((msg) => this.toastr.error(msg));
          }
          else if (err.error?.detail) {  // Handle error with 'detail'
            errorMessage = err.error.detail;
          }
          else if (err.error?.title) {  // Handle error with 'title'
            errorMessage = err.error.title;
          }
          else if (typeof err.error === "string") {  // Handle plain string error
            errorMessage = err.error;
          }

          this.toastr.error(errorMessage);
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.signupForm);
      this.toastr.error("Your Form is invalid");
    }
  }
}
