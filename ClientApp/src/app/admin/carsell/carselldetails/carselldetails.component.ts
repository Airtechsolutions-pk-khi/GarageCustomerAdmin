import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CarSells, CarSellImages, Customers } from 'src/app/_models/CarSell';
import { ToastService } from 'src/app/_services/toastservice';
import { Location } from 'src/app/_models/Location';
import { CarSellService } from 'src/app/_services/carsell.service';
import { Validators } from '@angular/forms';
@Component({
  selector: 'app-carselldetails',
  templateUrl: './carselldetails.component.html',
  providers: []
})

export class CarSelldetailsComponent implements OnInit {
  public carsell = new CarSells();
  private selectedBrand;
  Locations: Location[] = [];
  selectedLocations = [];
  locationID = 0;
  reason = [""];
  public customerInfo = new Customers();

  locationSubscription: Subscription;
  constructor(public service: CarSellService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public router: Router,
    private route: ActivatedRoute) {
    debugger
    //this.selectedBrand = this.ls.getSelectedBrand().brandID;

  }

  ngOnInit() {
    this.setSelectedOrder();

  }
  setSelectedOrder() {
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.service.getById(sid).subscribe(res => {
          //Set Forms
          debugger
          this.editForm(res);
        });
      }
    })
  }

  updateOrder(carsell, status) {
    debugger
    carsell.statusID = status;
    carsell.reason = this.reason;

    //Update customer
    this.service.updatestatus(carsell).subscribe(data => {

      if (data != 0) {
        this.ts.showSuccess("Success", "Record updated successfully.")
        this.router.navigate(['/admin/carsell']);
      }
    }, error => {
      this.ts.showError("Error", "Failed to update record.")
    });
  }
  private editForm(obj) {
    debugger
    this.carsell = obj.carsell;
    this.customerInfo = obj.customer;
  }
}
