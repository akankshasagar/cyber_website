import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})


export class AuthGuard{

  constructor(private auth: AuthService, private router: Router, private toastr: ToastrService){

  }

  canActivate():boolean {
    if(this.auth.isLoggedIn()){
      return true;
    }else{
      this.toastr.error("Please Login First!");
      this.router.navigate(['homepage/signin']);
      return false;
    }
    
  }
}