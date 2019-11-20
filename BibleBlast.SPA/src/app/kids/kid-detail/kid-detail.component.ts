import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { KidService } from 'src/app/_services/kid.service';
import { KidMemoryItem, KidMemoryCategory } from 'src/app/_models/memory';
import { Kid, CompletedMemory } from 'src/app/_models/kid';

@Component({
  selector: 'app-kid-detail',
  templateUrl: './kid-detail.component.html',
  styleUrls: ['./kid-detail.component.scss']
})
export class KidDetailComponent implements OnInit {
  kid: Kid;
  memoriesByCategory: KidMemoryCategory[];
  completedMemories: any[] = [];

  constructor(public router: Router, private kidService: KidService, private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.kid = data.kid;
      this.memoriesByCategory = data.memoryCategories;

      this.memoriesByCategory.map(category => {
        category.memories.map(memory => {
          const completedMemory = this.kid.completedMemories.find(cm => cm.memoryId === memory.id);
          if (completedMemory) {
            memory.dateCompleted = completedMemory.dateCompleted;
          }
        });
      });

      this.memoriesByCategory.forEach(c => c.memories.sort(this.sortMemories));
    });
  }

  getBadgeText = (memories: KidMemoryItem[]) => `${memories.filter(m => m.dateCompleted).length} / ${memories.length}`;

  updateKidMemories = () => {
    const completedToUpsert = this.completedMemories.filter(x => x.action === 'upsert');
    const completedToDelete = this.completedMemories.filter(x => x.action === 'delete');

    this.kidService.upsertKidMemories(this.kid.id, completedToUpsert).subscribe(() => {
      this.kidService.deleteKidMemories(this.kid.id, completedToDelete).subscribe(() => {
        this.alertify.success('Memory items saved successfully');
      }, console.error);
    }, console.error);
  }

  handleKidMemoryChanged(completedMemory: any) {
    this.completedMemories = this.completedMemories.filter(x => x.memoryId !== completedMemory.memoryId);
    if (completedMemory.action === 'none') {
      return;
    }

    this.completedMemories.push({
      memoryId: completedMemory.memoryId,
      dateCompleted: completedMemory.dateCompleted,
      action: completedMemory.action
    });
  }

  sortMemories = (a: KidMemoryItem, b: KidMemoryItem) => {
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
