import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';

import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';

import { ToastService } from 'src/app/_services/toastservice';
import { setting } from 'src/app/_models/Setting';
import { SettingService } from 'src/app/_services/setting.service';
import { ConfirmationDialogService } from '../confirm/confirmation-dialog.service';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  providers: []
})

export class SettingComponent implements OnInit {
  data$: Observable<setting[]>;
  oldData: setting[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedBrand;

  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
  

  constructor(public service: SettingService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public router: Router,
    private confirmationDialogService: ConfirmationDialogService) {
    //this.selectedBrand = this.ls.getSelectedBrand().brandID;

    this.loading$ = service.loading$;
    this.submit = false;

  }

  ngOnInit() {
    this.getData();
  }
  public async openConfirmationDialog(item) {
    debugger
    
      if(await this.confirmationDialogService.confirm('Please confirm..', 'Do you really want to delete ... ?') === true)
      {
        this.service.delete(item).subscribe((res: any) => {
          if(res!=0){
            this.ts.showSuccess("Success","Record deleted successfully.")
            this.getData();
          }
          else
          this.ts.showError("Error","Failed to delete record.")
    
        }, error => {
          this.ts.showError("Error","Failed to delete record.")
        });
      }
    else{}
  }
 
  getData() {
    this.service.getAllData();
    this.data$ = this.service.data$;
    this.total$ = this.service.total$;
    this.loading$ = this.service.loading$;
  }

  onSort({ column, direction }: SortEvent) {

    this.headers.forEach(header => {
      if (header.sortable !== column) {
        header.direction = '';
      }
    });
    this.service.sortColumn = column;
    this.service.sortDirection = direction;
  }

  Edit(setting) {
    this.router.navigate(["admin/setting/edit", setting]);
  }

  // Delete(obj) {
  //   this.service.delete(obj).subscribe((res: any) => {
  //     if (res != 0) {
  //       this.ts.showSuccess("Success", "Record deleted successfully.")
  //       this.getData();
  //     }
  //     else
  //       this.ts.showError("Error", "Failed to delete record.")

  //   }, error => {
  //     this.ts.showError("Error", "Failed to delete record.")
  //   });
  // }

}
