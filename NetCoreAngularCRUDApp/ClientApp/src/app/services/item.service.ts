import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Item } from '../models/item';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ItemService {
  appUrl: string;
  apiUrl: string;
  retries: number;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) {
    this.appUrl = environment.appUrl;
    this.apiUrl = 'api/item';
    this.retries = 3;
  }

  getItems(): Observable<Item[]> {
    return this.http.get<Item[]>(this.appUrl + this.apiUrl)
    .pipe(
      retry(this.retries),
      catchError(this.errorHandler)
    );
  }

  getItem(id: number): Observable<Item> {
    return this.http.get<Item>(this.appUrl + this.apiUrl + '/' + id)
    .pipe(
      retry(this.retries),
      catchError(this.errorHandler)
    );
  }

  errorHandler(error): Observable<never> {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
