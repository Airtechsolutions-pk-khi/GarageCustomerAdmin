import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BodyTypeComponent } from './bodytype.component';

describe('BodyTypeComponent', () => {
  let component: BodyTypeComponent;
  let fixture: ComponentFixture<BodyTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [BodyTypeComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BodyTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
