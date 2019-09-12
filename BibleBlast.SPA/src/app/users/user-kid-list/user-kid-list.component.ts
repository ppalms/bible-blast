import { Component, OnInit, Input } from '@angular/core';
import { Kid } from 'src/app/_models/kid';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap';
import { KidEditComponent } from 'src/app/kids/kid-edit/kid-edit.component';
import { KidService } from 'src/app/_services/kid.service';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { KidSearchComponent } from 'src/app/kids/kid-search/kid-search.component';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-user-kid-list',
  templateUrl: './user-kid-list.component.html',
  styleUrls: ['./user-kid-list.component.scss']
})
export class UserKidListComponent implements OnInit {
  @Input() user: User;
  bsModalRef: BsModalRef;

  constructor(
    private kidService: KidService, private userService: UserService,
    private modalService: BsModalService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.sortKids();
  }

  // todo get rid of 'any'
  openEdit(kid: any) {
    if (!kid.id) {
      kid.organizationId = this.user.organization.id;
      kid.parents = [{ id: this.user.id }];
      kid.isActive = true;
    }

    this.bsModalRef = this.modalService.show(KidEditComponent, { initialState: { kid } });
    this.bsModalRef.content.kidUpdated.subscribe((updatedKid: Kid) => {
      if (updatedKid.id) {
        this.kidService.updateKid(updatedKid).subscribe({
          next: null,
          error: this.alertify.error,
          complete: () => {
            this.alertify.success('Successfully updated kid');
            this.sortKids();
          }
        });
      } else {
        this.kidService.insertKid(updatedKid).subscribe((result: Kid) =>
          this.user.kids.push(result), this.alertify.error, () => {
            this.alertify.success('Successfully created kid');
            this.sortKids();
          });
      }
    });

    this.bsModalRef.content.kidDeleted.subscribe((id: number) => {
      this.kidService.deleteKid(id).subscribe(() => this.user.kids.splice(this.user.kids.findIndex(x => x.id === id), 1),
        this.alertify.error, () => this.alertify.success('Successfully deleted kid'));
    });
  }

  openSearch() {
    const initialState = { selectedKids: this.user.kids };
    this.bsModalRef = this.modalService.show(KidSearchComponent, { initialState });
    this.bsModalRef.content.kidsSelected.subscribe(() => {
      this.userService.updateUser(this.user).subscribe(() => {
        this.alertify.success('Successfully updated kids');
      }, this.alertify.error, () => this.sortKids());
    });
  }

  sortKids = () => this.user.kids && this.user.kids.sort((a, b) => {
    const lastNameA = a.lastName.toUpperCase();
    const firstNameA = a.firstName.toUpperCase();
    const lastNameB = b.lastName.toUpperCase();
    const firstNameB = b.firstName.toUpperCase();

    if (a.isActive && b.isActive) {
      if (lastNameA === lastNameB) {
        return firstNameA < firstNameB ? -1 : 1;
      }

      return lastNameA < lastNameB ? -1 : 1;
    }

    return a.isActive && !b.isActive ? -1 : 1;
  })
}
