import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { LandmarkService } from 'src/app/_services/landmark.service';


@Component({
  selector: 'app-addlandmark',
  templateUrl: './addlandmark.component.html',
  styleUrls: ['./addlandmark.component.css']
})
export class AddlandmarkComponent implements OnInit {

  submitted = false;
  landmarkForm: FormGroup;
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
    private landmarkService: LandmarkService

  ) {
    this.createForm();
  }

  ngOnInit() {
    debugger
     this.setSelectedCustomer();
  }

  get f() { return this.landmarkForm.controls; }

  private createForm() {
    this.landmarkForm = this.formBuilder.group({
      landmarkID: 0,
      name: ['', Validators.required],      
      statusID: [true],      
      image: [''],            
    });
  }

  private editForm(obj) {
    this.f.name.setValue(obj.name);
     
    this.f.landmarkID.setValue(obj.landmarkID);
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
        this.f.landmarkID.setValue(sid);
        this.landmarkService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingCustomer = false;
        });
      }
    })
  }

  onSubmit() {
    debugger
    this.landmarkForm.markAllAsTouched();
    this.submitted = true;
    if (this.landmarkForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    

    if (parseInt(this.f.landmarkID.value) === 0) {
      //Insert landmark
      console.log(JSON.stringify(this.landmarkForm.value));
      this.landmarkService.insert(this.landmarkForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/landmarks']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update banner
      this.landmarkService.update(this.landmarkForm.value).subscribe(data => {
        debugger
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/landmarks']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }



}
