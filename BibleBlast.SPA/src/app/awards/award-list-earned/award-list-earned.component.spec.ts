import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AwardListEarnedComponent } from './award-list-earned.component';

describe('AwardListEarnedComponent', () => {
  let component: AwardListEarnedComponent;
  let fixture: ComponentFixture<AwardListEarnedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AwardListEarnedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AwardListEarnedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
