import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KidMemoryItemComponent } from './kid-memory-item.component';

describe('KidMemoryItemComponent', () => {
  let component: KidMemoryItemComponent;
  let fixture: ComponentFixture<KidMemoryItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KidMemoryItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KidMemoryItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
