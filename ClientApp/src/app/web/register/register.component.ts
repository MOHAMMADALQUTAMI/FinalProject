import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registrationForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private auth: AuthService) {
    this.registrationForm = this.formBuilder.group({
      username: [''],
      email: [''],
      password: [''],
      confirmpassword: [''],
      isUser: [false],
      isShop: [false],
      isAdmin: [false]
    });
  }
  onSubmit() {
    if (this.registrationForm.valid) {
      const formData = this.registrationForm.value;
      this.auth.register(formData);
    }
  }



}
