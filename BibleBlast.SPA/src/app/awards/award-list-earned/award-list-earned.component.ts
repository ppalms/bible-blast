import { Component, Input } from '@angular/core';
import { KidService } from 'src/app/_services/kid.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AwardEarned } from 'src/app/_models/award';

@Component({
  selector: 'app-award-list-earned',
  templateUrl: './award-list-earned.component.html',
  styleUrls: ['./award-list-earned.component.scss']
})
export class AwardListEarnedComponent {
  @Input() awards: AwardEarned[];

  constructor(private kidService: KidService, private alertify: AlertifyService) { }

  presentAward(kidId: number, awardId: number) {
    const datePresented = new Date();
    datePresented.setHours(0, 0, 0, 0);

    this.kidService.insertKidAward({ kidId, awardId, datePresented })
      .subscribe(() =>
        this.alertify.success('Item awarded!'),
        () => this.alertify.error('Unable to award item'),
        () => {
          const kidAward = this.awards.find(a => a.id === awardId).kids.find(k => k.id === kidId);
          kidAward.datePresented = datePresented;
        });
  }
}
