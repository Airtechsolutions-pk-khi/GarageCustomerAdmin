import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarselllistComponent } from './carselllist.component';

describe('CarselllistComponent', () => {
  let component: CarselllistComponent;
  let fixture: ComponentFixture<CarselllistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarselllistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarselllistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
