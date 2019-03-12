import { Component, OnInit } from '@angular/core';
import { Kid } from '../../_models/kid';
import { KidService } from 'src/app/_services/kid.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from '../../_models/pagination';

@Component({
  selector: 'app-kid-list',
  templateUrl: './kid-list.component.html',
  styleUrls: ['./kid-list.component.scss']
})
export class KidListComponent implements OnInit {
  kids: Kid[];
  pagination: Pagination;

  constructor(private kidService: KidService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.kids = data.kids.result;
      this.pagination = data.kids.pagination;
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadKids();
  }

  loadKids() {
    this.kidService
      .getKids(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe((res: PaginatedResult<Kid[]>) => {
        this.kids = res.result;
        this.pagination = res.pagination;
      }, error => {
        console.error(error);
      });
  }
}
