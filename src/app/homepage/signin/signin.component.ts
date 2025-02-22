import { Component } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastModule, NgToastService, Position } from 'ng-angular-popup';
import { ToastrService } from 'ngx-toastr';
import ValidateForm from 'src/app/helpers/validateform';
import { AuthService } from 'src/app/services/auth.service';
import { UserstoreService } from 'src/app/services/userstore.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent {

  loginForm!: FormGroup;
  signupForm!: FormGroup;
  forgotPassword!: FormGroup;
  start: boolean = false;
  showPassword: boolean = false;
  forshowPassword: boolean = false;

  email!: string;
  otp!: string;
  newPassword!: string;
  showOTPInput: boolean = false;
  otpVerified: boolean = false;  

  hide = true;
  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router, private toastr: ToastrService, private userStore: UserstoreService) {

    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });    
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  togglePasswordVisibilityfor(){
    this.forshowPassword = !this.forshowPassword;
  }   

  onSubmit(forgotPasswordForm: NgForm){
    if (forgotPasswordForm.invalid) {
      // Form is invalid, do not proceed
      return;
    }    
  }

  sendOTP(){
    this.showOTPInput = true;
    this.auth.sendOTP(this.email)
      .subscribe({
        next: (response) => {          
          this.toastr.success(response.message);
        },
        error: (err) => {          
          this.toastr.error(err?.error.message);
        }
      });    
  }

  verifyOTP() {
    this.showOTPInput = true;
    this.auth.VerifyOTP(this.email, this.otp)
      .subscribe({
        next: (response) => {
          this.toastr.success(response.message);
          this.showOTPInput = false;
          this.otpVerified= true;          
        },
        error: (err) => {
          this.toastr.error(err?.error.message);
        }
      });      
  }

  updatePassword() {
    this.auth.UpdatePassword(this.email, this.newPassword)
      .subscribe({
        next: (response) => {
          this.toastr.success(response.message);   
          this.closeForm();              
        },
        error: (err) => {
          this.toastr.error(err?.error.message);
        }
      });    
  }

  closeForm() {
    // Reset form fields and flags
    this.email = '';
    this.otp = '';
    this.newPassword = '';
    this.showOTPInput = false;
    this.otpVerified = false;    
  }

  onLogin() {
    if (this.loginForm.valid) {

      this.auth.login(this.loginForm.value)
      .subscribe({
        next:(res) =>{
          this.loginForm.reset();
          this.auth.storeToken(res.token); 
          const tokenPayload = this.auth.decodeToken();
          const role = tokenPayload?.role;
          this.userStore.setFullNameForStore(tokenPayload.name);
          this.toastr.success(res.message);          

          if (tokenPayload?.role !== '1') {            
            this.router.navigate(['courses']);            
          } else {            
            this.router.navigate(['adminpage']);
          }
        },
        error:(err)=>{
          this.toastr.warning("Some other error Occurred");
          this.toastr.error(err?.error.message);
        }
      })
      //send the obj to database
    }
    else {
      
      ValidateForm.validateAllFormFields(this.loginForm);
      this.toastr.error("Your Form is Invalid!");
      //throw the error 
    }
  }

  Start(){
    this.start = true;
  }

  closeCard() {
    this.start = false;
  }
}
