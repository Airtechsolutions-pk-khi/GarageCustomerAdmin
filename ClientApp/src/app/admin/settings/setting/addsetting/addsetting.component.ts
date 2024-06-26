import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { alternateimageComponent } from 'src/app/imageupload/alternateimage.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { SettingService } from 'src/app/_services/setting.service';
import { LocationsService } from '../../../../_services/locations.service';


@Component({
  selector: 'app-addsetting',
  templateUrl: './addsetting.component.html',
  styleUrls: ['./addsetting.component.css']
})
export class AddsettingComponent implements OnInit {

  submitted = false;
  settingForm: FormGroup;
  loading = false;
  loadingCustomer = false;
  ButtonText = "Save";
  selectedSubCategoriesIds: string[];
  selectedLocationIds: string[];
  selectedgroupModifierIds: string[];
  LocationList = [];
  selectedLocationID = [];

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  @ViewChild(alternateimageComponent, { static: true }) altimg;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private settingService: SettingService,
    private locationService: LocationsService

  ) {
    this.createForm();
    this.loadLocations();
  }

  ngOnInit() {
    this.setSelectedCustomer();
  }

  get f() { return this.settingForm.controls; }

  private createForm() {
    debugger
    this.settingForm = this.formBuilder.group({
      id: 0,
      title: ['', Validators.required],
      description: [''],
      arabicTitle: [''],
      arabicDescription: [''],
      displayOrder: [0],
      pageName: [''],
      type: [''],
      statusID: [true],
      image: [''],
      alternateImage: [''],
      locationID: [null],
      locations: [],
    });
  }

  private editForm(obj) {
    debugger
    this.f.title.setValue(obj.title);
    this.f.arabicTitle.setValue(obj.arabicTitle);
    this.f.id.setValue(obj.id);
    this.f.image.setValue(obj.image);
    this.f.alternateImage.setValue(obj.alternateImage);
    this.f.description.setValue(obj.description);
    this.f.arabicDescription.setValue(obj.arabicDescription);
    this.f.displayOrder.setValue(obj.displayOrder);
    this.f.pageName.setValue(obj.pageName);
    this.f.type.setValue(obj.type);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
    this.altimg.alternateimageUrl = obj.alternateImage;

    if (obj.locations != "") {
      debugger
      var stringToConvert = obj.locations;
      this.selectedLocationID = stringToConvert.split(',').map(Number);
      this.f.locations.setValue(obj.locations);
    }
  }

  setSelectedCustomer() {
    debugger
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingCustomer = true;
        this.f.id.setValue(sid);
        this.settingService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingCustomer = false;
        });
      }
    })
  }

  private loadLocations() {
    debugger
    this.locationService.getAllLocations().subscribe((res: any) => {
      this.LocationList = res;
    });
  }

  onSubmit() {
    debugger
    this.settingForm.markAllAsTouched();
    this.submitted = true;
    if (this.settingForm.invalid) { return; }
    this.loading = true;
    this.f.locations.setValue(this.selectedLocationID == undefined ? "" : this.selectedLocationID.toString());
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    this.f.alternateImage.setValue(this.altimg.alternateimageUrl);


    if (parseInt(this.f.id.value) === 0) {
      debugger
      //Insert services
      console.log(JSON.stringify(this.settingForm.value));
      this.settingService.insert(this.settingForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/setting']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update banner
      this.settingService.update(this.settingForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/setting']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }



}
