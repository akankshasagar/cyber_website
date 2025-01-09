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
    // this.initializeForms();
    this.signupForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      deptName: ['', Validators.required]
    })    
  }

  ngOnInit(): void {
    this.departmentService.getDepartments().subscribe((data) => {
      console.log(this.departments);
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
        department: {
          deptName: this.signupForm.value.deptName
        }
      };
  
      console.log('Payload being sent:', payload); 

      this.auth.signUp(payload)
      .subscribe({
        next:(res => {
          this.toastr.success(res.message);
          // alert(res.message);
          this.signupForm.reset();
          this.router.navigate(['homepage/signin']);
        })
        ,error:(err => {
          this.toastr.error(err?.error.message);
          // alert(err?.error.message)
        })
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.signupForm);
      // alert("Your Form is invalid");
      this.toastr.error("Your Form is invalid");
    }
  }
}
