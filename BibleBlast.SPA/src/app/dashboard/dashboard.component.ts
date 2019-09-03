import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { KidService } from '../_services/kid.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  viewModel: any[];
  queryParams: any = {
    fromDate: new Date(),
    toDate: new Date(),
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
        console.log(viewModel);
        this.viewModel = viewModel;
      }, this.alertify.error);
  }
}
