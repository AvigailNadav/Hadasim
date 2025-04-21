import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { SupplierService } from '../../services/supplier-service/supplier.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth-service/auth.service';

@Component({
  selector: 'app-supplier-register',
  standalone: true,
  imports: [FormsModule,CommonModule,ReactiveFormsModule],
  templateUrl: './supplier-register.component.html',
  styleUrls: ['./supplier-register.component.css']
})
export class SupplierRegisterComponent {
  registerForm:FormGroup;
  message:string='';
  submitted=false;

  constructor(private fb:FormBuilder, private authService:AuthService,private router:Router){
    this.registerForm=this.fb.group({
      name:['',Validators.required],
      phoneNumber:['',[Validators.required,Validators.pattern(/^05\d{8}$/)]],
      password:['',[Validators.required,Validators.minLength(4)]],
      companyName:['',Validators.required]
    });
  }
  get form(){
    return this.registerForm?.controls;
  }
  register(){
    this.submitted=true;
    if(this.registerForm?.invalid){
      this.message='אנא ודא שכל השדות מולאו כראוי';
      return;
    }
    const supplier={
      ...this.registerForm?.value,
      role:'Supplier'
    };
    this.authService.register(supplier).subscribe({
      next:(response:any)=>{
        this.message='ההרשמה בוצעה בהצלחה!';
        if(response.token){
          this.authService.saveToken(response.token,supplier.role);
        }
        this.router.navigate(['navbar']);
      },
      error:(error)=>{
        console.error('Registration error: ',error);
        this.message=error?.error?.message||'אירעה שגיאה במהלך ההרשמה';
      }
  });
  }
}
