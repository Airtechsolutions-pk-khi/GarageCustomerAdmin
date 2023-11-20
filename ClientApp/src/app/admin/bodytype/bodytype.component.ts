import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BodyType } from 'src/app/_models/BodyType';
import { BodyTypeService } from 'src/app/_services/bodyType.service';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { ConfirmationDialogService } from '../settings/confirm/confirmation-dialog.service';

@Component({
  selector: 'app-bodytype',
  templateUrl: './bodytype.component.html',
  providers: [ExcelService]
})

export class BodyTypeComponent implements OnInit {
  data$: Observable<BodyType[]>;  
  oldData: BodyType[];
  total$: Observable<number>;
  loading$: Observable<boolean>; 
  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: BodyTypeService,
    public ls :LocalStorageService,
    public excelService: ExcelService,
    public ts :ToastService,
    public router: Router,
    private confirmationDialogService: ConfirmationDialogService) {
    this.loading$ = service.loading$;
    this.submit = false;    
  }

  ngOnInit() {
    this.getData();
  }
  exportAsXLSX(): void {
    this.service.ExportList().subscribe((res: any) => {    
      this.excelService.exportAsExcelFile(res, 'Report_Export');
    }, error => {
      this.ts.showError("Error","Failed to export")
    });
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

  Edit(bodytype) {
        
    this.router.navigate(["admin/bodytype/edit", bodytype]);
  }

  public async openConfirmationDialog(item) {
    debugger

    if (await this.confirmationDialogService.confirm('Please confirm..', 'Do you really want to delete ... ?') === true) {
      this.service.delete(item).subscribe((res: any) => {
        if (res != 0) {
          this.ts.showSuccess("Success", "Record deleted successfully.")
          this.getData();
        }
        else
          this.ts.showError("Error", "Failed to delete record.")

      }, error => {
        this.ts.showError("Error", "Failed to delete record.")
      });
    }
    else { }
  }


  Delete(obj) {
    
    this.service.delete(obj).subscribe((res: any) => {
      if(res!=0){
        this.ts.showSuccess("Success","Record deleted successfully.")
        this.getData();
      }
      else
      this.ts.showError("Error","Failed to delete record.");

    }, error => {
     this.ts.showError("Error","Failed to delete record.")
    });
  }
}
