import { Component, OnInit } from '@angular/core';
import { KidService } from 'src/app/_services/kid.service';
import { ActivatedRoute, Router } from '@angular/router';
import { KidMemoryListItem as KidMemoryListItem, KidMemoryCategory } from 'src/app/_models/memory';
import { FormGroup, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { Kid } from 'src/app/_models/kid';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-kid-detail',
  templateUrl: './kid-detail.component.html',
  styleUrls: ['./kid-detail.component.scss']
})
export class KidDetailComponent implements OnInit {
  kid: Kid;
  memoryForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig> = { dateInputFormat: 'YYYY-MM-DD' };

  constructor(
    public kidService: KidService, private route: ActivatedRoute, private formBuilder: FormBuilder,
    private router: Router, private alertify: AlertifyService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.kid = data.kid;
      this.kid.memoriesByCategory.forEach(c => c.memories.sort(this.sortMemories));
    });

    this.memoryForm = this.formBuilder.group({
      kidId: this.kid.id,
      memoriesByCategory: this.formBuilder.array(
        this.kid.memoriesByCategory.map(c =>
          this.formBuilder.group({
            categoryId: c.categoryId,
            categoryName: c.categoryName,
            memories: this.formBuilder.array(
              c.memories.map(m =>
                this.formBuilder.group({
                  memoryId: m.memoryId,
                  memoryName: m.memoryName,
                  memoryDescription: m.memoryDescription,
                  points: m.points,
                  dateCompleted: m.dateCompleted
                }))
            )
          })
        )
      )
    });
  }

  getBadgeText = (memories: KidMemoryListItem[]) => `${memories.filter(m => m.dateCompleted).length} / ${memories.length}`;

  isOpen = (category: KidMemoryCategory) => category.categoryId === 1;

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

    completedMemories.forEach(memory => completedToUpsert.push({ memoryId: memory.memoryId, dateCompleted: memory.dateCompleted }));

    this.kid.completedMemories.forEach(memory => {
      if (!completedMemories.some((m: KidMemoryListItem) => m.memoryId === memory.memoryId)) {
        completedToDelete.push({ memoryId: memory.memoryId, dateCompleted: memory.dateCompleted });
      }
    });

    this.kidService.upsertKidMemories(this.kid.id, completedToUpsert).subscribe(() => {
      this.kidService.deleteKidMemories(this.kid.id, completedToDelete).subscribe(() => {
        this.memoryForm.markAsPristine();
        this.alertify.success('Memory items saved successfully');
      }, error => console.log(error));
    }, error => {
      console.log(error);
    });
  }

  goBack = () => this.router.navigate(['/kids']);

  sortMemories = (a: KidMemoryListItem, b: KidMemoryListItem) => {
    const nameA = a.memoryName.toUpperCase();
    const nameB = b.memoryName.toUpperCase();
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
