import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddamenitiesComponent } from './addamenities.component';

describe('AddamenitiesComponent', () => {
  let component: AddamenitiesComponent;
  let fixture: ComponentFixture<AddamenitiesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddamenitiesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddamenitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
