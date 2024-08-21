import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { switchMap, tap, map } from 'rxjs/operators';
import { SortColumn, SortDirection } from '../_directives/sortable.directive';
import { State } from '../_models/State';
import { Location, LocationImages, LocationTimings } from '../_models/Location';
import { Amenities } from '../_models/Amenities';
import { Services } from '../_models/Services';
import { Landmark } from '../_models/Landmark';

interface SearchLocationsResult {
  data: Location[];
  total: number;
}
const compare = (v1: string, v2: string) => v1 < v2 ? -1 : v1 > v2 ? 1 : 0;

function sort(data: Location[], column: SortColumn, direction: string): Location[] {
  if (direction === '' || column === '') {
    return data;
  } else {
    return [...data].sort((a, b) => {
      const res = compare(`${a[column]}`, `${b[column]}`);
      return direction === 'asc' ? res : -res;
    });
  }
}

function matches(data: Location, term: string) {
  return data.name.toLowerCase().includes(term.toLowerCase())
}

@Injectable({
  providedIn: 'root'
})

export class LocationsService {

  constructor(private http: HttpClient) {
  }

  private _loading$ = new BehaviorSubject<boolean>(true);
  private _search$ = new Subject<void>();
  private _allData$ = new BehaviorSubject<Location[]>([]);
  private _data$ = new BehaviorSubject<Location[]>([]);
  private _total$ = new BehaviorSubject<number>(0);
  public locations: Location[];
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
    return this.http.get<Location[]>(`api/location/${id}`);
  }
  getAllLocations() {
    return this.http.get<Location[]>(`api/location/all`);
  }
  getAllData() {
    
    const url = `api/location/all`;
    console.log(url);
    tap(() => this._loading$.next(true)),
      this.http.get<Location[]>(url).subscribe(res => {
        this.locations = res;
          
        this._data$.next(this.locations);
        this._allData$.next(this.locations);

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

  private _search(): Observable<SearchLocationsResult> {
    const { sortColumn, sortDirection, pageSize, page, searchTerm } = this._state;

    // 1. sort
    let sortedData = sort(this.locations, sortColumn, sortDirection);

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
  loadLocationImages(id) {
    return this.http.get<LocationImages[]>( `api/location/images/${id}`);
  }
  loadTimings(id) {
    return this.http.get<LocationTimings[]>(`api/location/loadTimings/${id}`);
  }
  loadAmenities() {
    return this.http.get<Amenities[]>( `api/amenities/all`);
  }
  loadService() {
    return this.http.get<Services[]>( `api/service/all`);
  }
  loadLandmark() {
    return this.http.get<Landmark[]>( `api/landmark/all`);
  }
  insert(data) {
    return this.http.post('api/location/insertlocation', data)
      .pipe(map(res => {        
        console.log(res);
        return res;
      }));
  }

  update(update) {
    debugger
    return this.http.post('api/location/updated', update)
      .pipe(map(res => {
        console.log(res);
        return res;
      }));
  }
 
  delete(updateData) {  
    return this.http.post(`api/location/delete`, updateData);
  }

}
