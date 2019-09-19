import { Component, Input, OnInit } from '@angular/core';
import { Kid } from 'src/app/_models/kid';
import { Router } from '@angular/router';
import { MemoryCategory } from 'src/app/_models/memory';

@Component({
  selector: 'app-kid-card',
  templateUrl: './kid-card.component.html',
  styleUrls: ['./kid-card.component.scss']
})
export class KidCardComponent implements OnInit {
  @Input() kid: Kid;
  @Input() memoryCategories: MemoryCategory[];

  totalPoints = 0;
  totalCompleteMemories = 0;
  // todo
  max = 171;

  constructor(private router: Router) { }

  ngOnInit() {
    if (this.kid.completedMemories.length === 0) {
      return;
    }

    this.totalCompleteMemories = this.kid.completedMemories.length;
    this.totalPoints = this.kid.completedMemories
      .map(m => m.points).reduce((total, current) => total += current);
  }

  completedMemories = (categoryId: number) =>
    `${this.kid.completedMemories.filter(x => x.categoryId === categoryId).length}
      / ${this.memoryCategories.find(c => c.id === categoryId).memories.length}`

  navigateToDetail = () => this.router.navigate([`/kids/${this.kid.id}`]);
}
