import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-award-edit',
  templateUrl: './award-edit.component.html',
  styleUrls: ['./award-edit.component.scss']
})
export class AwardEditComponent implements OnInit {
  award: any;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => this.award = data.award);
  }
}
