import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';

import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { reviews } from 'src/app/_models/Reviews';
import { ToastService } from 'src/app/_services/toastservice';
import { ReviewsService } from 'src/app/_services/reviews.service';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  providers: []
})

export class ReviewsComponent implements OnInit {
  data$: Observable<reviews[]>;  
  oldData: reviews[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedBrand;
  
  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: ReviewsService,
    public ls :LocalStorageService,
    public ts :ToastService,
    public router:Router) {

    this.loading$ = service.loading$;
    this.submit = false;
    
  }

  ngOnInit() {
    this.getData();
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

  Edit(banner) {
        this.router.navigate(["admin/banner/edit", banner]);
  }

}
