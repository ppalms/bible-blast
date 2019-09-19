import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { KidService } from 'src/app/_services/kid.service';
import { KidMemoryListItem, KidMemoryCategory } from 'src/app/_models/memory';
import { Kid, CompletedMemory } from 'src/app/_models/kid';

@Component({
  selector: 'app-kid-detail',
  templateUrl: './kid-detail.component.html',
  styleUrls: ['./kid-detail.component.scss']
})
export class KidDetailComponent implements OnInit {
  kid: Kid;
  memoriesByCategory: KidMemoryCategory[];
  memoryForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig> = { dateInputFormat: 'MM/DD/YYYY' };

  constructor(
    public kidService: KidService, private route: ActivatedRoute, private formBuilder: FormBuilder,
    private router: Router, private alertify: AlertifyService
  ) { }

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

    this.memoryForm = this.formBuilder.group({
      kidId: this.kid.id,
      memoriesByCategory: this.formBuilder.array(
        this.memoriesByCategory.map(c =>
          this.formBuilder.group({
            id: c.id,
            name: c.name,
            memories: this.formBuilder.array(
              c.memories.map(m =>
                this.formBuilder.group({
                  id: m.id,
                  name: m.name,
                  description: m.description,
                  points: m.points,
                  dateCompleted: m.dateCompleted
                })
              )
            )
          })
        )
      )
    });
  }

  getBadgeText = (memories: KidMemoryListItem[]) => `${memories.filter(m => m.dateCompleted).length} / ${memories.length}`;

  isOpen = (category: KidMemoryCategory) => category.id === 1;

  calculateTotalPoints = (memories: KidMemoryListItem[]) => memories.filter(m => m.dateCompleted)
    .map(m => m.points).reduce((total, current) => total += current, 0)

  setCompleted = (memory: FormGroup, checked: boolean) => {
    const currentDate = new Date();
    currentDate.setHours(0, 0, 0, 0);

    if (checked) {
      memory.patchValue({ dateCompleted: currentDate });
    } else {
      memory.patchValue({ dateCompleted: null });
    }

    memory.markAsDirty();
  }

  updateKidMemories = () => {
    const completedMemories = this.memoryForm.value
      .memoriesByCategory.map((c: KidMemoryCategory) => c.memories)
      .reduce((prev: KidMemoryListItem[], curr: KidMemoryListItem[]) => prev.concat(curr))
      .filter((m: KidMemoryListItem) => m.dateCompleted);

    const completedToUpsert = [];
    const completedToDelete = [];

    completedMemories.forEach((kidMemory: KidMemoryListItem) =>
      completedToUpsert.push({ memoryId: kidMemory.id, dateCompleted: kidMemory.dateCompleted })
    );

    this.kid.completedMemories.forEach((kidMemory: CompletedMemory) => {
      if (!completedMemories.some((m: KidMemoryListItem) => m.id === kidMemory.memoryId)) {
        completedToDelete.push({ memoryId: kidMemory.memoryId, dateCompleted: kidMemory.dateCompleted });
      }
    });

    this.kidService.upsertKidMemories(this.kid.id, completedToUpsert).subscribe(() => {
      this.kidService.deleteKidMemories(this.kid.id, completedToDelete).subscribe(() => {
        this.memoryForm.markAsPristine();
        this.alertify.success('Memory items saved successfully');
      }, console.error);
    }, console.error);
  }

  goBack = () => this.router.navigate(['/kids']);

  sortMemories = (a: KidMemoryListItem, b: KidMemoryListItem) => {
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
