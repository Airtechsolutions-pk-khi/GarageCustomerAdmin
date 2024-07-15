import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { LocationsService } from 'src/app/_services/locations.service';
import { ToastService } from 'src/app/_services/toastservice';
import { LocationTimings, ArabicTimings } from '../../../../_models/Location';
import { CarSellService } from 'src/app/_services/carsell.service';
import { ImageuploadComponent } from '../../../../imageupload/imageupload.component';

@Component({
  selector: 'app-addlocation',
  templateUrl: './addlocation.component.html',
  styleUrls: ['./addlocation.component.css']
})
export class AddlocationComponent implements OnInit {
  submitted = false;
  locationForm: FormGroup;
  loading = false;
  loadingLocations = false;
  Images = [];
  Image = [];
  AmenitiesList = [];
  CountryList = [];
  CityList = [];
  ServiceList = [];
  LandmarkList = [];
  Items = [];
  selectedAmenitiesID = [];
  selectedServiceID = [];
  selectedLandmarkID = [];
  ButtonText = "Save";
  timings = [];
  public locationTimings: Array<Object> = [
    { name: 'Sunday', time: '', aName: 'الأحد', aTime: '' },
    { name: 'Monday', time: '', aName: 'الإثنين', aTime: '' },
    { name: 'Tuesday', time: '', aName: 'الثلاثاء', aTime: '' },
    { name: 'Wednesday', time: '', aName: 'الأربعاء', aTime: ''},
    { name: 'Thursday', time: '', aName: 'الخميس', aTime: '' },
    { name: 'Friday', time: '', aName: 'الجمعة', aTime: ''},
    { name: 'Saturday', time: '', aName: 'السبت', aTime: '' }
  ];
  time = [];
  public arabicTimings: Array<Object> = [
    { aName: 'الأحد', aTime: '' },
    { aName: 'الإثنين', aTime: '' },
    { aName: 'الثلاثاء', aTime: '' },
    { aName: 'الأربعاء', aTime: '' },
    { aName: 'الخميس', aTime: '' },
    { aName: 'الجمعة', aTime: '' },
    { aName: 'السبت', aTime: '' }
  ];
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private locationService: LocationsService,
    private service: CarSellService,

  ) {
    this.createForm();
    this.loadAmenities();
    this.loadService();
    this.loadLandmark();
    this.loadCountry();
  }

  ngOnInit() {
    this.setSelectedLocations();
  }

  get f() { return this.locationForm.controls; }


  private createForm() {

    this.locationForm = this.formBuilder.group({
      locationID: 0,
      userID: 0,
      name: ['', Validators.required],
      descripiton: ['', Validators.required],
      address: ['', Validators.required],
      arabicAddress: [''],
      contactNo: ['', Validators.required],
      arabicName: ['', Validators.required],
      arabicDescription: ['', Validators.required],
      email: ['', Validators.required],
      minOrderAmount: [0],
      latitude: ['', Validators.required],
      longitude: ['', Validators.required],
      statusID: [1],
      customerStatusID: [1],
      landmarkID: [],
      businessType: [''],
      gmaplink: [''],
      lastUpdatedBy: [''],
      lastUpdatedDate: [''],
      isFeatured: false,
      file: [''],
      imagesSource: [''],
      locationImages: [],
      amenities: [],
      service: [],
      amenitiesID: [null],
      serviceID: [null],
      locationTimings: [],
      //arabicTimings: [],
      cityID: 0,
      countryID: ['', Validators.required],
      brandThumbnailImage: [''],
    });
  }

  onFileChange(event) {
    this.Images = this.Images ?? [];
    if (event.target.files && event.target.files[0]) {
      var filesAmount = event.target.files.length;
      for (let i = 0; i < filesAmount; i++) {
        var reader = new FileReader();
        var fileSize = event.target.files[i].size / 100000;
        if (fileSize > 5) { alert("Filesize exceed 500 KB"); }
        else {

          reader.onload = (event: any) => {
            console.log(event.target.result);
            this.Images.push(event.target.result);
            this.locationForm.patchValue({
              imagesSource: this.Images
            });
          }
          reader.readAsDataURL(event.target.files[i]);
        }
      }
    }
  }

  private editForm(obj) {
    debugger
    this.f.name.setValue(obj.name);
    this.f.arabicName.setValue(obj.arabicName);
    this.f.email.setValue(obj.email);
    this.f.address.setValue(obj.address);
    this.f.arabicAddress.setValue(obj.arabicAddress);
    this.f.descripiton.setValue(obj.descripiton);
    this.f.arabicDescription.setValue(obj.arabicDescription);
    this.f.contactNo.setValue(obj.contactNo);
    this.f.minOrderAmount.setValue(obj.minOrderAmount);
    this.f.latitude.setValue(obj.latitude);
    this.f.longitude.setValue(obj.longitude);
    this.f.locationID.setValue(obj.locationID);
    this.f.userID.setValue(obj.userID);
    this.f.gmaplink.setValue(obj.gmaplink);
    this.f.landmarkID.setValue(obj.landmarkID);
    this.f.businessType.setValue(obj.businessType);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.f.customerStatusID.setValue(obj.customerStatusID === 1 ? true : false);
    this.f.isFeatured.setValue(obj.isFeatured === 1 ? true : false);

    this.f.brandThumbnailImage.setValue(obj.brandThumbnailImage);
    this.imgComp.imageUrl = obj.brandThumbnailImage;

    this.loadItemImages(this.f.locationID.value);
    this.f.locationTimings.setValue(obj.locationTimings);
    this.locationTimings = obj.locationTimings;
    //this.f.arabicTimings.setValue(obj.arabicTimings);
    //this.arabicTimings = obj.arabicTimings;
    this.f.brandThumbnailImage.setValue(obj.brandThumbnailImage);

    if (obj.amenities != "") {
      var stringToConvert = obj.amenities;
      this.selectedAmenitiesID = stringToConvert.split(',').map(Number);
      this.f.amenities.setValue(obj.amenities);
    }
    if (obj.service != "") {
      var stringToConvert = obj.service;
      this.selectedServiceID = stringToConvert.split(',').map(Number);
      this.f.service.setValue(obj.service);
    }

    this.f.cityID.setValue(obj.cityID);
    if (obj.countryID != "") {
      this.loadCity(obj.countryID, 1);
    }

  }
  private loadCountry() {
    this.service.loadCountry().subscribe((res: any) => {
      this.CountryList = res;
      if (!this.CountryList || this.CountryList.length === 0) {
        this.CountryList = [{ name: 'Saudia Arabia', code: 'SA' }];
      }
      this.f.countryID.setValue('SA');
      this.loadCity(this.f.countryID.value, 0);
    });
  }

  onSelect(event) {
    debugger
    let selectElementValue = event.target.value;
    let [index, value] = selectElementValue.split(':').map(item => item.trim());
    this.loadCity(value, 1);
    console.log(index);
    console.log(value);
  }
  loadCity(obj, type) {
    debugger
    this.service.loadCity(obj).subscribe((res: any) => {
      this.CityList = res;
      //debugger
      //if (type == 0)
      //  this.f.cityID.setValue(res[0].id);
      //else if (type == 1)
      //  debugger
      //  //var cityID = this.f.cityID;
      //this.f.cityID.setValue(this.f.cityID);
    });
  }
  private loadItemImages(id) {
    this.locationService.loadLocationImages(id).subscribe((res: any) => {
      this.Images = res;
      this.f.imagesSource.setValue(this.Images);
    });
  }

  removeImage(obj) {
    const index = this.Images.indexOf(obj);
    this.Images.splice(index, 1);
    this.f.imagesSource.setValue(this.Images);
  }
  private loadAmenities() {
    this.locationService.loadAmenities().subscribe((res: any) => {
      this.AmenitiesList = res;
    });
  }
  private loadService() {
    this.locationService.loadService().subscribe((res: any) => {
      this.ServiceList = res;
    });
  }
  private loadLandmark() {
    this.locationService.loadLandmark().subscribe((res: any) => {
      this.Items = res;
    });
  }
  setSelectedLocations() {
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingLocations = true;
        this.f.locationID.setValue(sid);
        this.locationService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingLocations = false;
        });
      }
    })
  }
  onSubmit() {
    debugger
    this.locationForm.markAllAsTouched();
    this.submitted = true;
    if (this.locationForm.invalid) { return; }
    this.loading = true;
    this.f.locationTimings.setValue(this.locationTimings);
    //this.f.arabicTimings.setValue(this.arabicTimings);
    this.f.amenities.setValue(this.selectedAmenitiesID == undefined ? "" : this.selectedAmenitiesID.toString());
    this.f.service.setValue(this.selectedServiceID == undefined ? "" : this.selectedServiceID.toString());
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.customerStatusID.setValue(this.f.customerStatusID.value === true ? 1 : 2);
    this.f.isFeatured.setValue(this.f.isFeatured.value === 1 ? true : false);
    this.f.brandThumbnailImage.setValue(this.imgComp.imageUrl);
    if (parseInt(this.f.locationID.value) === 0) {
      //Insert location
      console.log(JSON.stringify(this.locationForm.value));
    }
    else {
      //Update location     
      this.locationService.update(this.locationForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/location']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
}
