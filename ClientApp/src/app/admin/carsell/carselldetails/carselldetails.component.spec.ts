import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CarSelldetailsComponent } from './carselldetails.component';

describe('CarSelldetailsComponent', () => {
  let component: CarSelldetailsComponent;
  let fixture: ComponentFixture<CarSelldetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CarSelldetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CarSelldetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
