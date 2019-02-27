/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { KidListComponent } from './kid-list.component';

describe('KidListComponent', () => {
  let component: KidListComponent;
  let fixture: ComponentFixture<KidListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KidListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KidListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
