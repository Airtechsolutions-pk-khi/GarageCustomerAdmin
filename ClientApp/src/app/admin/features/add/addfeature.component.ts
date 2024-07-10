import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { FeaturesService } from 'src/app/_services/features.service';


@Component({
  selector: 'app-addfeature',
  templateUrl: './addfeature.component.html',
})
export class AddfeatureComponent implements OnInit {

  submitted = false;
  featureForm: FormGroup;
  loading = false;
  loadingCustomer = false;
  ButtonText = "Save";

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private featureService: FeaturesService,

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedFeature();
  }

  get f() { return this.featureForm.controls; }

  private createForm() {
    debugger
    this.featureForm = this.formBuilder.group({
      featureID: 0,
      name: ['', Validators.required],
      arabicName: [''],
      displayOrder: [],
      statusID: [true],
      image: [''],
    });
  }

  private editForm(obj) {
    debugger
    this.f.name.setValue(obj.name);
    this.f.arabicName.setValue(obj.arabicName);
    this.f.displayOrder.setValue(obj.displayOrder);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.f.image.setValue(obj.image);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedFeature() {
    debugger
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingCustomer = true;
        this.f.featureID.setValue(sid);
        this.featureService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingCustomer = false;
        });
      }
    })
  }

  onSubmit() {
    debugger
    this.featureForm.markAllAsTouched();
    this.submitted = true;
    if (this.featureForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);


    if (parseInt(this.f.featureID.value) === 0) {
      //Insert services
      this.featureService.insert(this.featureForm.value).subscribe(data => {
        debugger
        if (data != 0) {
          debugger
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/features']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });
    }
    else {
      //Update banner
      this.featureService.update(this.featureForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/features']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }



}
