import { Component, Input, OnInit } from '@angular/core';
import { AwardService } from 'src/app/_services/award.service';
import { AwardEarned } from 'src/app/_models/award';

@Component({
  selector: 'app-completed-memories',
  templateUrl: './completed-memories.component.html',
  styleUrls: ['./completed-memories.component.scss']
})
export class CompletedMemoriesComponent implements OnInit {
  @Input() category: any;
  @Input() fromDate: Date;
  @Input() toDate: Date;
  awards: AwardEarned[];

  constructor(private awardService: AwardService) { }

  ngOnInit(): void {
    this.loadAwardsEarned();
  }

  loadAwardsEarned() {
    this.awardService.getAwardsEarned(this.category.categoryId)
      .subscribe(awards => this.awards = awards, console.error);
  }
}
