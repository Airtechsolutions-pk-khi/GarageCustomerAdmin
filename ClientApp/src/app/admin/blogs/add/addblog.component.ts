import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { LocationsService } from 'src/app/_services/locations.service';
import { ToastService } from 'src/app/_services/toastservice';
import { BlogService } from '../../../_services/blog.service';
//import { EditorModule } from '@tinymce/tinymce-angular';
/*import tinymce from 'tinymce';*/
declare var tinymce: any;
@Component({
  selector: 'app-addblog',
  templateUrl: './addblog.component.html',
  styleUrls: ['./addblog.component.css']
})
export class AddBlogComponent implements OnInit {
  submitted = false;
  blogForm: FormGroup;
  loading = false;
  Images = [];
  CountryList = [];
  CityList = [];
  selectedCityID = [];
  ButtonText = "Save";
  country = "";
  datesstart: Date[];
  datesend: Date[];

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    public blogService: BlogService,

  ) {
    this.createForm();
    this.loadCountry();
  }

  ngOnInit() {
    this.setSelectedBlog();
    this.edtor();

  }
  getContentFromEditor(editorId: string): string {
    return tinymce.get(editorId).getContent();
  }

  get f() { return this.blogForm.controls; }

  private createForm() {
    this.blogForm = this.formBuilder.group({
      blogID: 0,
      title: ['', Validators.required],
      arabicTitle: [''],
      description: ['', Validators.required],
      arabicDescription: [''],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      statusID: [1],
      isFeatured: [false],
      imagesSource: ['', Validators.required],
      imageUrl: [''],
      blogImages: [],
      city: [, Validators.required],
      country: ['', Validators.required],
      file: ['', Validators.required],
      type: [''],
      formattedStartDate: [''],
      formattedEndDate: [''],
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
            this.blogForm.patchValue({
              imagesSource: this.Images
            });
          }
          reader.readAsDataURL(event.target.files[i]);
        }
      }
    }
  }
  private formatDate(date: Date): string {
    const day = date.getDate();
    const month = date.getMonth() + 1; // Month indices are 0-based
    const year = date.getFullYear();

    const formattedDate = `${day}/${month}/${year}`;
    return formattedDate;
  }
  private editForm(obj) {
    debugger

    const desc = tinymce.get("description").setContent(obj.description);
    const arabicDescription = tinymce.get("arabicDescription").setContent(obj.arabicDescription);

    this.f.title.setValue(obj.title);
    this.f.arabicTitle.setValue(obj.arabicTitle);
    this.f.description.setValue(desc);
    this.f.arabicDescription.setValue(arabicDescription);
    this.f.city.setValue(obj.city);
    this.f.country.setValue(obj.country);

    //const startDate = new Date(obj.startDate);
    //const endDate = new Date(obj.endDate);

    //const formattedStartDate = this.formatDate(startDate);
    //const formattedEndDate = this.formatDate(endDate);

    ////this.blogForm.get('formattedStartDate').setValue(formattedStartDate);
    ////this.blogForm.get('formattedEndDate').setValue(formattedEndDate);
    this.f.startDate.setValue(obj.startDate);
    this.f.endDate.setValue(obj.endDate);
    this.f.type.setValue(obj.type);
    this.f.blogID.setValue(obj.blogID);
    this.f.statusID.setValue(obj.statusID);
    this.loadBlogImages(this.f.blogID.value);

    if (obj.country != "") {
      this.blogService.loadCity(obj.country).subscribe((res: any) => {
        this.CityList = res;
      });
    }
  }
  private loadBlogImages(id) {
    debugger
    this.blogService.loadBlogImages(id).subscribe((res: any) => {
      this.Images = res;
      this.f.imagesSource.setValue(this.Images);
    });
  }
  removeImage(obj) {
    const index = this.Images.indexOf(obj);
    this.Images.splice(index, 1);

    this.f.imagesSource.setValue(this.Images);
  }
  edtor() {
    tinymce.init(
      {
        selector: "#description",

      });
    tinymce.init(
      {
        selector: "#arabicDescription",

      });
  }

  
  private loadCountry() {
    debugger
    this.blogService.loadCountry().subscribe((res: any) => {
      this.CountryList = res;
    });
  }

  onSelect(event) {
    let selectElementValue = event.target.value;
    let [index, value] = selectElementValue.split(':').map(item => item.trim());
    console.log(index);
    console.log(value);

    this.blogService.loadCity(value).subscribe((res: any) => {
      this.CityList = res;
    });
  }
  setSelectedBlog() {
    debugger
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loading = true;
        this.f.blogID.setValue(sid);
        this.blogService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loading = false;
        });
      }
    })
  }
  onSubmit() {
    debugger
    const description = this.getContentFromEditor('description');
    const arabicDescription = this.getContentFromEditor('arabicDescription');
    this.blogForm.markAllAsTouched();
    this.submitted = true;

    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.description.setValue(description);
    this.f.arabicDescription.setValue(arabicDescription);

    if (this.blogForm.invalid) { return; }
    this.loading = true;
    if (parseInt(this.f.blogID.value) === 0) {
      //Insert location
      this.blogService.insert(this.blogForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/blog']);
        }

      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update location
      this.blogService.update(this.blogForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/blog']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
}
