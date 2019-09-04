import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { KidService } from '../_services/kid.service';
import { AlertifyService } from '../_services/alertify.service';
import { KidMemoryCategory } from '../_models/memory';
import { DashboardViewModel } from '../_models/kid';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  viewModel: DashboardViewModel[];
  queryParams: any = {
    fromDate: new Date(new Date().setHours(0, 0, 0, 0)),
    toDate: new Date(new Date().setHours(23, 59, 59, 999)),
    kidName: '',
  };
  bsConfig: Partial<BsDatepickerConfig> = { dateInputFormat: 'MM/DD/YYYY' };
  // todo validate date range client-side

  constructor(private route: ActivatedRoute, private kidService: KidService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadCompletedMemories();
  }

  loadCompletedMemories() {
    this.kidService.getKidMemories(this.queryParams)
      .subscribe(viewModel => {
        viewModel.forEach(x => x.categories.sort(this.sortCategories));
        this.viewModel = viewModel;
      }, this.alertify.error);
  }

  sortCategories = (a: KidMemoryCategory, b: KidMemoryCategory) => {
    const nameA = a.categoryName.toUpperCase();
    const nameB = b.categoryName.toUpperCase();
    const numA: number = parseInt(nameA, 10);
    const numB: number = parseInt(nameB, 10);

    if (numA && numB) {
      return numA < numB ? -1 : 1;
    }

    if (nameA < nameB) {
      return -1;
    }

    if (nameA > nameB) {
      return 1;
    }

    return 0;
  }
}
