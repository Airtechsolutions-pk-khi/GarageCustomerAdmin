import { HttpClient } from '@angular/common/http';
import { Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of, Subscription } from 'rxjs';
import { delay, map } from 'rxjs/operators';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { Features } from '../../_models/Feature';
import { FeaturesService } from '../../_services/features.service';
import { ConfirmationDialogService } from '../settings/confirm/confirmation-dialog.service';

@Component({
  selector: 'app-featurelist',
  templateUrl: './featurelist.component.html',
  styleUrls: ['./featurelist.component.css'],
  providers: [ExcelService]
})
export class FeaturelistComponent implements OnInit {
  data$: Observable<Features[]>;
  FeatureList = [];
  oldData: Features[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  submit: boolean;

  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: FeaturesService,
    public ls: LocalStorageService,
    public excelService: ExcelService,
    public ts: ToastService,
    public router: Router,
    private confirmationDialogService: ConfirmationDialogService) {
    this.loading$ = service.loading$;
    this.submit = false;
  }

  ngOnInit() {
    this.getData();
  }

  Edit(featureID) {
    this.router.navigate(["admin/features/edit", featureID]);
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
      if (res != 0) {
        this.ts.showSuccess("Success", "Record deleted successfully.")
        this.getData();
      }
      else
        this.ts.showError("Error", "Failed to delete record.");
    }, error => {
      this.ts.showError("Error", "Failed to delete record.")
    });
  }

  updateOrder(order, status) {
    debugger
    order.statusID = status;
    //Update customer
    this.service.update(order).subscribe(data => {

      if (data != 0) {
        this.ts.showSuccess("Success", "Record updated successfully.")
        this.router.navigate(['/admin/features']);
      }
    }, error => {
      this.ts.showError("Error", "Failed to update record.")
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

  View(carsell) {
    this.router.navigate(["admin/carselldetails/view", carsell]);
  }

  Filter() {

    this.getData();
  }
}
