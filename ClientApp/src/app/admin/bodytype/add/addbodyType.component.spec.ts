import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddbodyTypeComponent } from './addbodyType.component';

describe('AddbodyTypeComponent', () => {
  let component: AddbodyTypeComponent;
  let fixture: ComponentFixture<AddbodyTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddbodyTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddbodyTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
