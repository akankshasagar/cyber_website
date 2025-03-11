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
    this.auth.sendOTP(this.email)
      .subscribe({
        next: (response) => {
          this.showOTPInput = true;
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

  // onLogin() {
  //   if (this.loginForm.valid) {

  //     this.auth.login(this.loginForm.value)
  //     .subscribe({
  //       next:(res) =>{
  //         console.log(res);
  //         this.loginForm.reset();
  //         this.auth.storeToken(res.token);
  //         console.log('Stored Token:', localStorage.getItem('token'));
  //         const tokenPayload = this.auth.decodeToken();
  //         console.log('Decoded Token:', tokenPayload);
  //         const role = tokenPayload?.role;
  //         this.userStore.setFullNameForStore(tokenPayload.name);
  //         this.toastr.success(res.message);
  //         console.log(tokenPayload?.role);
  //         console.log(res.appuser.roleId);
  //         if(res.appuser.roleId == '1'){
  //           console.log('Welcome Admin');
  //           this.router.navigate(['adminpage']);
  //         }else {
  //             this.router.navigate(['courses']);
  //           }
  //         // if (tokenPayload?.role === '3') {
  //         //   this.router.navigate(['courses']);
  //         // } else {
  //         //   this.router.navigate(['adminpage']);
  //         // }
  //       },
  //       error:(err)=>{
  //         this.toastr.warning("Some other error Occurred");
  //         this.toastr.error(err?.error.message);
  //       }
  //     })
  //     //send the obj to database
  //   }
  //   else {

  //     ValidateForm.validateAllFormFields(this.loginForm);
  //     this.toastr.error("Your Form is Invalid!");
  //     //throw the error
  //   }
  // }

  onLogin() {
    if (this.loginForm.valid) {
        this.auth.login(this.loginForm.value).subscribe({
            next: (res) => {

                // Store token
                this.auth.storeToken(res.token);

                // Store email separately
                const userEmail = res.appuser?.email;
                localStorage.setItem('userEmail', userEmail);
                this.auth.setUser(res.appuser.name);
                this.auth.setEmail(res.appuser.email);
                this.auth.setUserId(res.appuser.id);
                this.auth.setRoleId(res.appuser.roleId);
                // localStorage.setItem('userId', res.appuser.id.toString());

                // Decode token
                const tokenPayload = this.auth.decodeToken();

                // Extract roleId from response
                const roleId = res.appuser?.roleId;

                // Set user details in store
                this.userStore.setFullNameForStore(res.appuser.name);

                // Show success message
                this.toastr.success(res.message);

                // Navigate based on roleId
                if (roleId === 1) {
                    this.router.navigate(['adminpage']);
                } else {
                    this.router.navigate(['courses']);
                }
            },
            error: (err) => {
                console.error("Login Error:", err);
                this.toastr.warning("Some other error occurred");
                this.toastr.error(err?.error?.message || "Login failed!");
            }
        });
    } else {
        ValidateForm.validateAllFormFields(this.loginForm);
        this.toastr.error("Your Form is Invalid!");
    }
  }



  Start(){
    this.start = true;
  }

  closeCard() {
    this.start = false;
  }
}
