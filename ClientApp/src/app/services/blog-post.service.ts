import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BlogPost } from '../models/blogpost';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {
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
    this.apiUrl = 'api/blogposts';
    this.retries = 3;
  }

  getBlogPosts(): Observable<BlogPost[]> {
    return this.http.get<BlogPost[]>(this.appUrl + this.apiUrl)
    .pipe(
      retry(this.retries),
      catchError(this.errorHandler)
    );
  }

  getBlogPost(postId: number): Observable<BlogPost> {
    return this.http.get<BlogPost>(this.appUrl + this.apiUrl + '/' + postId)
    .pipe(
      retry(this.retries),
      catchError(this.errorHandler)
    );
  }

  saveBlogPost(blogPost): Observable<BlogPost> {
    return this.http.post<BlogPost>(this.appUrl + this.apiUrl, JSON.stringify(blogPost), this.httpOptions)
    .pipe(
      retry(this.retries),
      catchError(this.errorHandler)
    );
  }

  updateBlogPost(postId: number, blogPost): Observable<BlogPost> {
    return this.http.put<BlogPost>(this.appUrl + this.apiUrl + '/' + postId, JSON.stringify(blogPost), this.httpOptions)
    .pipe(
      retry(this.retries),
      catchError(this.errorHandler)
    );
  }

  deleteBlogPost(postId: number): Observable<BlogPost> {
      return this.http.delete<BlogPost>(this.appUrl + this.apiUrl + '/' + postId)
      .pipe(
        retry(this.retries),
        catchError(this.errorHandler)
      );
  }

  errorHandler(error) {
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
