import { Component, OnInit, Input } from '@angular/core';
import { Kid } from 'src/app/_models/kid';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { KidEditComponent } from 'src/app/kids/kid-edit/kid-edit.component';
import { KidService } from 'src/app/_services/kid.service';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-user-kid-list',
  templateUrl: './user-kid-list.component.html',
  styleUrls: ['./user-kid-list.component.scss']
})
export class UserKidListComponent implements OnInit {
  @Input() user: User;
  bsModalRef: BsModalRef;

  constructor(private kidService: KidService, private modalService: BsModalService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.sortKids();
  }

  // todo get rid of 'any'
  openKidEdit(kid: any) {
    if (!kid.id) {
      kid.organizationId = this.user.organization.id;
      kid.parents = [{ id: this.user.id }];
      kid.isActive = true;
    }

    this.bsModalRef = this.modalService.show(KidEditComponent, { initialState: { kid } });
    this.bsModalRef.content.kidUpdated.subscribe((updatedKid: Kid) => {
      if (updatedKid.id) {
        this.kidService.updateKid(updatedKid).subscribe(null, this.alertify.error, () => {
          this.alertify.success('Successfully updated kid');
          this.sortKids();
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
