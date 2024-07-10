import { HttpClient } from '@angular/common/http';
import { Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbDate, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of, Subscription } from 'rxjs';
import { delay, map } from 'rxjs/operators';
import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { Cars } from 'src/app/_models/Cars';
import { CarsService } from 'src/app/_services/cars.service';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';
//import { NgbdModalContent } from '../../sales/orders/modal-content/ngbd-OrderDetail-content.component';
const now = new Date();
@Component({
  selector: 'app-carslist',
  templateUrl: './carslist.component.html',
  styleUrls: ['./carslist.component.css'],
  providers: [ExcelService]
})

export class CarslistComponent implements OnInit {
  data$: Observable<Cars[]>;
  oldData: Cars[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
  
   
   submit: boolean;
  
   @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
   
  
  constructor(public service: CarsService,
    public ls: LocalStorageService,
    public excelService: ExcelService,
    public ts: ToastService,
    
    public router: Router) {
    this.loading$ = service.loading$;
    this.submit = false;

  }
  ngOnInit() {
    const date: NgbDate = new NgbDate(now.getFullYear(), now.getMonth() + 1, 1);
    this._datepicker.fromDate = date;
    this.getData();
  }
  Edit(carID) {
     
    this.router.navigate(["admin/cars/edit", carID]);
  }

  updateCars(cars, status) {
    debugger
    cars.statusID = status;
    //Update customer
    this.service.update(cars).subscribe(data => {

      if (data != 0) {
        this.ts.showSuccess("Success", "Record updated successfully.")
        this.router.navigate(['/admin/cars']);
      }
    }, error => {
      this.ts.showError("Error", "Failed to update record.")
    });
  }

  getData() {
    debugger
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

  parseDate(obj) {
    return obj.year + "-" + obj.month + "-" + obj.day;;
  }
  
  Filter() {
    
    this.getData();
  }
}
