import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Organization } from 'src/app/_models/organization';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  user: User;
  orgSelectList: Organization[] = [];

  constructor(
    private userService: UserService, private route: ActivatedRoute,
    private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user;
      this.orgSelectList = data.organizations;
    });
  }

  sameId = (a, b) => a && b && a.id === b.id;

  goBack() {
    this.router.navigate(['/users']);
  }

  updateUser() {
    this.userService.updateUser(this.user).subscribe(() => {
      this.alertify.success('User updated successfully');
      this.editForm.reset(this.user);
    }, this.alertify.error);
  }
}
