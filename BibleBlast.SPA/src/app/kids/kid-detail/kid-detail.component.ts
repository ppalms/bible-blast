import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { KidService } from 'src/app/_services/kid.service';
import { ActivatedRoute } from '@angular/router';
import { Kid } from 'src/app/_models/kid';
import { MemoryService } from 'src/app/_services/memory.service';
import { Memory, MemoryCategory } from 'src/app/_models/memory';
import { AccordionComponent } from 'ngx-bootstrap';

@Component({
  selector: 'app-kid-detail',
  templateUrl: './kid-detail.component.html',
  styleUrls: ['./kid-detail.component.css']
})
export class KidDetailComponent implements OnInit {
  kid: Kid;
  memoriesByCategory: MemoryCategory[];

  constructor(public kidService: KidService, private memoryService: MemoryService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.kid = data.kid;
      this.memoryService.getMemoriesByCategory().subscribe(memories => this.memoriesByCategory = memories);
    });
  }

  heading = (cat: MemoryCategory) => `${cat.name} (${this.getCompletedCountByCategory(cat.id)} / ${cat.memories.length})`;

  isOpen = (cat: MemoryCategory) => cat.id === 1;

  isComplete = (memoryId: number) => this.kid.completedMemories.find(x => x.memoryId === memoryId);

  getCompletedDate = (memoryId: number) => this.isComplete(memoryId) && this.isComplete(memoryId).dateCompleted;

  getCompletedCountByCategory = (categoryId: number) => this.kid.completedMemories.filter(x => x.categoryId === categoryId).length;
}
