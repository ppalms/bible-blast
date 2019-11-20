import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { KidMemoryCategory } from 'src/app/_models/memory';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/ngx-bootstrap-datepicker';
import { CompletedMemory } from 'src/app/_models/kid';

@Component({
  selector: 'app-kid-memory-category',
  templateUrl: './kid-memory-category.component.html',
  styleUrls: ['./kid-memory-category.component.scss']
})
export class KidMemoryCategoryComponent implements OnInit {
  @Input() kidMemoryCategory: KidMemoryCategory;
  @Output() kidMemoryChanged = new EventEmitter<any>();
  categoryForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig> = { dateInputFormat: 'MM/DD/YYYY' };

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.categoryForm = this.formBuilder.group({
      memories: this.formBuilder.array(
        this.kidMemoryCategory.memories.map(memory =>
          this.formBuilder.group({
            memoryId: memory.id,
            name: memory.name,
            description: memory.description,
            dateCompleted: memory.dateCompleted
          })
        )
      )
    });
  }

  onCompletedChanged = (completedMemory: CompletedMemory) => this.kidMemoryChanged.emit(completedMemory);

  get memories(): FormArray { return this.categoryForm.get('memories') as FormArray; }
}
