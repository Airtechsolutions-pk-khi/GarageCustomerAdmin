import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { alternateimageComponent } from 'src/app/imageupload/alternateimage.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { DiscountService } from 'src/app/_services/discount.service';
import { LocationsService } from '../../../../_services/locations.service';
import { thumbnailimageComponent } from '../../../../imageupload/thumbnailimage.component';

@Component({
  selector: 'app-adddiscount',
  templateUrl: './adddiscount.component.html',
})
export class AdddiscountComponent implements OnInit {

  submitted = false;
  discountForm: FormGroup;
  loading = false;
  loadingCustomer = false;
  ButtonText = "Save";
  selectedSubCategoriesIds: string[];
  selectedLocationIds: string[];
  LocationList = [];
  selectedLocationID = [];
  fromTime = { hour: new Date().getHours(), minute: new Date().getMinutes() };
  toTime = { hour: new Date().getHours(), minute: new Date().getMinutes() };
  Images = [];


  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  @ViewChild(thumbnailimageComponent, { static: true }) thmbimg;
  @ViewChild(alternateimageComponent, { static: true }) arbimg;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private discountService: DiscountService,
    private locationService: LocationsService

  ) {
    this.createForm();
    this.loadLocations();
  }

  ngOnInit() {
    this.setSelectedCustomer();
  }

  get f() { return this.discountForm.controls; }

  private createForm() {
    this.discountForm = this.formBuilder.group({
      discountID: 0,
      name: ['', Validators.required],
      description: [''],
      arabicName: [''],
      arabicDescription: [''],
      toDate: ['', Validators.required],
      toTime: [''],
      backgroundColor: [''],
      fontColor: [''],
      fromDate: ['', Validators.required],
      fromTime: [''],
      statusID: [true],
      image: [''],
      thumbnailImage: [''],
      arabicImage: [''],
      locationID: [null],
      locations: [],
     // file: [''],
    });
  }

  private editForm(obj) {
    debugger
    this.f.name.setValue(obj.name);
    this.f.arabicName.setValue(obj.arabicName);
    this.f.discountID.setValue(obj.discountID);
    this.f.image.setValue(obj.image);
    this.f.arabicImage.setValue(obj.arabicImage);
    this.f.description.setValue(obj.description);
    this.f.arabicDescription.setValue(obj.arabicDescription);
    this.f.toTime.setValue(obj.toTime);
    this.f.fromTime.setValue(obj.fromTime);
    this.f.fromDate.setValue(obj.fromDate);
    this.f.toDate.setValue(obj.toDate);
    this.f.backgroundColor.setValue(obj.backgroundColor);
    this.f.fontColor.setValue(obj.fontColor);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
    this.thmbimg.thumbnailimageimageUrl = obj.thumbnailImage;
    this.arbimg.alternateimageUrl = obj.arabicImage;
    //this.arbimg.alternateimageUrl = obj.arabicImage;
    this.fromTime = { hour: new Date("1/1/1900 " + obj.fromTime).getHours(), minute: new Date("1/1/1900 " + obj.fromTime).getMinutes() };
    this.toTime = { hour: new Date("1/1/1900 " + obj.toTime).getHours(), minute: new Date("1/1/1900 " + obj.toTime).getMinutes() };
    if (obj.locations != "") {
      var stringToConvert = obj.locations;
      this.selectedLocationID = stringToConvert.split(',').map(Number);
      this.f.locations.setValue(obj.locations);
    }
  }

  setSelectedCustomer() {
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingCustomer = true;
        this.f.discountID.setValue(sid);
        this.discountService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingCustomer = false;
        });
      }
    })
  }

  private loadLocations() {
    this.locationService.getAllLocations().subscribe((res: any) => {
      this.LocationList = res;
    });
  }

  onSubmit() {
    debugger
    this.discountForm.markAllAsTouched();
    this.submitted = true;
    this.f.fromTime.setValue(this.fromTime.hour + ":" + this.fromTime.minute);
    this.f.toTime.setValue(this.toTime.hour + ":" + this.toTime.minute);
    if (this.discountForm.invalid) { return; }
    this.loading = true;
    this.f.locations.setValue(this.selectedLocationID == undefined ? "" : this.selectedLocationID.toString());
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    this.f.arabicImage.setValue(this.arbimg.alternateimageUrl);
    this.f.thumbnailImage.setValue(this.thmbimg.thumbnailimageimageUrl);

    if (parseInt(this.f.discountID.value) === 0) {
      //Insert services
      this.discountService.insert(this.discountForm.value).subscribe(data => {
        debugger
        if (data != 0) {
          debugger
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/discount']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update banner
      this.discountService.update(this.discountForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/discount']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }



}
