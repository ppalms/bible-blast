/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UserKidListComponent } from './user-kid-list.component';

describe('UserKidListComponent', () => {
  let component: UserKidListComponent;
  let fixture: ComponentFixture<UserKidListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserKidListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserKidListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
