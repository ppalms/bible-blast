import { Component, OnInit, ViewChild, ComponentFactoryResolver } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Organization } from 'src/app/_models/organization';
import { AuthService } from 'src/app/_services/auth.service';
import { BsModalService } from 'ngx-bootstrap';
import { UserPasswordResetComponent } from '../user-password-reset/user-password-reset.component';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
  editForm: FormGroup;
  user: User;
  roleSelectList = ['Coach', 'Member'];
  orgSelectList: Organization[] = [];

  constructor(
    private userService: UserService, private authService: AuthService, private route: ActivatedRoute,
    private router: Router, private modalService: BsModalService, private alertify: AlertifyService,
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user || {};
      this.orgSelectList = data.organizations;

      this.editForm = this.formBuilder.group({
        username: [{ value: this.user.username, disabled: this.user.id }, Validators.required],
        firstName: [this.user.firstName, Validators.required],
        lastName: [this.user.lastName, Validators.required],
        email: [this.user.email, Validators.email],
        phoneNumber: [this.user.phoneNumber, [Validators.maxLength(11), Validators.pattern('^\\d+$')]],
        userRole: [{
          value: this.user.userRole || 'Member', disabled: this.user.id === this.authService.decodedToken.nameid
        }, Validators.required],
        organization: [this.user.organization || { id: this.authService.decodedToken.organizationId }, Validators.required],
        password: [{ value: '', disabled: this.user.id }, [
          Validators.required, Validators.minLength(10),
          // Validators.pattern('(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{10,}')
        ]],
        confirmPassword: [{ value: '', disabled: this.user.id }, Validators.required],
      }, { validator: this.passwordMatchValidator });
    });
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : { mismatch: true };
  }

  sameId = (a: any, b: any) => a && b && a.id === b.id;

  goBack() {
    this.router.navigate(['/users']);
  }

  isCurrentUser = () => this.user.id === +this.authService.decodedToken.nameid;

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
      this.authService.register(this.user).subscribe((newUser: User) => {
        this.alertify.success('User created');
        this.user = newUser;
      }, this.alertify.error, () => {
        this.router.navigate([`/users/${this.user.id}`]);
      });
    }
  }

  resetPassword() {
    this.modalService.show(UserPasswordResetComponent, { initialState: { userId: this.user.id } });
  }

  deleteUser() {
    this.alertify.confirm('Delete user', `Are you sure you want to delete ${this.user.firstName} ${this.user.lastName}?`, () => {
      this.userService.deleteUser(this.user.id).subscribe(() => {
        this.router.navigate(['/users']);
      }, this.alertify.error);
    });
  }
}
