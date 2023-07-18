import { Injectable} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { switchMap, tap, map } from 'rxjs/operators';
import { SortColumn, SortDirection } from '../_directives/sortable.directive';
import { State } from '../_models/State';
import { CarSellImages, CarSells } from '../_models/CarSell';
import { Features } from '../_models/Feature';
import { City } from '../_models/City';
import { BodyType } from '../_models/BodyType';
import { Customers, Make, Models } from '../_models/Carsell';


interface SearchCarSellResult {
  data: CarSells[];
  total: number;
}
const compare = (v1: string, v2: string) => v1 < v2 ? -1 : v1 > v2 ? 1 : 0;

function sort(data: CarSells[], column: SortColumn, direction: string): CarSells[] {
  if (direction === '' || column === '') {
    return data;
  } else {
    return [...data].sort((a, b) => {
      const res = compare(`${a[column]}`, `${b[column]}`);
      return direction === 'asc' ? res : -res;
    });
  }
}

function matches(data: CarSells, term: string) {
  
  return data.name.toLowerCase().includes(term.toLowerCase())
}

@Injectable({
  providedIn: 'root'
})

export class CarSellService {

  constructor(private http: HttpClient) {
  }
  
  private _loading$ = new BehaviorSubject<boolean>(true);
  private _search$ = new Subject<void>();
  private _allData$ = new BehaviorSubject<CarSells[]>([]);
  private _data$ = new BehaviorSubject<CarSells[]>([]);
  private _total$ = new BehaviorSubject<number>(0);
  public carsells: CarSells[];
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
  
  getById(id) {
    debugger
    return this.http.get<any[]>(`api/carsell/${id}`);
  }
  getcarId(id) {
    debugger
    return this.http.get<any[]>(`api/carsell/carsellid/${id}`);
  }
  printorder(id) {
    return this.http.get<CarSells[]>(`api/orders/print/${id}`);
  }
  getAllData(fromDate,toDate) {

    const url = `api/carsell/all/${fromDate}/${toDate}`;
    console.log(url);
    tap(() => this._loading$.next(true)),
      this.http.get<CarSells[]>(url).subscribe(res => {
        this.carsells = res;
           
        this._data$.next(this.carsells);
        this._allData$.next(this.carsells);

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

  private _search(): Observable<SearchCarSellResult> {
    const { sortColumn, sortDirection, pageSize, page, searchTerm } = this._state;

    // 1. sort
    let sortedData = sort(this.carsells, sortColumn, sortDirection);

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
  insert(data) {
    debugger;
    return this.http.post('api/carsell/insert', data)
      .pipe(map(res => {        
        console.log(res);
        return res;
      }));
  }
  loadCarSellImages(id) {
    debugger
    return this.http.get<CarSellImages[]>( `api/carsell/images/${id}`);
  }
  loadFeature() {
    return this.http.get<Features[]>( `api/feature/all`);
  }
  loadBodyType() {
    return this.http.get<BodyType[]>(`api/bodytype/all`);
  }
  loadMake() {
    return this.http.get<Make[]>( `api/carsell/allMake`);
  }
  loadModel(event) {
    return this.http.get<Models[]>( `api/carsell/allModel/${event}`);
  }
  loadCountry() {
    return this.http.get<Features[]>( `api/carsell/allCountry`);
  }
  loadCustomer() {
    return this.http.get<Customers[]>(`api/customer/all`);
  }
  loadCity(event) {
    debugger
    return this.http.get<any[]>( `api/carsell/allCity/${event}`);
  }
  update(updateData) {
    return this.http.post(`api/carsell/update`, updateData)
      .pipe(map(res => {
        console.log(res);
        return res;
      }));
  }   
  updatestatus(carsell) {
    debugger
    return this.http.post(`api/carsell/updatestatus`, carsell)
      .pipe(map(res => {
        console.log(res);
        return res;
      }));
  } 
}
