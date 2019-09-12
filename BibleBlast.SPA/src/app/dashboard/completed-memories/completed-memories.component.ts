import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-completed-memories',
  templateUrl: './completed-memories.component.html',
  styleUrls: ['./completed-memories.component.scss']
})
export class CompletedMemoriesComponent implements OnInit {
  @Input() category: any;

  constructor() { }

  ngOnInit() {
  }
}
