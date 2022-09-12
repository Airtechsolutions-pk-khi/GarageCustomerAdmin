import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { SettingService } from 'src/app/_services/setting.service';


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

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private settingService: SettingService

  ) {
    this.createForm();
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
      displayOrder: [0],    
      pageName: [''],      
      type: [''],  
      statusID: [true],      
      image: [''],            
    });
  }

  private editForm(obj) {
    this.f.title.setValue(obj.title);     
    this.f.id.setValue(obj.id);
    this.f.image.setValue(obj.image);
    this.f.description.setValue(obj.description);   
    this.f.displayOrder.setValue(obj.displayOrder);
    this.f.pageName.setValue(obj.pageName);
    this.f.type.setValue(obj.type);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
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

  onSubmit() {
    debugger
    this.settingForm.markAllAsTouched();
    this.submitted = true;
    if (this.settingForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    

    if (parseInt(this.f.id.value) === 0) {
      debugger
      //Insert services
      console.log(JSON.stringify(this.settingForm.value));
      this.settingService.insert(this.settingForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/setting']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update banner
      this.settingService.update(this.settingForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/setting']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }



}
