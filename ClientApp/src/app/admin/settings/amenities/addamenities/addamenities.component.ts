import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { AmenitiesService } from 'src/app/_services/amenities.service';


@Component({
  selector: 'app-addamenities',
  templateUrl: './addamenities.component.html',
  styleUrls: ['./addamenities.component.css']
})
export class AddamenitiesComponent implements OnInit {

  submitted = false;
  amenitiesForm: FormGroup;
  loading = false;
  loadingCustomer = false;
  ButtonText = "Save"; 
  selectedSubCategoriesIds: string[];
  selectedLocationIds: string[];
  selectedgroupModifierIds: string[];

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private amenitiesService: AmenitiesService

  ) {
    this.createForm();
  }

  ngOnInit() {
     this.setSelectedCustomer();
  }

  get f() { return this.amenitiesForm.controls; }

  private createForm() {
    this.amenitiesForm = this.formBuilder.group({
      amenitiesID: 0,
      name: ['', Validators.required],   
      arabicName:[''],   
      statusID: [true],      
      image: [''],            
    });
  }

  private editForm(obj) {
    this.f.name.setValue(obj.name);
    this.f.arabicName.setValue(obj.arabicName);
    this.f.amenitiesID.setValue(obj.amenitiesID);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedCustomer() {
    debugger
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingCustomer = true;
        this.f.amenitiesID.setValue(sid);
        this.amenitiesService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingCustomer = false;
        });
      }
    })
  }

  onSubmit() {
    debugger
    this.amenitiesForm.markAllAsTouched();
    this.submitted = true;
    if (this.amenitiesForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    

    if (parseInt(this.f.amenitiesID.value) === 0) {
      //Insert amenities
      console.log(JSON.stringify(this.amenitiesForm.value));
      this.amenitiesService.insert(this.amenitiesForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/amenities']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update banner
      this.amenitiesService.update(this.amenitiesForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/amenities']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }



}
