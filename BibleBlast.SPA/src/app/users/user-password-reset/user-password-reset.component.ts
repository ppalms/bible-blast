import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { BsModalRef } from 'ngx-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-user-password-reset',
  templateUrl: './user-password-reset.component.html',
  styleUrls: ['./user-password-reset.component.scss']
})
export class UserPasswordResetComponent implements OnInit {
  @Input() userId: number;
  passwordForm: FormGroup;

  constructor(
    public bsModalRef: BsModalRef, private authService: AuthService,
    private formBuilder: FormBuilder, private alertify: AlertifyService) { }

  ngOnInit() {
    this.passwordForm = this.formBuilder.group({
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required],
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : { mismatch: true };
  }

  save() {
    this.authService.resetPassword(this.userId, this.passwordForm.get('password').value).subscribe(() => {
      this.alertify.success('Password successfully changed');
    }, this.alertify.error, () => this.bsModalRef.hide());
  }
}
