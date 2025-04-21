import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { SupplierService } from '../../services/supplier-service/supplier.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import {jwtDecode} from 'jwt-decode';
import { AuthService } from '../auth-service/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(private fb: FormBuilder,
    private authService: AuthService,
    private router: Router) {
    this.loginForm = this.fb.group({
      phoneNumber: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      this.errorMessage = 'נא למלא סיסמא וטלפון';
      console.log(this.errorMessage);
      return;
    }

    const loginData = this.loginForm.value;
    console.log('Login Data:', loginData);

    this.authService.login(loginData).subscribe({
      next: (response) => {
        console.log('Login successfully', response);
        this.errorMessage = '';
        localStorage.setItem('token', response.token);
        this.setLoggedInSupplierId();
        this.router.navigate(['/dashboard']);
      },
      error: (error) => {
        console.error('Login failed: ', error);
        console.error('Response body ', error.error)
        if (error.status === 401 || error.error?.message === 'The password is incorrect') {
          this.errorMessage = 'הסיסמא לא נכונה';
        } else {
          this.errorMessage = error?.error?.errorMessage || error?.errorMessage || 'שגיאה לא ידועה בהתחברות';
        }
        alert(this.errorMessage)
      }
    }
    );
  }
  setLoggedInSupplierId(): void {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decodedToken = jwtDecode(token);
        console.log(decodedToken);
      }
      catch (e) {
        console.error("Error decoding token", e);
      }
    }
  }
}

