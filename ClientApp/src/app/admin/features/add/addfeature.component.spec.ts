import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddfeatureComponent } from './addfeature.component';

describe('AddfeatureComponent', () => {
  let component: AddfeatureComponent;
  let fixture: ComponentFixture<AddfeatureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddfeatureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddfeatureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
