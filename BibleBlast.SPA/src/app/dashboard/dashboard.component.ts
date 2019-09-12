import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { AlertifyService } from '../_services/alertify.service';
import { KidMemoryCategory } from '../_models/memory';
import { DashboardViewModel } from '../_models/kid';
import { MemoryService } from '../_services/memory.service';

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

  constructor(private memoryService: MemoryService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadCompletedMemories();
  }

  loadCompletedMemories() {
    this.memoryService.getCompletedMemories(this.queryParams)
      .subscribe(viewModel => {
        this.viewModel = viewModel;
      }, this.alertify.error);
  }
}
