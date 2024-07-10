import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { BodyTypeService } from 'src/app/_services/bodyType.service';
import { ToastService } from 'src/app/_services/toastservice';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-addbodyType',
  templateUrl: './addbodyType.component.html',
  styleUrls: ['./addbodyType.component.css']
})
export class AddbodyTypeComponent implements OnInit {
  submitted = false;
  bodytypeForm: FormGroup;
  loading = false;
  loadingbodyType = false;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public ts: ToastService,
    private ls: LocalStorageService,
    private bodytypeService: BodyTypeService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelected();
  }

  get f() { return this.bodytypeForm.controls; }

  private createForm() {
    this.bodytypeForm = this.formBuilder.group({
      name: ['', Validators.required],
      displayOrder: [],
      arabicName: [''],
      statusID: [true],
      bodyTypeID: 0,
      image: [''],
    });
  }

  private editForm(obj) {
    
    this.f.name.setValue(obj.name);
    this.f.displayOrder.setValue(obj.displayOrder);
    this.f.arabicName.setValue(obj.arabicName);
    this.f.bodyTypeID.setValue(obj.bodyTypeID);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelected() {
    
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingbodyType = true;
        this.f.bodyTypeID.setValue(sid);
        this.bodytypeService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingbodyType = false;
        });
      }
    })
  }

  onSubmit() {
    debugger
    this.bodytypeForm.markAllAsTouched();
    this.submitted = true;
    if (this.bodytypeForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    if (parseInt(this.f.bodyTypeID.value) === 0) {

      //Insert modifier
      console.log(JSON.stringify(this.bodytypeForm.value));
      this.bodytypeService.insert(this.bodytypeForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/bodytype']);
        }
        // this.alertService.success("Item has been created");
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update modifier
      this.bodytypeService.update(this.bodytypeForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/bodytype']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
}

