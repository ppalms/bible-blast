import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KidMemoryCategoryComponent } from './kid-memory-category.component';

describe('KidMemoryCategoryComponent', () => {
  let component: KidMemoryCategoryComponent;
  let fixture: ComponentFixture<KidMemoryCategoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KidMemoryCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KidMemoryCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
