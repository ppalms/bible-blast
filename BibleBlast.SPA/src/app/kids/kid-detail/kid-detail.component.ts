import { Component, OnInit } from '@angular/core';
import { KidService } from 'src/app/_services/kid.service';
import { ActivatedRoute } from '@angular/router';
import { Kid } from 'src/app/_models/kid';
import { MemoryService } from 'src/app/_services/memory.service';
import { Memory, MemoryCategory } from 'src/app/_models/memory';

@Component({
  selector: 'app-kid-detail',
  templateUrl: './kid-detail.component.html',
  styleUrls: ['./kid-detail.component.css']
})
export class KidDetailComponent implements OnInit {
  kid: Kid;
  memoriesByCategory: MemoryCategory[];
  totalPoints = 0;

  constructor(public kidService: KidService, private memoryService: MemoryService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.kid = data.kid;
      this.memoryService.getMemoriesByCategory().subscribe(memories => {
        this.memoriesByCategory = memories;
        this.memoriesByCategory.forEach(category => category.memories.sort(this.sortMemories));
      });
    });
  }

  heading = (cat: MemoryCategory) => `${cat.name} (${this.kid.completedMemories
    .filter(x => x.categoryId === cat.id).length} / ${cat.memories.length})`

  isOpen = (cat: MemoryCategory) => cat.id === 1;

  getCompletedMemory = (memoryId: number) => this.kid.completedMemories.find(x => x.memoryId === memoryId);

  getCompletedDate = (memoryId: number) => this.getCompletedMemory(memoryId) && this.getCompletedMemory(memoryId).dateCompleted;

  getCompletedCountByCategory = (categoryId: number) => this.kid.completedMemories.filter(x => x.categoryId === categoryId).length;

  getTotalPoints = (categoryId: number) => this.kid.completedMemories
    .filter(x => x.categoryId === categoryId).map(x => x.points)
    .reduce((total, current) => total += current, 0)

  sortMemories = (a: Memory, b: Memory) => {
    const nameA = a.name.toUpperCase();
    const nameB = b.name.toUpperCase();
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
