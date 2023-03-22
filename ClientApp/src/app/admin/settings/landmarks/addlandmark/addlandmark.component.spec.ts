import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddlandmarkComponent } from './addlandmark.component';

describe('AddamenitiesComponent', () => {
  let component: AddlandmarkComponent;
  let fixture: ComponentFixture<AddlandmarkComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddlandmarkComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddlandmarkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
