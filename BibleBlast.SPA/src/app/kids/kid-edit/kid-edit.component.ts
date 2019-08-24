import { Component, EventEmitter, Output, OnInit, ViewChild } from '@angular/core';
import { Kid } from 'src/app/_models/kid';
import { BsModalRef, BsDatepickerConfig } from 'ngx-bootstrap';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-kid-edit',
  templateUrl: './kid-edit.component.html',
  styleUrls: ['./kid-edit.component.scss']
})
export class KidEditComponent implements OnInit {
  @ViewChild('kidForm', { static: true }) kidForm: NgForm;
  @Output() kidUpdated = new EventEmitter();
  @Output() kidDeleted = new EventEmitter();
  kid: Kid;
  initialValue: Kid;
  genderList = [
    { display: 'Male', value: 'male' },
    { display: 'Female', value: 'female' },
    { display: 'Other', value: 'other' }
  ];
  bsConfig: Partial<BsDatepickerConfig> = { dateInputFormat: 'MM/DD/YYYY' };

  constructor(public bsModalRef: BsModalRef, private alertify: AlertifyService) { }

  ngOnInit() {
    this.initialValue = Object.assign({}, this.kid);
  }

  saveKid() {
    this.kidUpdated.emit(this.kid);
    this.bsModalRef.hide();
  }

  deleteKid() {
    this.alertify.confirm('Delete kid', `Are you sure you want to delete ${this.kid.firstName} ${this.kid.lastName}?`, () => {
      this.kidDeleted.emit(this.kid.id);
      this.bsModalRef.hide();
    });
  }

  cancel() {
    this.kidForm.reset(this.initialValue);
    this.bsModalRef.hide();
  }
}
