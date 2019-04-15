import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Kid } from 'src/app/_models/kid';
import { KidService } from 'src/app/_services/kid.service';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-kid-search',
  templateUrl: './kid-search.component.html',
  styleUrls: ['./kid-search.component.css']
})
export class KidSearchComponent implements OnInit {
  @Output() kidsSelected = new EventEmitter();
  kids: Kid[];
  kidParams: any = {};
  pagination: Pagination = { currentPage: 1, itemsPerPage: 10, totalItems: null, totalPages: null };
  selectedKids: Kid[];

  constructor(private kidService: KidService, private bsModalRef: BsModalRef) { }

  ngOnInit() {
    this.kidService.getKids(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe((response: PaginatedResult<Kid[]>) => {
        this.kids = response.result;
        this.pagination = response.pagination;
      });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadKids();
  }

  loadKids() {
    this.kidService
      .getKids(this.pagination.currentPage, this.pagination.itemsPerPage, this.kidParams)
      .subscribe((res: PaginatedResult<Kid[]>) => {
        this.kids = res.result;
        this.pagination = res.pagination;
      }, error => {
        console.error(error);
      });
  }

  selectKid(id: number, checked: boolean) {
    if (checked) {
      this.selectedKids.push(this.kids.find(x => x.id === id));
    } else {
      const index = this.selectedKids.findIndex(x => x.id === id);
      this.selectedKids.splice(index, 1);
    }
  }

  isChecked = (id: number) => this.selectedKids && this.selectedKids.findIndex(x => x.id === id) !== -1;

  save() {
    this.kidsSelected.emit(this.selectedKids);
    this.bsModalRef.hide();
  }

  cancel() {
    this.bsModalRef.hide();
  }
}
