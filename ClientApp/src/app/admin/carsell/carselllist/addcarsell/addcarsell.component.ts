import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CarSellService } from 'src/app/_services/carsell.service';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { LocationsService } from 'src/app/_services/locations.service';
import { ToastService } from 'src/app/_services/toastservice';
import { BodyTypeService } from '../../../../_services/bodyType.service';

@Component({
  selector: 'app-addcarsell',
  templateUrl: './addcarsell.component.html',
  styleUrls: ['./addcarsell.component.css']
})
export class AddcarsellComponent implements OnInit {
  submitted = false;
  carSellForm: FormGroup;
  loading = false;

  Images = [];
  FeaturesList = [];
  BodyTypeList = [];
  ModelList = [];
  MakeList = [];
  CountryList = [];

  CityList = [];

  selectedFeatureID : string[];
  selectedBodyTypeID = [];
  selectedModelID = [];
  selectedMakeID = [];
  selectedCityID = [];
  YearList = ['2023', '2022', '2021', '2020', '2019', '2018', '2017', '2016', '2015', '2014', '2013', '2012', '2011', '2010', '2009', '2008', '2007', '2006', '2005', '2004', '2003', '2002', '2001', '2000', '1999', '1998', '1997', '1996', '1995', '1994', '1993', '1992', '1991', '1990'];
  ButtonText = "Save";

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private carsellService: CarSellService

  ) {
    this.createForm();
    this.loadCarSellFeature();
    this.loadBodyType();
    //this.loadModel();
    this.loadMake();
    this.loadCountry();
    //this.loadCity();
  }

  ngOnInit() {
    this.setSelectedCarSell();
  }

  get f() { return this.carSellForm.controls; }

  private createForm() {

    this.carSellForm = this.formBuilder.group({
      carSellID: 0,
      customerID: [1],
      name: ['', Validators.required],
      description: ['', Validators.required],
      address: ['', Validators.required],
      statusID: [1],
      createdBy: [],
      updatedBy: [],
      isInspected: [false],
      imagesSource: ['', Validators.required],
      features: ['', Validators.required],
      registrationNo: ['', Validators.required],
      bodyTypeID: ['', Validators.required],
      bodyType: [''],
      fuelType: ['', Validators.required],
      engineType: ['', Validators.required],
      kilometer: ['', Validators.required],
      year: ['', Validators.required],
      makeID: [, Validators.required],
      modelID: [, Validators.required],
      transmition: ['', Validators.required],
      price: [0, Validators.required],
      cityID: [, Validators.required],
      countryCode: ['', Validators.required],
      carSellAddID: 0,
      bodyColor: ['', Validators.required],
      assembly: ['', Validators.required],
      file: ['', Validators.required],

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
            this.carSellForm.patchValue({
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
    this.f.address.setValue(obj.address);
    this.f.description.setValue(obj.description);
    this.f.assembly.setValue(obj.assembly);
    this.f.cityID.setValue(obj.cityID);
    this.f.bodyColor.setValue(obj.bodyColor);
    this.f.countryCode.setValue(obj.countryCode);
    this.f.price.setValue(obj.price);
    this.f.transmition.setValue(obj.transmition);
    this.f.modelID.setValue(obj.modelID);
    this.f.makeID.setValue(obj.makeID);
    this.f.year.setValue(obj.year);
    this.f.kilometer.setValue(obj.kilometer);
    this.f.fuelType.setValue(obj.fuelType);
    this.f.engineType.setValue(obj.engineType);
    this.f.bodyTypeID.setValue(obj.bodyTypeID);
    this.f.registrationNo.setValue(obj.registrationNo);
    this.f.statusID.setValue(obj.statusID == 1 ? true : false);
    //this.f.features.setValue(obj.features);

    this.loadCarSellImages(this.f.carSellID.value);

    if (obj.features != "") {
      debugger
      var stringToConvert = obj.features;
      this.selectedFeatureID = stringToConvert.split(',').map(Number);
      this.f.features.setValue(obj.features);
    }
    if (obj.countryCode != "")
    {
      this.carsellService.loadCity(obj.countryCode).subscribe((res: any) => {
        this.CityList = res;
      });
    }
    if (obj.makeID != "") {
      this.carsellService.loadModel(obj.makeID).subscribe((res: any) => {
        this.ModelList = res;
      });
    }
  }
  private loadCarSellImages(id) {

    this.carsellService.loadCarSellImages(id).subscribe((res: any) => {
      this.Images = res;
      this.f.imagesSource.setValue(this.Images);
    });
  }
  removeImage(obj) {
    const index = this.Images.indexOf(obj);
    this.Images.splice(index, 1);

    this.f.imagesSource.setValue(this.Images);
  }
  private loadCarSellFeature() {
    this.carsellService.loadFeature().subscribe((res: any) => {
      this.FeaturesList = res;
    });
  }
  private loadBodyType() {
    debugger
    this.carsellService.loadBodyType().subscribe((res: any) => {
      this.BodyTypeList = res;
    });
  }

  private loadMake() {
    this.carsellService.loadMake().subscribe((res: any) => {
      this.MakeList = res;
    });
  }
  private loadCountry() {
    this.carsellService.loadCountry().subscribe((res: any) => {
      this.CountryList = res;
    });
  }

  onSelect(event) {
    debugger;
    let selectElementValue = event.target.value;
    let [index, value] = selectElementValue.split(':').map(item => item.trim());
    console.log(index);
    console.log(value);

    this.carsellService.loadCity(value).subscribe((res: any) => {
      this.CityList = res;
    });
  }

  onChange(event) {
    debugger;
    let selectElementValue = event.target.value;
    let [index, value] = selectElementValue.split(':').map(item => item.trim());
    console.log(index);
    console.log(value);

    this.carsellService.loadModel(value).subscribe((res: any) => {
      this.ModelList = res;
    });
  }
  setSelectedCarSell() {
      debugger
      this.route.paramMap.subscribe(param => {
        const sid = +param.get('id');
        if (sid) {
          this.loading = true;
          this.f.carSellID.setValue(sid);
          this.carsellService.getcarId(sid).subscribe(res => {
            //Set Forms
            this.editForm(res);
            this.loading = false;
          });
        }
      })
    }

  onSubmit() {
    debugger
    this.carSellForm.markAllAsTouched();
    this.submitted = true;
    if (this.carSellForm.invalid) { return; }
    this.loading = true;
    this.f.features.setValue(this.selectedFeatureID == undefined ? "" : this.selectedFeatureID.toString());
    if (parseInt(this.f.carSellID.value) === 0) {
      //Insert location
      this.carsellService.insert(this.carSellForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/carsell']);
        }

      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update location
      this.carsellService.update(this.carSellForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/carsell']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
}
