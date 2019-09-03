import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompletedMemoriesComponent } from './completed-memories.component';

describe('CompletedMemoriesComponent', () => {
  let component: CompletedMemoriesComponent;
  let fixture: ComponentFixture<CompletedMemoriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompletedMemoriesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompletedMemoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
