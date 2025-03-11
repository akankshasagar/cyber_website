import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, tap } from 'rxjs';
import { User } from '../Model/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl: string = environment.apiURL + "User/";
  private ansapiurl: string = environment.apiURL + "TestAnswers01";
  private apiUrl = environment.apiURL + "User/RegisterAdminOrManager";
  private isacttest001: string = environment.apiURL + "IsactAnswers001";
  private attacksurfacestest001: string = environment.apiURL + "AttackSurfacesAnswers001";
  private phishingspoofingtest001url: string = environment.apiURL + "PhishingSpoofingAnswer001";
  private wirelessenvurl: string = environment.apiURL + "WirelessEnvironmentAnswer001";
  private dosdontsurl: string = environment.apiURL + "DosDontsAnswer001";
  private irmngmnturl: string = environment.apiURL + "IRMngmntAnswer001";
  private dataprotecturl: string = environment.apiURL + "DataProtectAnswer001";
  private dnsapturl: string = environment.apiURL + "DnsAptAnswer001";
  private cyberstalkurl: string = environment.apiURL + "CyberStalkBullyAnswer001";
  private courseurl: string = environment.apiURL + "CourseEnrollments";
  private coursecompl: string = environment.apiURL + "CoursesCompleted";
  private forgotpwd: string = environment.apiURL + "User/forgot-password";
  private topicadd = environment.apiURL + 'https://your-api-url/AddTopicToModule';
  // private test01: string = ""
  private userPayload: any;
  private fullName: string = '';
  private email: string = '';
  private Id: number = 0;
  private roleId: number = 0;

  constructor(private http: HttpClient, private router: Router){
    this.userPayload = this.decodeToken();
  }

  setUser(fullName: string) {
    this.fullName = fullName;
    localStorage.setItem('fullName', fullName); // Persist user data
  }

  getUser(): string {
    return localStorage.getItem('fullName') || this.fullName;
  }

  setEmail(email: string){
    this.email = email;
    localStorage.setItem('email', email);
  }

  getEmail(): string{
    return localStorage.getItem('email') || this.email;
  }

  setUserId(id: number){
    this.Id = id;
    localStorage.setItem('userId', id.toString());
  }

  getUserId(): string {
    return localStorage.getItem('userId') || '';
  }

  setRoleId(roleId: number){
    this.roleId = roleId;
    localStorage.setItem('roleId', roleId.toString());
  }

  getRoleId(): string{
    return localStorage.getItem('roleId') || '';
  }

  clearUser() {
    this.fullName = '';
    localStorage.removeItem('fullName');
  }

  signUp(userObj: any){
    return this.http.post<any>(`${this.baseUrl}apiuserregister`, userObj);
  }

  login(loginObj: any){
    return this.http.post<any>(`${this.baseUrl}apiuserlogin`, loginObj).pipe(
      tap((response) => {
        if (response && response.userId) {
          localStorage.setItem('userId', response.userId);
        }
      })
    );;
  }

  forgot(userObj: any){
    return this.http.post<any>(`${this.forgotpwd}`, userObj );
  }

  signOut(){
    localStorage.clear();
    this.router.navigate(['homepage']);
  }

  // storeToken(token: string){
  //   localStorage.setItem('token', token);
  // }

  storeToken(token: string) {
    localStorage.setItem('token', token);
  }


  getToken(){
    return localStorage.getItem('token')
  }

  isLoggedIn(): boolean{
    return !!localStorage.getItem('token')
  }

  decodeToken() {
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    return jwtHelper.decodeToken(token)
  }

  getFullNameFromToken() {
    if (this.userPayload)
      return this.userPayload.name;
  }

  getEmailFromToken() {
    if (this.userPayload)
      return this.userPayload.email;
  }

  getUserEmail(): string | null {
    return localStorage.getItem('userEmail');
  }

  getRoleFromToken() {
    if (this.userPayload)
      return this.userPayload.role;
  }

  submitForm(formData: any): Observable<any> {
    return this.http.post(`${this.ansapiurl}`, formData);
  }

  isacttestForm(formData: any): Observable<any>{
    return this.http.post(`${this.isacttest001}`, formData);
  }

  attacksurfacesTestForm(formData: any): Observable<any>{
    return this.http.post(`${this.attacksurfacestest001}`, formData);
  }

  phishingspoofingtestForm(formData: any): Observable<any>{
    return this.http.post(`${this.phishingspoofingtest001url}`, formData);
  }

  wirelessenvironmenttestForm(formData: any): Observable<any>{
    return this.http.post(`${this.wirelessenvurl}`, formData);
  }

  dosanddontsForm(formData: any): Observable<any>{
    return this.http.post(`${this.dosdontsurl}`, formData);
  }

  irmngmntForm(formData: any): Observable<any>{
    return this.http.post(`${this.irmngmnturl}`, formData);
  }

  dataprotectForm(formData: any): Observable<any>{
    return this.http.post(`${this.dataprotecturl}`, formData);
  }

  dnsaptForm(formData: any): Observable<any>{
    return this.http.post(`${this.dnsapturl}`, formData);
  }

  cyberstalkForm(formData: any): Observable<any>{
    return this.http.post(`${this.cyberstalkurl}`, formData);
  }

  enroll(email: string, course: string){
    return this.http.post<any>(`${this.courseurl}?email= ${email.toString()}&course=${course.toString()}`, {email, course});
  }

  complete(email: string, course: string){
    return this.http.post<any>(`${this.coursecompl}?email= ${email.toString()}&course=${course.toString()}`, {email, course});
  }

  test01(email: string){
    return this.http.post<any>(`${this.ansapiurl}/test01?email= ${email.toString()}`, {email});
  }

  isacttest001check(email: string){
    return this.http.post<any>(`${this.isacttest001}/isactest001?email= ${email.toString()}`, {email});
  }

  attackcurfacestest001check(email: string){
    return this.http.post<any>(`${this.attacksurfacestest001}/attacksurfacestest001?email = ${email.toString()}`, {email});
  }

  phishingspoofingtest001check(email: string){
    return this.http.post<any>(`${this.phishingspoofingtest001url}/phishingspoofingtest001?email = ${email.toString()}`, {email});
  }

  wirelessenvtest001check(email: string){
    return this.http.post<any>(`${this.wirelessenvurl}/wirelessenvironmenttest001?email= ${email.toString()}`, {email});
  }

  dosanddontstest001check(email: string){
    return this.http.post<any>(`${this.dosdontsurl}/dosdontstest001?email= ${email.toString()}`, {email});
  }

  irmngmnttest001check(email: string){
    return this.http.post<any>(`${this.irmngmnturl}/irmngmnttest001?email= ${email.toString()}`, {email});
  }

  dataprotecttest001check(email: string){
    return this.http.post<any>(`${this.dataprotecturl}/dataprotecttest001?email= ${email.toString()}`, {email});
  }

  dnsapttest001check(email: string){
    return this.http.post<any>(`${this.dnsapturl}/dnsapttest001?email= ${email.toString()}`, {email});
  }

  cyberstalktest001check(email: string){
    return this.http.post<any>(`${this.cyberstalkurl}/cyberstalkbullytest001?email= ${email.toString()}`, {email});
  }

  sendOTP(email: string): Observable<any> {
    const url = `${environment.apiURL}User/send?email=${email.toString()}`;
    return this.http.post<any>(url, {email});
  }

  VerifyOTP(email: string, otp: string): Observable<any> {
    const url = `${environment.apiURL}User/verify?otp=${otp.toString()}`;
    return this.http.post<any>(url, {email, otp});
  }

  UpdatePassword(email: string, newPassword: string): Observable<any> {
    const url = `${environment.apiURL}User/reset-password`;
    return this.http.post<any>(url, {email, newPassword});
  }

  registerUser(user: any) {
    return this.http.post<any>(this.apiUrl, user);
  }

  getUserByEmail(email: string): Observable<any> {
    const url = `${environment.apiURL}Courses/GetUserByEmail/${email}`;
    return this.http.get(url);
  }

}

// private baseUrl:string = "https://localhost:7243/api/User/";
// private ansapiurl: string = "https://localhost:7243/api/TestAnswers01";
// private apiUrl = 'https://localhost:7243/api/User/RegisterAdminOrManager';
// private isacttest001: string = "https://localhost:7243/api/IsactAnswers001";
// private attacksurfacestest001: string = "https://localhost:7243/api/AttackSurfacesAnswers001";
// private phishingspoofingtest001url: string = "https://localhost:7243/api/PhishingSpoofingAnswer001";
// private wirelessenvurl: string = "https://localhost:7243/api/WirelessEnvironmentAnswer001";
// private dosdontsurl: string = "https://localhost:7243/api/DosDontsAnswer001";
// private irmngmnturl: string = "https://localhost:7243/api/IRMngmntAnswer001";
// private dataprotecturl: string = "https://localhost:7243/api/DataProtectAnswer001";
// private dnsapturl: string = "https://localhost:7243/api/DnsAptAnswer001";
// private cyberstalkurl: string = "https://localhost:7243/api/CyberStalkBullyAnswer001";
// private courseurl: string = "https://localhost:7243/api/CourseEnrollments";
// private coursecompl: string = "https://localhost:7243/api/CoursesCompleted";
// private forgotpwd: string = "https://localhost:7243/api/User/forgot-password";
// const url = `https://localhost:7243/api/OTPSender/send?email=${email.toString()}`;
// const url = `https://localhost:7243/api/OTPSender/verify?email=${email.toString()}&otp=${otp.toString()}`;
// const url = `https://localhost:7243/api/OTPSender/reset?email=${email.toString()}&newPassword=${newPassword.toString()}`;
// const url = `https://localhost:7243/api/CourseEnrollments/GetUserByEmail/${email}`;
