import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { LocationsService } from 'src/app/_services/locations.service';
import { ToastService } from 'src/app/_services/toastservice';

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
  AmenitiesList =[];
  ServiceList =[];
  LandmarkList =[];
  Items = [];
  selectedAmenitiesID=[];
  selectedServiceID=[];
  selectedLandmarkID=[];
  ButtonText = "Save";
   
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private locationService: LocationsService

  ) {
    this.createForm();    
    this.loadAmenities();
    this.loadService();
    this.loadLandmark();
  }

  ngOnInit() {
    this.setSelectedLocations();
  }

  get f() { return this.locationForm.controls; }

  private createForm() {
    
    this.locationForm = this.formBuilder.group({
      locationID: 0,       
      name: ['', Validators.required],
      descripiton: ['', Validators.required],
      address: ['', Validators.required],
      arabicAddress: ['', Validators.required],
      contactNo: ['', Validators.required],  
      arabicName: ['', Validators.required],
      arabicDescription: ['', Validators.required],    
      email: ['', Validators.required],                  
      minOrderAmount: [0],  
      latitude: ['', Validators.required],
      longitude: ['', Validators.required],  
      statusID: [1],
      landmarkID:[],      
      gmaplink: ['', Validators.required],     
      imageURL: [''],
      lastUpdatedBy:[''],                 
      lastUpdatedDate:[''],
      isFeatured:false,
      file: [''],
      imagesSource: [''],
      locationImages: [],
      amenities:[],
      service:[],
      //landmark:[],
      amenitiesID:[null],
      serviceID:[null]
      
      
    });
  }
  onFileChange(event) {
    this.Images=this.Images??[];
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
    this.f.gmaplink.setValue(obj.gmaplink);
    this.f.landmarkID.setValue(obj.landmarkID);    
    this.f.imageURL.setValue(obj.imageURL);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.f.isFeatured.setValue(obj.isFeatured  === 1 ? true : false);    

    this.loadItemImages(this.f.locationID.value);

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
    // if (obj.landmark != "") {
    //   var stringToConvert = obj.landmark;
    //   this.selectedLandmarkID = stringToConvert.split(',').map(Number);
    //   this.f.landmark.setValue(obj.landmark);
    // }
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
   
    this.locationForm.markAllAsTouched();
    this.submitted = true;
     
    if (this.locationForm.invalid) { return; }
    this.loading = true;
    this.f.amenities.setValue(this.selectedAmenitiesID == undefined ? "" : this.selectedAmenitiesID.toString());
    this.f.service.setValue(this.selectedServiceID == undefined ? "" : this.selectedServiceID.toString());
    //this.f.landmark.setValue(this.selectedLandmarkID == undefined ? "" : this.selectedLandmarkID.toString());
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    // this.f.isFeatured.setValue(this.f.isFeatured.value === true ? 1 : 0);

   
    if (parseInt(this.f.locationID.value) === 0) {
      //Insert location
      console.log(JSON.stringify(this.locationForm.value));
      //this.locationService.insert(this.locationForm.value).subscribe(data => {
      //  this.loading = false;
      //  if (data != 0) {
      //    this.ts.showSuccess("Success", "Record added successfully.")
      //    this.router.navigate(['/admin/location']);
      //  }
        
      //}, error => {
      //  this.ts.showError("Error", "Failed to insert record.")
      //  this.loading = false;
      //});

    } else {
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
