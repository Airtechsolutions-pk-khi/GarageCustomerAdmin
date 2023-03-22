import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { switchMap, tap, map } from 'rxjs/operators';
import { SortColumn, SortDirection } from '../_directives/sortable.directive';
import { State } from '../_models/State';
import { reviews } from '../_models/Reviews';


interface SearchReviewsResult {
  data: reviews[];
  total: number;
}
const compare = (v1: string, v2: string) => v1 < v2 ? -1 : v1 > v2 ? 1 : 0;

function sort(data: reviews[], column: SortColumn, direction: string): reviews[] {
  if (direction === '' || column === '') {
    return data;
  } else {
    return [...data].sort((a, b) => {
      const res = compare(`${a[column]}`, `${b[column]}`);
      return direction === 'asc' ? res : -res;
    });
  }
}

function matches(data: reviews, term: string) {
  debugger
  return data.name.toLowerCase().includes(term.toLowerCase())
}

@Injectable({
  providedIn: 'root'
})

export class ReviewsService {

  constructor(private http: HttpClient) {
  }
  private _loading$ = new BehaviorSubject<boolean>(true);
  private _search$ = new Subject<void>();
  private _allData$ = new BehaviorSubject<reviews[]>([]);
  private _data$ = new BehaviorSubject<reviews[]>([]);
  private _total$ = new BehaviorSubject<number>(0);
  public Review: reviews[];
  private _state: State = {
    page: 1,
    pageSize: 10,
    searchTerm: '',
    sortColumn: '',
    sortDirection: ''
  };

  get total$() { return this._total$.asObservable(); }
  get loading$() { return this._loading$.asObservable(); }
  get page() { return this._state.page; }
  get pageSize() { return this._state.pageSize; }
  get searchTerm() { return this._state.searchTerm; }

  set page(page: number) { this._set({ page }); }
  set pageSize(pageSize: number) { this._set({ pageSize }); }
  set searchTerm(searchTerm: any) { this._set({ searchTerm }); }
  set sortColumn(sortColumn: SortColumn) { this._set({ sortColumn }); }
  set sortDirection(sortDirection: SortDirection) { this._set({ sortDirection }); }

  get data$() {
    return this._data$.asObservable();
  }
  get allData$() {
    return this._allData$.asObservable();
  }

  getAllData() {

    const url = `api/reviews/all/`;
    console.log(url);
    tap(() => this._loading$.next(true)),
      this.http.get<reviews[]>(url).subscribe(res => {
        this.Review = res;
        debugger
        this._data$.next(this.Review);
        this._allData$.next(this.Review);

        this._search$.pipe(
          switchMap(() => this._search()),
          tap(() => this._loading$.next(false))
        ).subscribe(result => {
          this._data$.next(result.data);
          this._total$.next(result.total);
        });

        this._search$.next();
      });
  }
  private _set(patch: Partial<State>) {
    Object.assign(this._state, patch);
    this._search$.next();
  }

  private _search(): Observable<SearchReviewsResult> {
    const { sortColumn, sortDirection, pageSize, page, searchTerm } = this._state;

    // 1. sort
    let sortedData = sort(this.Review, sortColumn, sortDirection);

    //// 2. filter
    sortedData = sortedData.filter(data => matches(data, searchTerm));
    const total = sortedData.length;

    // 3. paginate
    const data = sortedData.slice((page - 1) * pageSize, (page - 1) * pageSize + pageSize);
    return of({ data, total });
  }


  clear() {
    // clear by calling subject.next() without parameters
    this._search$.next();
    this._data$.next(null);
    this._allData$.next(null);
    this._total$.next(null);
    this._loading$.next(null);
    this._state = {
      page: 1,
      pageSize: 10,
      searchTerm: '',
      sortColumn: '',
      sortDirection: ''
    };
  }

}
