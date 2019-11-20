import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/ngx-bootstrap-datepicker';
import { FormGroup } from '@angular/forms';
import { CompletedMemory } from 'src/app/_models/kid';

@Component({
  selector: 'app-kid-memory-item',
  templateUrl: './kid-memory-item.component.html',
  styleUrls: ['./kid-memory-item.component.scss']
})
export class KidMemoryItemComponent implements OnInit {
  @Input() memoryForm: FormGroup;
  @Output() completedChanged = new EventEmitter<any>();
  originalValue: Date;
  bsConfig: Partial<BsDatepickerConfig> = { dateInputFormat: 'MM/DD/YYYY' };

  constructor() { }

  ngOnInit() {
    this.originalValue = this.memoryForm.get('dateCompleted').value;

    this.memoryForm.valueChanges.subscribe((m: CompletedMemory) => {
      this.completedChanged.emit({
        memoryId: m.memoryId,
        dateCompleted: m.dateCompleted,
        action: getAction(m.dateCompleted || false),
      });
    }, console.error);

    const getAction = completedMemoryExists =>
      this.memoryForm.pristine ? 'none' : completedMemoryExists ? 'upsert' : 'delete';
  }

  setCompleted(complete: boolean) {
    const currentDate = new Date();
    currentDate.setHours(0, 0, 0, 0);

    if (currentDate === this.originalValue || (!complete && !this.originalValue)) {
      this.memoryForm.markAsPristine();
    } else {
      this.memoryForm.markAsDirty();
    }

    this.memoryForm.patchValue({ dateCompleted: complete ? currentDate : null });
  }
}
