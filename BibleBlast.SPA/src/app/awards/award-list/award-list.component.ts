import { Component, Input } from '@angular/core';
import { AwardListItem } from 'src/app/_models/award';
import { Router } from '@angular/router';

@Component({
  selector: 'app-award-list',
  templateUrl: './award-list.component.html',
  styleUrls: ['./award-list.component.scss']
})
export class AwardListComponent {
  @Input() awards: AwardListItem[];

  constructor(private router: Router) { }

  editAward(id: number) {
    return;
    // this.router.navigate([`/awards/${id}`]);
  }
}
