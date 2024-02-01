import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { alternateimageComponent } from './alternateimage.component';

describe('alternateimageComponent', () => {
  let component: alternateimageComponent;
  let fixture: ComponentFixture<alternateimageComponent>;
  alternateimageComponent
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [alternateimageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(alternateimageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
