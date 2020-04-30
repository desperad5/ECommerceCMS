import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  // communicate with web api
  constructor(private http: HttpClient, private router: Router) { }

  // properties needed
  private baseUrlLogin = '/api/Auth';
  private loginStatus = new BehaviorSubject<boolean>(this.getLoginStatus());
  private username = new BehaviorSubject<string>(localStorage.getItem('username'));
  private userRole = new BehaviorSubject<string>(localStorage.getItem('userRole'));
  private UserId = new BehaviorSubject<string>(localStorage.getItem('UserId'));



  getLoginStatus(): boolean {
    return false;
  }

  get CurrentUserId() {
    return this.UserId;
  }

  get IsLoggedIn() {
    return this.loginStatus;
  }
  get CurrentUsername() {
    return this.username;
  }
  get CurrentUserRole() {
    return this.userRole;
  }


  loginWithEmail(username, password) {
    return this.http.post<any>(this.baseUrlLogin + "/Login", { "emailAddress": username, "password": password }).pipe(
      map(result => {
        if (result && result.message != null) {
          console.log(result);
          this.loginStatus.next(true);
          localStorage.setItem('username', result.username);

        }
        return result;
      })
    );
  }

  sendRegistrationMail(email: string) {
     
    return this.http.post<any>(this.baseUrlLogin + "/SendCodeToEmailAddress", { "Email": email }).pipe(
      map(result => console.log(result))
    );
  }

  forgotPasswordSendEmail(emailAddress: string) {
     
    return this.http.post<any>(this.baseUrlLogin + "/ForgotPasswordSendEmail", { "emailAddress": emailAddress }).pipe(
      map(result => console.log(result))
    );
  }


  changePasswordWithCode(changePasswordModel) {
    return this.http.post<any>(this.baseUrlLogin + "/ChangePasswordWithCode", changePasswordModel).pipe(
      map(result => {
        if (result && result.message != null) {
        }
        return result;
      })
    );
  }

  changePassword(changePasswordModel) {
    return this.http.post<any>(this.baseUrlLogin + "/ChangePassword", changePasswordModel).pipe(
      map(result => {
        if (result && result.message != null) {
        }
        return result;
      })
    );

  }


}
