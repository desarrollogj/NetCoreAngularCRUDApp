import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { throwError, Observable } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { BlogCategory } from '../models/blogcategory';

@Injectable({
  providedIn: 'root'
})
export class BlogCategoryService {
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
    this.apiUrl = 'api/blogcategory';
    this.retries = 3;
  }

  getBlogCategories(): Observable<BlogCategory[]> {
    return this.http.get<BlogCategory[]>(this.appUrl + this.apiUrl)
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
