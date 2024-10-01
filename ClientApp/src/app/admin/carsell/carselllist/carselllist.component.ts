import { HttpClient } from '@angular/common/http';
import { Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbDate, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of, Subscription } from 'rxjs';
import { delay, map } from 'rxjs/operators';
import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { CarSells } from 'src/app/_models/CarSell';
import { CarSellService } from 'src/app/_services/carsell.service';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { NgbdModalContent } from '../../sales/orders/modal-content/ngbd-OrderDetail-content.component';
const now = new Date();
@Component({
  selector: 'app-carselllist',
  templateUrl: './carselllist.component.html',
  styleUrls: ['./carselllist.component.css'],
  providers: [ExcelService]
})

export class CarselllistComponent implements OnInit {
  data$: Observable<CarSells[]>;
  FeatureList =[];
  oldData: CarSells[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
  
   
   submit: boolean;
  
   @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
   
  
  constructor(public service: CarSellService,
    public ls: LocalStorageService,
    public excelService: ExcelService,
    public ts: ToastService,
    
    public router: Router) {
    this.loading$ = service.loading$;
    this.submit = false;
    

    //this.selectedBrand = this.ls.getSelectedBrand().brandID;
    //this.loadLocations();
  }
  ngOnInit() {
    //const date: NgbDate = new NgbDate(now.getFullYear(), now.getMonth() + 1, 1);
    //this._datepicker.fromDate = date;
    this.getData();
  }
  Edit(carSellID) {
    debugger
    this.router.navigate(["admin/carsell/edit", carSellID]);
  }

  updateOrder(order, status) {
    debugger
    order.statusID = status;
    //Update customer
    this.service.update(order).subscribe(data => {

      if (data != 0) {
        this.ts.showSuccess("Success", "Record updated successfully.")
        this.router.navigate(['/admin/carsell']);
      }
    }, error => {
      this.ts.showError("Error", "Failed to update record.")
    });
  }

  //UpdateStatus(item, status) {
  //  debugger
  //  item.statusID = status;
  //  //Update customer
  //  this.service.updatestatus(item).subscribe(data => {

  //    if (data != 0) {
  //      this.ts.showSuccess("Success", "Record updated successfully.")
  //      this.router.navigate(['/admin/carsell']);
  //    }
  //  }, error => {
  //    this.ts.showError("Error", "Failed to update record.")
  //  });
  //}
  

  getData() {
     debugger
    this.service.getAllData();//(this.parseDate(this._datepicker.fromDate), this.parseDate(this._datepicker.toDate));
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

  View(carSellID) {
    debugger
    this.router.navigate(["admin/carselldetails/view", carSellID]);
  }
  Print(sid) {
    this.service.printorder(sid).subscribe((res: any) => {
      //Set Forms
      
      if (res.status == 1) {
        this.printout(res.html);
      }
      else
        this.ts.showError("Error", "Failed to print.")
    });
  }
  parseDate(obj) {
    return obj.year + "-" + obj.month + "-" + obj.day;;
  }

  
  Filter() {
    
    this.getData();
  }
  printout(html) {

    var newWindow = window.open('_self');
    newWindow.document.write(html);
    newWindow.print();
  }
}
