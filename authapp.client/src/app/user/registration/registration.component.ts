import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { AbstractControl, Form, FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-registration',
  imports: [ReactiveFormsModule, NgIf],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  form: FormGroup;
  isSubmitted = false;

  passwordMatchValidator: ValidatorFn = (control: AbstractControl): null => {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');

    if (password && confirmPassword && password.value === confirmPassword.value) {
      confirmPassword?.setErrors({ passwordMissmatch: true });
    } else {
      confirmPassword?.setErrors(null);
    }
    return null;
  }

  constructor(public formBuilder: FormBuilder) {
    this.form = this.formBuilder.group({
      fullName: ['', Validators.required],
      email: ['', Validators.required, Validators.email],
      password: ['', Validators.required, Validators.minLength(6), Validators.pattern('^(?=.*[A-Z])(?=.*\d)(?=.*\W).+$')],
      confirmPassword: ['', Validators.required],
    }, { validators: this.passwordMatchValidator });
  }

  onSubmit() {
    console.log(this.form.value);
    this.isSubmitted = true;
  }

  hasDisplayError(controlName: string): Boolean {
    const control = this.form.get(controlName);
    return Boolean( control?.invalid) && (this.isSubmitted || Boolean(control?.touched));
  }
}
