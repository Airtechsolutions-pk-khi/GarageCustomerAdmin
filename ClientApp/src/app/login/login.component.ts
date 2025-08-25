import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LoginService } from '../_services/login.service';
import { first } from 'rxjs/operators';
import { LocalStorageService } from '../_services/local-storage.service';
import { ToastService } from '../_services/toastservice';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  constructor(private formBuilder: FormBuilder,
    public service: LoginService,
    public ts: ToastService,
    private router: Router,
    private ls: LocalStorageService) { }

  ngOnInit() {
    this.createForm();
  }
  onSubmit() {

    debugger
    this.loginForm.markAllAsTouched();
    if (this.loginForm.invalid) {
      return;
    }
    debugger
    if (this.f.username.value == "admin@garage.com" && this.f.password.value == "Admin@123") {
    localStorage.setItem("_usrAuthenticated", "true"); // ✅ set authentication
      this.router.navigate(["/admin/dashboard"]);
    }
    else {
      this.ts.showError("Error", "Username or password is not correct.");
    }
  }
  get f() { return this.loginForm.controls; }
  private createForm() {

    this.loginForm = this.formBuilder.group({

      username: ['', Validators.required],
      password: ['', Validators.required],

    });
  }



}
