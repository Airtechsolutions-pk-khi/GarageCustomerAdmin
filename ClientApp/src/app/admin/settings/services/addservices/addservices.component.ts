import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { ServicesService } from 'src/app/_services/services.service';


@Component({
  selector: 'app-addservices',
  templateUrl: './addservices.component.html',
  styleUrls: ['./addservices.component.css']
})
export class AddservicesComponent implements OnInit {

  submitted = false;
  serviceForm: FormGroup;
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
    private servicesService: ServicesService

  ) {
    this.createForm();
  }

  ngOnInit() {
     this.setSelectedCustomer();
  }

  get f() { return this.serviceForm.controls; }

  private createForm() {
    debugger
    this.serviceForm = this.formBuilder.group({
      serviceID: 0,
      serviceTitle: ['', Validators.required],
      serviceDescription: [''],    
      arabicServiceTitle: [''],
      arabicServiceDescription: [''],  
      displayOrder: [0],      
      statusID: [true],      
      image: [''],   
      type: ['']            
    });
  }

  private editForm(obj) {
    debugger
    this.f.serviceTitle.setValue(obj.serviceTitle);
    this.f.arabicServiceTitle.setValue(obj.arabicServiceTitle);
    this.f.serviceID.setValue(obj.serviceID);
    this.f.image.setValue(obj.image);
    this.f.serviceDescription.setValue(obj.serviceDescription);
    this.f.arabicServiceDescription.setValue(obj.arabicServiceDescription);
    this.f.displayOrder.setValue(obj.displayOrder);
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
        this.f.serviceID.setValue(sid);
        this.servicesService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingCustomer = false;
        });
      }
    })
  }

  onSubmit() {
    debugger
    this.serviceForm.markAllAsTouched();
    this.submitted = true;
    if (this.serviceForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    

    if (parseInt(this.f.serviceID.value) === 0) {
      debugger
      //Insert services
      console.log(JSON.stringify(this.serviceForm.value));
      this.servicesService.insert(this.serviceForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/services']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update banner
      this.servicesService.update(this.serviceForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/services']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }



}
