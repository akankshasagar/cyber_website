import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  // constructor(private auth: AuthService, private toastr: ToastrService, private router: Router) {}

  // intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
  //   const myToken = this.auth.getToken();

  //   if(myToken){
  //     request = request.clone({
  //       setHeaders: { Authorization: `Bearer ${myToken}`}
  //     })
  //   }

  //   return next.handle(request).pipe(
  //     catchError((err:any)=>{
  //       if(err instanceof HttpErrorResponse){
  //         if(err.status === 401){
  //           this.toastr.error(err?.error.message);
  //           this.router.navigate(['homepage/signin']);
  //         }
  //         else if (err.status === 404) {
  //           this.toastr.error(err?.error.message);
  //         }
  //         else if(err.status === 400){
  //           this.toastr.error(err?.error.message);
  //         }
  //       }
  //       return throwError(() => new Error("Some Other Error Occured"))
  //     })
  //   );
  // }

  constructor(
    private auth: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const myToken = this.auth.getToken();

    if (myToken) {
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${myToken}` }
      });
    }

    return next.handle(request).pipe(
      catchError((err: any) => {
        if (err instanceof HttpErrorResponse) {
          console.error("HTTP Error:", err); // Debugging log

          let errorMessage = 'An error occurred';

          if (err.error) {
            if (typeof err.error === 'string') {
              errorMessage = err.error; // Handle plain text errors
            }
            else if (err.error.errors) {
              // Extract validation errors and format with <br> for Toastr
              errorMessage = Object.values(err.error.errors)
                .flat()
                .join('<br>'); // Use <br> for new lines in Toastr
            }
            else if (err.error.message) {
              errorMessage = err.error.message;
            }
          } else if (err.message) {
            errorMessage = err.message;
          }

          switch (err.status) {
            case 401:
              this.toastr.error(errorMessage, '', { enableHtml: true }); // Enable HTML for <br> support
              this.router.navigateByUrl('homepage/signin');
              break;
            case 400:
            case 404:
              this.toastr.error(errorMessage, '', { enableHtml: true }); // Enable HTML for Toastr
              break;
            default:
              this.toastr.error('Unexpected error occurred');
          }
        }
        return throwError(() => new Error(err.message || "Unknown error occurred"));
      })
    );
  }



}
