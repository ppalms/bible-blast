import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Organization } from 'src/app/_models/organization';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
  editForm: FormGroup;
  user: User;
  orgSelectList: Organization[] = [];

  constructor(
    private userService: UserService, private authService: AuthService, private route: ActivatedRoute,
    private router: Router, private alertify: AlertifyService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user || {};
      this.orgSelectList = data.organizations;

      this.editForm = this.formBuilder.group({
        username: [{ value: this.user.username, disabled: this.user.id }, Validators.required],
        firstName: [this.user.firstName, Validators.required],
        lastName: [this.user.lastName, Validators.required],
        organization: [this.user.organization, Validators.required],
        password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
        confirmPassword: ['', Validators.required],
      }, { validator: this.passwordMatchValidator });
    });
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : { mismatch: true };
  }

  sameId = (a: Organization, b: Organization) => a && b && a.id === b.id;

  goBack() {
    this.router.navigate(['/users']);
  }

  saveUser() {
    if (!this.editForm.valid) {
      return;
    }

    this.user = Object.assign(this.user, this.editForm.value);

    if (this.user.id) {
      this.userService.updateUser(this.user).subscribe(() => {
        this.alertify.success('User updated');
        this.editForm.reset(this.user);
      }, this.alertify.error);
    } else {
      this.authService.register(this.user).subscribe(() => {
        this.alertify.success('User created');
      }, this.alertify.error, () => {
        this.router.navigate(['/users']);
      });
    }
  }

  deleteUser() {
    this.alertify.confirm('Delete user', `Are you sure you want to delete ${this.user.firstName} ${this.user.lastName}?`, () => {
      this.userService.deleteUser(this.user.id).subscribe(() => {
        this.router.navigate(['/users']);
      }, this.alertify.error);
    });
  }
}
