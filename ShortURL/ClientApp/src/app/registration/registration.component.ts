import { Component, Inject, OnInit } from '@angular/core';
import { from } from 'rxjs';
import { FormBuilder, FormGroup, Validators, ValidationErrors } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  userToRegForm: FormGroup;
  userToReg: UserToReg;
  private baseUrl: string;

  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private formBuilder: FormBuilder) {
    this.buildForm();
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
  }

  private buildForm() {
    this.userToRegForm = this.formBuilder.group({
      login: ['', Validators.required],
      password: ['', Validators.required]      
    });
  }
  public onSubmit() {
    this.userToReg = this.userToRegForm.value;
    localStorage.setItem('userToReg', JSON.stringify(this.userToReg));

    const payload = this.userToReg;
    this.http.post(this.baseUrl + 'user/add-user', payload).subscribe(
      result => { console.log("Users controller says: OK") },
      error => { console.log("Users controller says: " + error) });
    this.router.navigate(['/']);
  }

  public clearData() {
    localStorage.clear();
    this.userToRegForm.reset();
  }

  public isControlInvalid(controlName: string): boolean {
    let control = this.userToRegForm.get(controlName);
    return !control.valid;
  }

}

export class UserToReg {
  login: string;
  password: string;
 }
