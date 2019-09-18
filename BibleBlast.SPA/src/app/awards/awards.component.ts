import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AwardCategory } from '../_models/award';

@Component({
  selector: 'app-awards',
  templateUrl: './awards.component.html',
  styleUrls: ['./awards.component.scss']
})
export class AwardsComponent implements OnInit {
  awardsByCategory: AwardCategory[];

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => this.awardsByCategory = data.awards);
  }
}
