import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CarSells, CarSellImages, Customers, Make, Models, Country, City, Feature } from 'src/app/_models/CarSell';
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
  public makeInfo = new Make();
  public modelInfo = new Models();
  public countryInfo = new Country();
  public cityInfo = new City();
  public featureInfo = new Feature();
  public images = new CarSellImages();

  locationSubscription: Subscription;
  constructor(public service: CarSellService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public router: Router,
    private route: ActivatedRoute) {
    debugger
    //this.selectedBrand = this.ls.getSelectedBrand().brandID;
    this.loadCarSellImages(this.carsell.carSellID);
  }

  ngOnInit() {
    this.setSelectedOrder();

  }
  setSelectedOrder() {
    debugger
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
    this.makeInfo = obj.make;
    this.modelInfo = obj.model;
    this.countryInfo = obj.country;
    this.cityInfo = obj.city;
    this.featureInfo = obj.feature;
    this.images = obj.image;
  }
  private loadCarSellImages(carsell) {
    debugger
    this.service.loadCarSellImages(carsell).subscribe((res: any) => {
      this.images = res;
      //this.f.imagesSource.setValue(this.Images);
    });
  }
}
